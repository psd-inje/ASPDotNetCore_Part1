using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZeroRolesType.Data;
using ZeroRolesType.Models;

namespace ZeroRolesType.Pages.RoleTypeManager
{
    public class DeleteModel : PageModel
    {
        private readonly ZeroRolesType.Data.ApplicationDbContext _context;

        public DeleteModel(ZeroRolesType.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RoleType RoleType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoleType = await _context.RoleType.FirstOrDefaultAsync(m => m.Id == id);

            if (RoleType == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoleType = await _context.RoleType.FindAsync(id);

            if (RoleType != null)
            {
                _context.RoleType.Remove(RoleType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
