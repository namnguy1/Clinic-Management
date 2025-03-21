using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Application.Dtos.Auth;

namespace ClinicManagement.Application.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(User user, string password,RegisterRequestDto additionalInfo);
        Task<string> LoginAsync(string email, string password);
    }
}