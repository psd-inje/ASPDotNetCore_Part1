using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZeroRolesType.Data;
using ZeroRolesType.Models;

namespace ZeroRolesType.Pages.Blogs
{
    public class IndexModel : PageModel
    {
        private readonly ZeroRolesType.Data.ApplicationDbContext _context;

        public IndexModel(ZeroRolesType.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Blog> Blog { get;set; }

        public async Task OnGetAsync()
        {
            Blog = await _context.Blog.ToListAsync();
        }
    }
}
