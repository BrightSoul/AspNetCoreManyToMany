using System.Collections.Generic;
using AspNetCoreManyToManyDemo.Models.Entities;

namespace AspNetCoreManyToManyDemo.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Product> Products { get; set; }
        public List<Tag> Tags { get; set; }
    }
}