using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreManyToManyDemo.Models.Entities;
using AspNetCoreManyToManyDemo.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreManyToManyDemo.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        public EditModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        [BindProperty]
        public int[] TagIds { get; set; }

        public SelectList TagOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await dbContext.Products.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return RedirectToPage("/Index");
            }

            var tags = await dbContext.Tags.ToListAsync();

            Name = product.Name;
            Quantity = product.Quantity;
            TagIds = product.Tags.Select(t => t.Id).ToArray();
            TagOptions = new SelectList(tags, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var product = await dbContext.Products.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return RedirectToPage("/Index");
            }

            if (ModelState.IsValid && await TryUpdateModelAsync(product, string.Empty, x => x.Name, x => x.Quantity))
            {
                foreach (var tag in product.Tags)
                {
                    // Remove tags no longer associated to the product
                    if (!TagIds.Contains(tag.Id))
                    {
                        product.Tags.Remove(tag);
                    }
                }

                // Extract only new tags associated with the product (this preserves previously created associations)
                TagIds = TagIds.Except(product.Tags.Select(tag => tag.Id)).ToArray();
                if (TagIds.Length > 0)
                {
                    var selectedTags = await dbContext.Tags.Where(t => TagIds.Contains(t.Id)).ToListAsync();
                    foreach (var selectedTag in selectedTags)
                    {
                        ProductTag productTag = new(
                            productId: product.Id,
                            tagId: selectedTag.Id,
                            associatedAt: DateTime.UtcNow,
                            description: "Some info about this association");
                        
                        dbContext.ProductTags.Add(productTag);
                    }
                }

                product.ModifiedAt = DateTime.Now;
                await dbContext.SaveChangesAsync();
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}