using Microservicio2.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Microservicio2.Database
{
    public class AppDBContextReport:DbContext
    {

        public DbSet<Reportes> Reportes { get; set; } 

       
        public AppDBContextReport(DbContextOptions<AppDBContextReport> options) : base(options) { }

     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

    }
}
