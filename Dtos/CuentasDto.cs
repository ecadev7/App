using System.ComponentModel.DataAnnotations;

namespace App.Dtos
{
    public class CuentaDto
    {
        public int Id { get; set; }
        public int NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public int ClienteId { get; set; }
    }

    public record CreateCuentaDto
    {
        [Required]
        public int NumeroCuenta { get; set; }
        [Required]
        public string TipoCuenta { get; set; }
        [Required]
        public decimal SaldoInicial { get; set; }
        [Required]
        public bool Estado { get; set; }
        [Required]
        public int ClienteId { get; set; }
    }

    public record UpdateCuentaDto
    {
        public int Id { get; set; }
        [Required]
        public int NumeroCuenta { get; set; }
        [Required]
        public string TipoCuenta { get; set; }
        [Required]
        public bool Estado { get; set; }
        [Required]
        public int ClienteId { get; set; }
    }

  

}