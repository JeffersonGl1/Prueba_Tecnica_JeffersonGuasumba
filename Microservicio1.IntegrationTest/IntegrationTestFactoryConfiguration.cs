using Microservicio1.Database;
using Microservicio1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Json;

namespace Microservicio1.IntegrationTest
{
    internal class IntegrationTestFactoryConfiguration: WebApplicationFactory<Program>
    {


        override protected void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                // Remove the existing DbContextOptions
                services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

                // Register a new DBContext that will use our test connection string
                string? connString = GetConnectionString();
                services.AddSqlServer<ApplicationDbContext  >(connString);             

            });
        }

        private static string? GetConnectionString()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<IntegrationTestFactoryConfiguration>()
                .Build();

            var connString = configuration.GetConnectionString("MatchMaker");
            return connString;
        }





    }
}