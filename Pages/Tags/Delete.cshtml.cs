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
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        public DeleteModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var tag = await dbContext.Tags.FindAsync(id);
            if (tag == null)
            {
                return RedirectToPage("/Index");
            }

            dbContext.Remove(tag);
            await dbContext.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}