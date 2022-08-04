using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSA.backend.Model;


namespace MSA.backend.Data
{
    public class WebAPIDBContext : DbContext, IWebAPIDBContext
    {
        public WebAPIDBContext(DbContextOptions<WebAPIDBContext> options) : base(options) {
            Database.EnsureCreated();        
        }

        public DbSet<pokemon> pokemons { get; set; }
       

    }
}
