using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntrianRS.Models;

namespace AntrianRS.Models
{
    public class ArtisDBContext2 : DbContext
    {
        public DbSet<Artist> Artist2 { get; set; }
    }
}
