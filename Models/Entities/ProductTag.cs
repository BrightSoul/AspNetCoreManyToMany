using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreManyToManyDemo.Models.Entities
{
    public class ProductTag
    {
        public ProductTag(int tagId, int productId, DateTime associatedAt, string description)
        {
            TagId = tagId;
            ProductId = productId;
            AssociatedAt = associatedAt;
            Description = description;
        }

        public int TagId { get; private set; }
        public Tag Tag { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public DateTime AssociatedAt { get; private set; }
        public string Description { get; private set; }
    }
}
