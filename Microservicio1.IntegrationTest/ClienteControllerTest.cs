using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservicio1.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microservicio1.Models;
using System.Net.Http.Json;

namespace Microservicio1.IntegrationTest
{
    public class ClienteControllerTest
    {




        [Fact]
        public async Task GetCliente_RetornaOkResult()
        {
            // Arrange
            var webAppFactory = new IntegrationTestFactoryConfiguration();


            var _httpclient = webAppFactory.CreateClient();

            var cliente = new Cliente
            {
                IdCliente = 10,
                Nombre = "Jefferson Guasumba",
                Direccion = "Chillogallo",
                Estado = true,
                Contrasenia = "12345*",
                Identificacion = "1724294129",
                Edad = 28,
                Genero = "Masculino",
                Telefono = "0983875112"
            };

            // Act
            var response = await _httpclient.PostAsJsonAsync("api/Clientes/", cliente);
            response.EnsureSuccessStatusCode();

            var getResponse = await _httpclient.GetAsync("api/Clientes/1724294129");

            getResponse.EnsureSuccessStatusCode();
            var returnCliente = await getResponse.Content.ReadFromJsonAsync<Cliente>();

            //Assert
            Assert.Equal("Jefferson Guasumba", returnCliente.Nombre);

        }

    }
}
