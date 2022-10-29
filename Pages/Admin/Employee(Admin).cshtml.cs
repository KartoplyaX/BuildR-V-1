using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BuildR_V_1.Data;
using Microsoft.AspNetCore.Authorization;

namespace BuildR_V_1.Pages
{
    [Authorize(Roles = "Admin")]
    public class Employee_Admin_Model : PageModel
    {
        private readonly AppDbContext _db;

        public IList<BuildR> BuildR { get; private set; }
        [BindProperty]
        public String Search { get; set; }
        public Employee_Admin_Model(AppDbContext db) { _db = db; }
        public void OnGet()
        {
            BuildR = _db.BuildR.FromSqlRaw("SELECT * FROM BuildR WHERE Active = 1").ToList();
        }
        public IActionResult OnPostSearch()
        {
            BuildR = _db.BuildR.FromSqlRaw(
                "SELECT * FROM BuildR WHERE Active = 1 AND Name LIKE '%" + Search + "%'"
                ).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int itemID)
        {
            var item = await _db.BuildR.FindAsync(itemID);
            item.Active = false;//set item as deleted
            _db.Attach(item).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new Exception($"item {item.ID} not found", e);
            }
            return RedirectToPage("/Admin/Employee(Admin)");
        }
    }
}
