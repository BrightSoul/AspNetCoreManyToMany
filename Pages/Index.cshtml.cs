using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreManyToManyDemo.Models.Entities;
using AspNetCoreManyToManyDemo.Models.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreManyToManyDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        public IndexModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Product> Products { get; private set; }
        public List<Tag> Tags { get; private set; }

        public async Task OnGetAsync()
        {
            Products = await dbContext.Products.Include(p => p.Tags).ToListAsync();
            Tags = await dbContext.Tags.ToListAsync();
        }
    }
}