using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CasoEstudio2.Models
{
    public class CasasModel
    {
        public long IdCasa { get; set; }
        public string DescripcionCasa { get; set; }
        public decimal PrecioCasa { get; set; }
        public string UsuarioAlquiler { get; set; }
        public DateTime? FechaAlquiler { get; set; }

        //mostrar estado y fecha
        public string Estado
        {
            get { return string.IsNullOrEmpty(UsuarioAlquiler) ? "Disponible" : "Reservada"; }
        }

        public string FechaFormateada
        {
            get { return FechaAlquiler.HasValue ? FechaAlquiler.Value.ToString("dd/MM/yyyy") : ""; }
        }
    }

    public class AlquilerModel
    {
        public long IdCasa { get; set; }
        public decimal PrecioCasa { get; set; }

        [Required(ErrorMessage = "El usuario es requerido")]
        public string UsuarioAlquiler { get; set; }
    }
}