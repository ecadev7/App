using System.ComponentModel.DataAnnotations;

namespace App.Dtos
{
    public class MovimientoDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public bool Estado { get; set; }
        public int CuentaId { get; set; }
    }

    public record CreateMovimientoDto
    {
        [Required]
        public string TipoMovimiento { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public int CuentaId { get; set; }
        public DateTime Fecha { get; set; }
    }

    public record UpdateMovimientoDto
    {
        public int Id { get; set; }
        [Required]
        public string TipoMovimiento { get; set; }
        [Required]
        public decimal Valor { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
    }

}