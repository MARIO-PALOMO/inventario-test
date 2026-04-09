using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventario.Domain.Entities
{
    [Table("TransactionType")]
    public class TransactionType
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
