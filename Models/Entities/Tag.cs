using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreManyToManyDemo.Models.Entities
{
    public class Tag
    {
        public Tag()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
