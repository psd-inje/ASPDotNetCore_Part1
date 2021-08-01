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
    public class IndexModel : PageModel
    {
        private readonly ZeroRolesType.Data.ApplicationDbContext _context;

        public IndexModel(ZeroRolesType.Data.ApplicationDbContext context)
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
