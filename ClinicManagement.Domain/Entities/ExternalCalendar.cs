using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Domain.Entities
{
    public class ExternalCalendar
    {
        [Key]
        public int ExternalCalendarId { get; set; }

        public int UserId { get; set; } // Liên kết với bảng Users

        [Required]
        [MaxLength(50)]
        public string? Provider { get; set; } // "Google", "Outlook"

        public string? AccessToken { get; set; } // OAuth Token

        public string? RefreshToken { get; set; } // Dùng để lấy lại AccessToken khi hết hạn

        public DateTime? ExpiresAt { get; set; } // Ngày hết hạn AccessToken

        [MaxLength(100)]
        public string? CalendarId { get; set; } // ID lịch của người dùng (nếu cần)
    }
}