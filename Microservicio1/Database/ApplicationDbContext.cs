using Microservicio1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;


namespace Microservicio1.Database
{
    public class ApplicationDbContext: DbContext
    {
      
       public DbSet<Cliente> Clientes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            try {

                var rdbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator; 

                if (rdbCreator != null )
                {
                    if (!rdbCreator.CanConnect())
                    {
                        rdbCreator.Create();


                    }

                    
                        rdbCreator.CreateTables();

                    

                }

            } catch (Exception ex) {
            
                Console.WriteLine(ex.Message);
            }
        }

    }
}
