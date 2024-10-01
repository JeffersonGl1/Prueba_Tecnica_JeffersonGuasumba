using Microservicio2.Database;
using Microservicio2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microservicio2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public MovimientoController(ApplicationDbContext context)
        {

            _context = context;


        }


        // GET: api/<MovimientoController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _context.Movimientos.ToListAsync();

            if (results == null)
            {
                return NotFound("No se encontraron resultados");
            }

            return Ok(results);
        }

        // GET api/<MovimientoController>/5
        [HttpGet("{cuenta}")]
        public async Task<IActionResult> Get(long cuenta)
        {
            var movimientosList = await _context.Movimientos.ToListAsync();

            var movimientosCuenta = movimientosList.FindAll(x=> x.NumeroCuenta == cuenta);

            if (movimientosCuenta == null|| movimientosCuenta.Count == 0)
            {
                return NotFound("No se encontraron resultados");
            }

            return Ok(movimientosCuenta);
        }

        // POST api/<MovimientoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] RMovimiento rMovimiento)
        {

            var cuentaI = await _context.Cuentas.SingleOrDefaultAsync(x => x.NumeroCuenta == rMovimiento.NumeroCuenta);

            var movimientos = await _context.Movimientos.ToListAsync();

            var descMovimiento = "Depósito de $";

            int tipoMovimiento = 0;

            if (cuentaI == null)
            {

                return NotFound("No se encontró la ceunta especificada");

            }

            


            if (rMovimiento.ValorMovimiento == 0)
            {

                return NotFound("El valor del movimiento no puede ser 0");

            }

            var ultmovimiento = movimientos.OrderByDescending(x => x.FechaMovimiento).First();

         

            if (rMovimiento.ValorMovimiento < 0 )
            {
                if (ultmovimiento.ValorMovimiento == 0 || ultmovimiento.Saldo <= rMovimiento.ValorMovimiento*(-1))
                {
                    return BadRequest("Saldo no disponible");
                }

                descMovimiento = "Retiro de $";

                tipoMovimiento = 1;

            }



            var movimiento = new Movimiento
            {
                FechaMovimiento = DateTime.Now,
                DescMovimiento = descMovimiento + rMovimiento.ValorMovimiento,
                ValorMovimiento = rMovimiento.ValorMovimiento,
                TipoMovimiento = tipoMovimiento,
                Saldo = ultmovimiento.Saldo + rMovimiento.ValorMovimiento,
                NumeroCuenta = rMovimiento.NumeroCuenta

            };



            
            await _context.Movimientos.AddAsync(movimiento);
            await _context.SaveChangesAsync();
            return Ok(movimiento);



        }

        // PUT api/<MovimientoController>/5
        [HttpPut("{idMovimiento}")]
        public async Task<IActionResult> Put(int idMovimiento, [FromForm] Movimiento movimiento)
        {
            try
            {
                var movimientoI = await _context.Movimientos.SingleOrDefaultAsync(x => x.IdMovimiento == idMovimiento);

                if (movimientoI == null)
                {
                    return NotFound("No se encontró el movimiento");

                }

                movimientoI.FechaMovimiento = movimiento.FechaMovimiento;
                movimientoI.DescMovimiento = movimiento.DescMovimiento;
                movimientoI.TipoMovimiento = movimiento.TipoMovimiento;
                movimientoI.NumeroCuenta = movimiento.NumeroCuenta;
                movimientoI.ValorMovimiento = movimiento.ValorMovimiento;
                movimientoI.Saldo = movimiento.Saldo;
                
                _context.Attach(movimientoI);
                await _context.SaveChangesAsync();

                return Ok(movimientoI);


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }


        }

        // DELETE api/<MovimientoController>/5
        [HttpDelete("{idMovimiento}")]
        public async Task<IActionResult> Delete(int idMovimiento)
        {

            try
            {
                var movimientoI = await _context.Movimientos.SingleOrDefaultAsync(x => x.IdMovimiento == idMovimiento);

                if (movimientoI == null)
                {
                    return NotFound("No se encontro el movimiento");

                }

                _context.Remove(movimientoI);
                await _context.SaveChangesAsync();

                return Ok("Movimiento Eliminado: " + movimientoI.DescMovimiento);


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }

        }



        public class RMovimiento {

            [Required]
            public double ValorMovimiento { get; set; }

            [Required]
            public long NumeroCuenta { get; set; }




        }


    }
}
