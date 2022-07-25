using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace App.Dtos
{
    public class ReporteDto
    {
        [JsonPropertyName("Fecha")]
        public DateTime Fecha { get; set; }
        [JsonPropertyName("Cliente")]
        public string Cliente { get; set; }
        [JsonPropertyName("Numero Cuenta")]
        public int NumeroCuenta { get; set; }
        [JsonPropertyName("Tipo")]
        public string TipoCuenta { get; set; }
        [JsonPropertyName("Saldo Inicial")]
        public decimal SaldoInicial { get; set; }
        [JsonPropertyName("Estado")]
        public bool Estado { get; set; }
        [JsonPropertyName("Movimiento")]
        public decimal Movimiento { get; set; }
        [JsonPropertyName("Saldo Disponible")]
        public decimal SaldoDisponible { get; set; }
    }

}