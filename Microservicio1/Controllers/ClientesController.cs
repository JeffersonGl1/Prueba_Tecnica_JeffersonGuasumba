using Microservicio1.Database;
using Microservicio1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microservicio1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public ClientesController(ApplicationDbContext context)
        {

            _context = context;


        }

        // GET: api/<ClienteController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _context.Clientes.ToListAsync();

            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // GET api/<ClienteController>/5
        [HttpGet("{cedula}")]
        public async Task<IActionResult> Get(string cedula)
        {
            var result = await _context.Clientes.SingleOrDefaultAsync(x => x.Identificacion == cedula.Trim());

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/<ClienteController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Cliente cliente)
        {
            try
            {
                var result = await _context.Clientes.SingleOrDefaultAsync(x => x.Identificacion == cliente.Identificacion.Trim());

                if (result == null)
                {
                    await _context.Clientes.AddAsync(cliente);
                    await _context.SaveChangesAsync();
                    return Ok(cliente);
                }


                return BadRequest("Esta cédula ya se encuentra registrada");



            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }

            
            

        }

        // PUT api/<ClienteController>/5
        [HttpPut("{cedula}")]
        public async Task<IActionResult> Put(string cedula, [FromForm] Cliente cliente)
        {

            try
            {
                var clienteI = await _context.Clientes.SingleOrDefaultAsync(x => x.Identificacion == cedula.Trim());

                if (clienteI == null)
                {
                    return NotFound("No se encontró el cliente");

                }

                clienteI.Nombre = cliente.Nombre;
                clienteI.Telefono = cliente.Telefono;
                clienteI.Edad = cliente.Edad;
                clienteI.Direccion = cliente.Direccion;
                clienteI.Estado = cliente.Estado;
                clienteI.Contrasenia = cliente.Contrasenia;
                clienteI.Genero = cliente.Genero;
                _context.Attach(clienteI);
                await _context.SaveChangesAsync();

                return Ok(clienteI);


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }

            
           

        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{cedula}")]
        public async Task<IActionResult> Delete(string cedula)
        {

            try
            {
                var clienteI = await _context.Clientes.SingleOrDefaultAsync(x => x.Identificacion == cedula.Trim());

                if (clienteI == null)
                {
                    return NotFound();

                }

                _context.Remove(clienteI);
                await _context.SaveChangesAsync();

                return Ok("Cliente Eliminado: " + clienteI.Nombre);


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }


        }
    }   
}
