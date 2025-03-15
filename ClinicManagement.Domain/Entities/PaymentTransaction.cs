using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Domain.Entities
{
    public class PaymentTransaction
    {
        [Key]
        public int PaymentId { get; set; }

        public int? AppointmentId { get; set; } // Liên kết với lịch hẹn

        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; } // Stripe, PayPal, VNPay

        [Required]
        public decimal Amount { get; set; } // Số tiền thanh toán

        [Required]
        [MaxLength(50)]
        public string PaymentStatus { get; set; } // Pending, Completed, Failed

        public string TransactionId { get; set; } // Mã giao dịch từ cổng thanh toán

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    }
}