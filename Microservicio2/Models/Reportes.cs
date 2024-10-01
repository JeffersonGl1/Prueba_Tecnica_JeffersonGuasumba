using Microservicio2.Database;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Microservicio2.Models
{
    public class Reportes
    {

        [Key]
        public int IdCliente { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string? Nombre { get; set; }
        public long NumeroCuenta {  get; set; } 
        public int TipoCuenta { get; set; }
        public bool Estado {  get; set; }
        public int TipoMovimiento { get; set; }
        public string? DescMovimiento { get; set; }
        public double ValorMovimiento {get; set; }
        public double Saldo {  get; set; }


       



    }
}
