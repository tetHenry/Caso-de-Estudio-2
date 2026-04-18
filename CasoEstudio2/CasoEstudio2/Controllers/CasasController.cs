using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasoEstudio2.EntityFramework;

namespace CasoEstudio2.Controllers
{
    public class CasasController : Controller
    {
        // Instancia a la base de datos
        CasoEstudioKNEntities bd = new CasoEstudioKNEntities();
        [HttpGet]
        public ActionResult Consulta()
        {
            // Filtramos las casas que cuestan entre 115.000 y 180.000
            var casas = bd.CasasSistema
                .Where(c => c.PrecioCasa >= 115000 && c.PrecioCasa <= 180000)
                .ToList();
            // Ordenamos el resultado por estado: disponibles primero
            // Evaluamos si el string 'UsuarioAlquiler' es nulo o vacío
            var casasOrdenadas = casas
                .OrderBy(c => string.IsNullOrEmpty(c.UsuarioAlquiler) ? 0 : 1)
                .ToList();
            return View(casasOrdenadas);
        }
    }
}