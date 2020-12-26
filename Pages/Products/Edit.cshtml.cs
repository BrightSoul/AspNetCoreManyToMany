using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                product.Tags.Clear();
                if (TagIds.Length > 0)
                {
                    var selectedTags = await dbContext.Tags.Where(t => TagIds.Contains(t.Id)).ToListAsync();
                    foreach (var selectedTag in selectedTags)
                    {
                        product.Tags.Add(selectedTag);
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