using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntrianRS.Models
{
    public class ArtisDbContext : DbContext
    {
        public ArtisDbContext(DbContextOptions<ArtisDbContext> options) : base(options)
        {

        }

        public DbSet<Artist> Artists { get; set; }
    }
}
