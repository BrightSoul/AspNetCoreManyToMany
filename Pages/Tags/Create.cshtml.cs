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
            var tag = new Tag();
            if (ModelState.IsValid && await TryUpdateModelAsync(tag, string.Empty, x => x.Name))
            {
                tag.CreatedAt = tag.ModifiedAt = DateTime.Now;
                dbContext.Add(tag);
                await dbContext.SaveChangesAsync();
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}