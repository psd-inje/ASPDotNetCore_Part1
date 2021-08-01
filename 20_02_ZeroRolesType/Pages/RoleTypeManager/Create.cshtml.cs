using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using _20_02_ZeroRolesType.Data;
using _20_02_ZeroRolesType.Models;

namespace _20_02_ZeroRolesType.Pages.RoleTypeManager
{
    public class CreateModel : PageModel
    {
        private readonly _20_02_ZeroRolesType.Data.ApplicationDbContext _context;

        public CreateModel(_20_02_ZeroRolesType.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RoleType RoleType { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RoleType.Add(RoleType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
