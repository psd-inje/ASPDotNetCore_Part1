using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _20_02_ZeroRolesType.Data;
using _20_02_ZeroRolesType.Models;

namespace _20_02_ZeroRolesType.Pages.RoleTypeManager
{
    public class DetailsModel : PageModel
    {
        private readonly _20_02_ZeroRolesType.Data.ApplicationDbContext _context;

        public DetailsModel(_20_02_ZeroRolesType.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
