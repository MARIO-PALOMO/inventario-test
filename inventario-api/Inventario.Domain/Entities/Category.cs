using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventario.Domain.Entities
{
    [Table("Category")]
    public class Category : Base<long>
    {
        
        [Required]
        public string Name { get; set; }
    }
}
