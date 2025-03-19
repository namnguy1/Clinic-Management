using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Domain.Entities
{
    public class PaymentTransaction
    {
        [Key]
        public int PaymentId { get; set; }

        [ForeignKey("Appointment")]
        public int? AppointmentId { get; set; } // Liên kết với lịch hẹn

        [Required]
        public PaymentMethodEnum? PaymentMethod { get; set; } // Stripe, PayPal, VNPay

        [Required]
        public decimal Amount { get; set; } // Số tiền thanh toán

        [Required]
        [MaxLength(50)]
        public PaymentStatusEnum? PaymentStatus { get; set; } // Pending, Completed, Failed

        [MaxLength(100)]
        public string? TransactionId { get; set; } // Mã giao dịch từ cổng thanh toán

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        // Navigation property nếu có entity Appointment
        public virtual Appointment? Appointment { get; set; }
    }
}