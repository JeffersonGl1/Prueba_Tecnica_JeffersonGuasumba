using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservicio2.Models
{
    public class Cuenta
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCuenta { get; set; }

        public int IdCliente { get; set; }

      
        public long NumeroCuenta { get; set; }

        
        public int TipoCuenta { get; set; }

      
        public double SaldoInicial { get; set; }

       
        public bool Estado { get; set; }

        
    }
}
