using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Microservicio1.Models
{
    public class Cliente:Persona
    {
       

        [Required]
        public string? Contrasenia { get; set; }

        [Required]
        public bool Estado { get; set; }

       

    }
}
