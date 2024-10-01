using Microservicio1.Controllers;
using Microservicio1.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Microservicio1.Test
{
    public class UnitTest1
    {

        private ApplicationDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            return new ApplicationDbContext(options);
        }



        [Fact]
        public void GetCliente_DevuelveOk_CuandoNoExisteElCliente()
        {
            // Arrange

            var context = CreateDbContext();
            context.Clientes.Add(new Models.Cliente { IdCliente = 1, Nombre = "Jefferson Guasumba", Direccion = "Chillogallo", 
                Estado = true, Contrasenia = "12345*", Identificacion = "1724294127", Edad = 28, Genero = "Masculino", Telefono = "0983875112" });
            context.SaveChanges();

            //Act
            var controller = new ClientesController(context);

            var result = controller.Get("1724294127");

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnCustomer = Assert.IsType<Models.Cliente>(okResult.Value);
            Assert.Equal(1, returnCustomer.IdCliente);




        }


        [Fact]
        public void GetCliente_DevuelveNotFound_CuandoNoExisteElCliente()
        {

            //Arrange
            var context = CreateDbContext();
            var controller = new ClientesController(context);

           //Act
            var result = controller.Get("1724294128");

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);




        }
    }
}