using System;
using System.Threading.Tasks;
using AspNetCoreManyToManyDemo.Models.Entities;
using AspNetCoreManyToManyDemo.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreManyToManyDemo.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        public CreateModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Name { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var product = new Product();
            if (ModelState.IsValid && await TryUpdateModelAsync(product, string.Empty, x => x.Name))
            {
                product.CreatedAt = product.ModifiedAt = DateTime.Now;
                dbContext.Add(product);
                await dbContext.SaveChangesAsync();
                return RedirectToPage("/Products/Edit", new { id = product.Id });
            }

            return Page();
        }
    }
}