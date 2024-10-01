using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microservicio2.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microservicio2.Database
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            try
            {

                var rdbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

                if (rdbCreator != null)
                {
                    if (!rdbCreator.CanConnect())
                    {
                        rdbCreator.Create();


                    }



                    rdbCreator.CreateTables();




                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }


      

    }
}
