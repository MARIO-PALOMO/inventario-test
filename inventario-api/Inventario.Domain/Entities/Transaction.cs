using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventario.Domain.Entities
{
    [Table("Transaction")]
    public class Transaction : Base<long>
    {
        [Required]
        public Guid Code { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("TransactionType")]
        public long TransactionTypeId { get; set; }
        public virtual TransactionType? TransactionType { get; set; }


        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public string? Detail { get; set; }


        public Transaction()
        {
            this.Code = Guid.NewGuid();
        }
    }
}
