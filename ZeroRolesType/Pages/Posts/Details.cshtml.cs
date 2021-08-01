using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZeroRolesType.Data;
using ZeroRolesType.Models;

namespace ZeroRolesType.Pages.Posts
{
    public class DetailsModel : PageModel
    {
        private readonly ZeroRolesType.Data.ApplicationDbContext _context;

        public DetailsModel(ZeroRolesType.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post = await _context.Post
                .Include(p => p.Blog).FirstOrDefaultAsync(m => m.Id == id);

            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
