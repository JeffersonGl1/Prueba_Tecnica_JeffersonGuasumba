using Microservicio2.Database;
using Microservicio2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microservicio2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public CuentaController(ApplicationDbContext context)
        {

            _context = context;


        }


        // GET: api/<CuentaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _context.Cuentas.ToListAsync();

            if (results == null)
            {
                return NotFound("No se encontraron resultados");
            }

            return Ok(results);
        }

        // GET api/<CuentaController>/5
        [HttpGet("{cuenta}")]
        public async Task<IActionResult> Get(long cuenta)
        {


            var result = await _context.Cuentas.SingleOrDefaultAsync(x => x.NumeroCuenta == cuenta);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/<CuentaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] RCuenta rCuenta)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            using var client = new HttpClient(handler);

            var response = await client.GetAsync("https://microservicio1:5001/api/Clientes/" + rCuenta.CedulaCliente?.Trim());

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound("El cliente no se encuentra registrado");
            }

            var jsonO = JObject.Parse(await response.Content.ReadAsStringAsync());

            var idc = Convert.ToInt32(jsonO["idCliente"]);

            // var dr = JsonConvert.DeserializeObject <DataRow>(jsonO.ToString());



            try
            {
                var result = await _context.Cuentas.SingleOrDefaultAsync(x => x.NumeroCuenta == rCuenta.NumeroCuenta);

                if (result == null)
                {

                    if (rCuenta.SaldoInicial < 0) { return BadRequest("El valor inicial no puede ser menor a 0"); };


                    var cuenta = new Cuenta
                    {
                        IdCliente = idc,
                        Estado = true,
                        SaldoInicial = rCuenta.SaldoInicial,
                        NumeroCuenta = rCuenta.NumeroCuenta,
                        TipoCuenta = rCuenta.TipoCuenta
                    };

                    var movimiento = new Movimiento {

                        NumeroCuenta = rCuenta.NumeroCuenta,
                        TipoMovimiento = 0,
                        DescMovimiento = "Despósito de $" + rCuenta.SaldoInicial,
                        FechaMovimiento = DateTime.Now,
                        Saldo = rCuenta.SaldoInicial,
                        ValorMovimiento = rCuenta.SaldoInicial


                    };




                    await _context.Cuentas.AddAsync(cuenta);
                    await _context.Movimientos.AddAsync(movimiento);
                    await _context.SaveChangesAsync();
                    return Ok(cuenta);
                }


                return BadRequest("Esta cuenta ya se encuentra registrada");



            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }





        }

        // PUT api/<CuentaController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] MCuenta mcuenta)
        {


            try
            {
                var cuentaI = await _context.Cuentas.SingleOrDefaultAsync(x => x.NumeroCuenta == mcuenta.NumeroCuenta);

                if (cuentaI == null)
                {
                    return NotFound("No se encontró la cuenta especificada");

                }



                cuentaI.NumeroCuenta = mcuenta.NumeroCuenta;
                cuentaI.TipoCuenta = mcuenta.TipoCuenta;
                cuentaI.SaldoInicial = mcuenta.SaldoInicial;
                cuentaI.Estado = mcuenta.Estado;

                

                var movimientol = await _context.Movimientos.ToListAsync();

                var movCuenta = movimientol.FindAll(x => x.NumeroCuenta == mcuenta.NumeroCuenta);



                if (movCuenta != null && movCuenta.Count == 1)
                {


                    var movInic = movCuenta[0];

                    if (movInic != null)
                    {

                        movInic.ValorMovimiento = mcuenta.SaldoInicial;

                        movInic.Saldo = mcuenta.SaldoInicial;

                        _context.Attach(movInic);
                    }
                }
                else { 
                
                return BadRequest("La cuenta ya tiene mas de un movimiento asociado!!");

                }





                _context.Attach(cuentaI);
                await _context.SaveChangesAsync();

                return Ok(cuentaI);


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }









        }

        // DELETE api/<CuentaController>/5
        [HttpDelete("{cuenta}")]
        public async Task<IActionResult> Delete(long cuenta)
        {

            try
            {
                var cuentaI = await _context.Cuentas.SingleOrDefaultAsync(x => x.NumeroCuenta == cuenta);

                if (cuentaI == null)
                {
                    return NotFound("No se encontro la cuenta especificada");

                }


                var movimientosI = await _context.Movimientos.ToListAsync();


                movimientosI = movimientosI.FindAll(x => x.NumeroCuenta == cuenta);

                if (movimientosI != null && movimientosI.Count > 0)
                {

                    foreach (var movimiento in movimientosI)
                    {

                        _context.Remove(movimiento);

                    }

                }


                _context.Remove(cuentaI);




                await _context.SaveChangesAsync();

                return Ok("La cuenta y los movimientos asociados se han eliminado!!");


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }





        }


        public class RCuenta
        {

            [Required]
            public string?  CedulaCliente { get; set; }

            [Required]
            public long NumeroCuenta { get; set; }

            [Required]
            public int TipoCuenta { get; set; }

            [Required]
            public double SaldoInicial { get; set; }

            [Required]
            public bool Estado { get; set; }

        }


        public class MCuenta
        {

            

            [Required]
            public long NumeroCuenta { get; set; }

            [Required]
            public int TipoCuenta { get; set; }

            [Required]
            public double SaldoInicial { get; set; }

            [Required]
            public bool Estado { get; set; }




        }


    }

}
