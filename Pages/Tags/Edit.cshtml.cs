using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AspNetCoreManyToManyDemo.Models.Entities;
using AspNetCoreManyToManyDemo.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreManyToManyDemo.Pages.Tags
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

        public async Task<IActionResult> OnGet(int id)
        {
            var tag = await dbContext.Tags.FindAsync(id);
            if (tag == null)
            {
                return RedirectToPage("/Index");
            }

            Name = tag.Name;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var tag = await dbContext.Tags.FindAsync(id);
            if (tag == null)
            {
                return RedirectToPage("/Index");
            }
            if (ModelState.IsValid && await TryUpdateModelAsync(tag, string.Empty, x => x.Name))
            {
                tag.ModifiedAt = DateTime.Now;
                await dbContext.SaveChangesAsync();
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}