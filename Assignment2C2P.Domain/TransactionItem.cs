using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment2C2P.Domain
{
    public class TransactionItem
    {
        [Key]
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
    }
}
