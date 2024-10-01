using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Microservicio2.Models
{
    public class Movimiento
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMovimiento { get; set; }

        [Required]
        public DateTime FechaMovimiento { get; set; }

        [Required]
        public string? DescMovimiento { get; set; }

        [Required]
        public int TipoMovimiento { get; set; }

        [Required]
        public double ValorMovimiento { get; set; }

        [Required]
        public double Saldo { get; set; }

        [Required]
        public long NumeroCuenta { get; set; }



    }
}
