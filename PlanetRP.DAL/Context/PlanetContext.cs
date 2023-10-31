using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PlanetRP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.DAL.Context
{
    public class PlanetContext : DbContext
    {
        //static PlanetContext()
        //{
        //    Database.
        //}
        public DbSet<AccountModel> Accounts { get; set; }

        public PlanetContext(DbContextOptions<PlanetContext> options) : base(options)
        {
            
        }

        
    }
}
