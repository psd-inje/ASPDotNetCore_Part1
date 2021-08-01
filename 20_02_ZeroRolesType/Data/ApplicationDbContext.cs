using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using _20_02_ZeroRolesType.Models;

namespace _20_02_ZeroRolesType.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<_20_02_ZeroRolesType.Models.RoleType> RoleType { get; set; }
    }
}
