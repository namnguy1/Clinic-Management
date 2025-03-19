using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Domain.Entities
{
    public class NotificationLog
    {
        [Key]
        public int NotificationId { get; set; }

        public int? UserId { get; set; } // Người nhận thông báo

        [Required]
        public NotificationTypeEnum NotificationType { get; set; }

        [Required]
        public string? Message { get; set; } // Nội dung thông báo

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        [Required]
        public NotificationStatusEnum Status { get; set; }

        [MaxLength(500)]
        public string? ErrorMessage { get; set; } // Lưu lỗi nếu có

        public User? User { get; set; }
    }
}