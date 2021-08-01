using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeroRolesType.Models
{
    [Table("Blogs")]
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
