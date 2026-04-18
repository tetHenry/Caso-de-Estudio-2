using CasoEstudio2.EntityFramework;
using CasoEstudio2.Models;
using System;
using System.Linq;
using System.Web.Mvc;


namespace CasoEstudio2.Controllers
{
    public class CasasController : Controller
    {
        // consulta casas
        public ActionResult Consulta()
        {
            using (var db = new CasoEstudioKNEntities())
            {
                var casas = (from c in db.CasasSistema
                             where c.PrecioCasa >= 115000 && c.PrecioCasa <= 180000
                             orderby c.UsuarioAlquiler
                             select new CasasModel
                             {
                                 IdCasa = c.IdCasa,
                                 DescripcionCasa = c.DescripcionCasa,
                                 PrecioCasa = c.PrecioCasa,
                                 UsuarioAlquiler = c.UsuarioAlquiler,
                                 FechaAlquiler = c.FechaAlquiler
                             }).ToList();

                return View(casas);
            }
        }

        // alquiler casas
        public ActionResult Alquiler()
        {
            using (var db = new CasoEstudioKNEntities())
            {
                var casasDisponibles = (from c in db.CasasSistema
                                        where c.UsuarioAlquiler == null
                                        select new
                                        {
                                            c.IdCasa,
                                            c.DescripcionCasa
                                        }).ToList();

                ViewBag.ListaCasas = new SelectList(casasDisponibles, "IdCasa", "DescripcionCasa");
            }

            return View(new AlquilerModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alquiler(AlquilerModel model)
        {
            if (!ModelState.IsValid)
            {
                using (var db = new CasoEstudioKNEntities())
                {
                    var casasDisponibles = (from c in db.CasasSistema
                                            where c.UsuarioAlquiler == null
                                            select new
                                            {
                                                c.IdCasa,
                                                c.DescripcionCasa
                                            }).ToList();

                    ViewBag.ListaCasas = new SelectList(casasDisponibles, "IdCasa", "DescripcionCasa");
                }
                return View(model);
            }

            using (var db = new CasoEstudioKNEntities())
            {
                var casa = (from c in db.CasasSistema
                            where c.IdCasa == model.IdCasa
                            select c).FirstOrDefault();

                if (casa != null)
                {
                    casa.UsuarioAlquiler = model.UsuarioAlquiler;
                    casa.FechaAlquiler = DateTime.Now;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Consulta");
        }

        // precio casas
        public JsonResult ObtenerPrecio(long id)
        {
            using (var db = new CasoEstudioKNEntities())
            {
                var precio = (from c in db.CasasSistema
                              where c.IdCasa == id
                              select c.PrecioCasa).FirstOrDefault();

                return Json(new { precio = precio }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}