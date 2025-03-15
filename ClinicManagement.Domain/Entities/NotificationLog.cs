using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Domain.Entities
{
    public class NotificationLog
    {
        [Key]
        public int NotificationId { get; set; }

        public int? UserId { get; set; } // Người nhận thông báo

        [Required]
        [MaxLength(50)]
        public string NotificationType { get; set; } // Email, SMS, Push

        [Required]
        public string Message { get; set; } // Nội dung thông báo

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } // Success, Failed

        public string ErrorMessage { get; set; } // Lưu lỗi nếu có
    }
}