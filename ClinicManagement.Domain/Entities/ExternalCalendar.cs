using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ClinicManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagement.Domain.Entities
{
    public class ExternalCalendar
    {
        [Key]
        public int ExternalCalendarId { get; set; }

        public int UserId { get; set; } // Liên kết với bảng Users

        [Required]
        public CalendarProvider? Provider { get; set; } // "Google", "Outlook"

        [MaxLength(255)]
        public string? AccessToken { get; set; } // OAuth Token

        [MaxLength(255)]
        public string? RefreshToken { get; set; } // Dùng để lấy lại AccessToken khi hết hạn

        public DateTime? ExpiresAt { get; set; } // Ngày hết hạn AccessToken

        [MaxLength(100)]
        public string? CalendarId { get; set; } // ID lịch của người dùng (nếu cần)

        // Navigation property (nếu có entity User)
        public User? User { get; set; }
    }
}