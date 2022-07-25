using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.VSCode.Data
{
    public class HotelListingDbContext : DbContext
    {
        public HotelListingDbContext(DbContextOptions options) : base(options)
        {
             
        }


        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        
    }
}