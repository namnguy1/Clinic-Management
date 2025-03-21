using ClinicManagement.Application.Interfaces;
using ClinicManagement.Infrastructure.Repositories;
using ClinicManagement.Application.Services;
using ClinicManagement.Infrastructure.ClinicDbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Cấu hình SQL Server từ Connection String (đặt trong appsettings.json)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ClinicDbContext>(options =>
    options.UseSqlServer(connectionString));

// Đăng ký Repository và Service (Sử dụng Interface cho khả năng mở rộng và test dễ bảo trì)
builder.Services.AddScoped<IUserRepository, UserRepository>();
// Đăng ký các repository khác: AppointmentRepository, MedicalRecordRepository, PrescriptionRepository, v.v.
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

builder.Services.AddScoped<IPatientRepository, PatientRepository>();


// Cấu hình JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "clinic-management-api",
            ValidAudience = builder.Configuration["Jwt:Audience"] ?? "clinic-management-client",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured")))
        };
    });

// Các dịch vụ khác: AddControllers, Swagger, Authentication, etc.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Thêm middleware Authentication và Authorization
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
