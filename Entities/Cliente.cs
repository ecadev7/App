using System.ComponentModel.DataAnnotations.Schema;

namespace App.Entities
{
    [Table("Clientes")]
    public class Cliente : Persona
    {
        //public int Id { get; set; }
        public string Contrasenia { get; set; }
        public bool Estado { get; set; }
    }
}