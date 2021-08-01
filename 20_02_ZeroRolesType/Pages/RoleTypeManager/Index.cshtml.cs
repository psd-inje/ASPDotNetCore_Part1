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
    public class IndexModel : PageModel
    {
        private readonly _20_02_ZeroRolesType.Data.ApplicationDbContext _context;

        public IndexModel(_20_02_ZeroRolesType.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RoleType> RoleType { get;set; }

        public async Task OnGetAsync()
        {
            RoleType = await _context.RoleType.ToListAsync();
        }
    }
}
