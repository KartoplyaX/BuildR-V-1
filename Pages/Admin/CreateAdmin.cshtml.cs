using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BuildR_V_1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuildR_V_1.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class CreateAdminModel : PageModel
    {
        private AppDbContext _db;
        [BindProperty]
        public BuildR Bildr { get; set; }

        public CreateAdminModel(AppDbContext db) { _db = db; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }
            Bildr.Active = true;
            foreach (var file in Request.Form.Files)
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                Bildr.ImageData = ms.ToArray();

                ms.Close();
                ms.Dispose();
            }
                _db.BuildR.Add(Bildr);
                await _db.SaveChangesAsync();
                return RedirectToPage("/Admin/CreateAdmin");
        }
        public void OnGet()
        { }
    }
}
