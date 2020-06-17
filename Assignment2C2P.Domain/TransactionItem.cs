using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment2C2P.Domain
{
    public class TransactionItem
    {
        [Key]
        [MaxLength(50)]
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionItemStatus Status { get; set; }
    }
}
