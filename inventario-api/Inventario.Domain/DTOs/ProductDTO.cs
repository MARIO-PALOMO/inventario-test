using Inventario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario.Domain.DTOs
{
    public class ProductDTO
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public byte[] Image { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool StatusStock { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
