using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Domain.Entities
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        public int PaymentId { get; set; } // Liên kết với PaymentTransaction

        [Required]
        [MaxLength(50)]
        public string InvoiceNumber { get; set; } // Số hóa đơn

        public DateTime IssuedDate { get; set; } = DateTime.UtcNow;

        public string InvoiceDetails { get; set; } // Nội dung hóa đơn
    }
}