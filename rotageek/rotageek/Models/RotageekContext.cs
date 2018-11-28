using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace rotageek.Models
{
    public class RotageekContext : DbContext
    {
        public DbSet<ContactMessage> ContactMessages { get; set; }

        public RotageekContext(DbContextOptions<RotageekContext> options)
            : base(options)
        { }
    }
}
