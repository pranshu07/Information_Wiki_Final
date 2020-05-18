using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Information_Wiki_Final.Models;

namespace Information_Wiki_Final.Models
{
    public class Information_Wiki_DataContext : DbContext
    {
        public Information_Wiki_DataContext (DbContextOptions<Information_Wiki_DataContext> options)
            : base(options)
        {
        }

        public DbSet<Information_Wiki_Final.Models.Author> Author { get; set; }

        public DbSet<Information_Wiki_Final.Models.Viewer> Viewer { get; set; }

        public DbSet<Information_Wiki_Final.Models.WikiPage> WikiPage { get; set; }
    }
}
