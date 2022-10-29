using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BuildR_V_1.Data;
using Microsoft.EntityFrameworkCore;

namespace BuildR_V_1.Pages.Admin
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public BuildR Item { get; set; }

        private readonly AppDbContext _db;
        public EditModel(AppDbContext db) { _db = db; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await _db.BuildR.FindAsync(id);
            if (Item == null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Item.Active = true;

            _db.Attach(Item).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new Exception($"Item {Item.ID} not found!", e);
            }
            return RedirectToPage("/Admin/Employee(Admin)");
        }
    }
}
