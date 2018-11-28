using GSA.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSA.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {

        }
          
        public DbSet<Capital> Capitals{ get; set; }

        public DbSet<PNL> PNLs { get; set; }

        public DbSet<Strategy> Strategies { get; set; }
         

    }
}
