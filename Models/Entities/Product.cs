using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreManyToManyDemo.Models.Entities
{
    public class Product
    {
        public Product()
        {
            Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
