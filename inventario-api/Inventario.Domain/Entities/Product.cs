using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventario.Domain.Entities
{
    [Table("Product")]
    public class Product : Base<Guid>
    {

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [ForeignKey("Category")]
        public long CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        [Required]
        public byte[] Image { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }

        public Product()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
