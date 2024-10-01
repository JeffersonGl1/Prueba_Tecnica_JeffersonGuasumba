using Microservicio2.Database;
using Microservicio2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microservicio2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {


        private readonly AppDBContextReport _context;
        public ReportesController(AppDBContextReport context)
        {

            _context = context;


        }


        // GET: api/<ReportesController>
        [HttpGet("{identificacion}/{fechaInicio}/{fechaFinal}")]
        public async Task<IActionResult> Get(string identificacion, DateTime fechaInicio, DateTime fechaFinal)
        {
            if (fechaInicio > fechaFinal)
            {

                return BadRequest("La fecha inicial no puede ser mayor a la fecha final");

            }

          
            


            var listReporte = new List<ReporteF>();


            var reporte = await _context.Reportes
                    .FromSqlRaw("EXEC SP_ReporteFechasUsuario @Identificacion = {0}, @FechaInicial = {1}, @FechaFinal = {2} ", identificacion,fechaInicio, fechaFinal)
                    .ToListAsync();

            if (reporte.Count == 0)
            {

                return BadRequest("No existen Datos!!");

            }

            foreach (Reportes repo in reporte)
            {

                string tipoCuenta = "Ahorros";
                string estadoCuenta = "Activo";
                string tipoMovimiento = "Deposito";


                if (repo.TipoCuenta == 1)
                {
                    tipoCuenta = "Corriente";

                }

                if (repo.Estado == true)
                {
                    estadoCuenta = "Inactivo";
                }

                if (repo.TipoMovimiento == 1)
                {
                    tipoMovimiento = "Retiro";
                }


                listReporte.Add(new ReporteF
                {
                    FechaMovimiento = repo.FechaMovimiento,
                    Nombre = repo.Nombre,
                    NumeroCuenta = repo.NumeroCuenta,
                    TipoCuenta = tipoCuenta,
                    Estado = estadoCuenta,
                    TipoMovimiento = tipoMovimiento,
                    DescMovimiento = repo.DescMovimiento,
                    ValorMovimiento = repo.ValorMovimiento,
                    Saldo = repo.Saldo
                });

            }

            var jsonString = JsonConvert.SerializeObject(listReporte, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });

            return Ok(jsonString);
        }

        // GET api/<ReportesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ReportesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ReportesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReportesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        public class ReporteF
        {

            public DateTime FechaMovimiento { get; set; }
            public string? Nombre { get; set; }
            public long NumeroCuenta { get; set; }
            public string? TipoCuenta { get; set; }
            public string? Estado { get; set; }
            public string? TipoMovimiento { get; set; }
            public string? DescMovimiento { get; set; }
            public double ValorMovimiento { get; set; }
            public double Saldo { get; set; }




        }

    }
}
