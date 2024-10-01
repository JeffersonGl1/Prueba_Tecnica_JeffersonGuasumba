using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservicio1.Models
{
    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCliente { get; set; }

        [Required]
        public string? Nombre { get; set; }

        [Required]
        public string? Genero { get; set; }

        [Required]
        public int Edad {  get; set; }

        [Required]
        public string? Identificacion { get; set; }

        [Required]
        public string? Direccion { get; set; }

        [Required]
        public string? Telefono {  get; set; }  

        


    }
}
