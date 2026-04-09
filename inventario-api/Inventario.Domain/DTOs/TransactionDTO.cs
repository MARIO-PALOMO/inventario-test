using Inventario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario.Domain.DTOs
{
    public class TransactionDTO
    {
        public long TransactionId { get; set; }
        public Guid Code { get; set; }
        public DateTime Date { get; set; }
        public long TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Detail { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
