using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AspNetCoreManyToManyDemo.Models.Entities;
using AspNetCoreManyToManyDemo.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreManyToManyDemo.Pages.Products
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
            var product = await dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return RedirectToPage("/Index");
            }

            dbContext.Remove(product);
            await dbContext.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}