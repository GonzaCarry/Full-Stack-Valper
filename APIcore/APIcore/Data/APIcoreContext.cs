using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APIcore.Models;

namespace APIcore.Models
{
    public class APIcoreContext : DbContext
    {
        public APIcoreContext (DbContextOptions<APIcoreContext> options)
            : base(options)
        {
        }

        public DbSet<APIcore.Models.User> User { get; set; }

        //public DbSet<APIcore.Models.Function> Function { get; set; }
    }
}
