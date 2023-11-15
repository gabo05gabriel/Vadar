using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vadar.Models;
using System.Configuration;
using Rotativa;
using Rotativa.Options;

namespace Vadar.Controllers
{
    [Authorize(Roles = "Administrador, Jefa, Empleado")]
    public class pasajesController : Controller
    {
        private oficinaModels db = new oficinaModels();

        // GET: pasajes
        public ActionResult Index()
        {
            return View(db.pasajes.ToList());
        }

        [Authorize(Roles = "Administrador, Jefa")]

        // GET: pasajes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pasajes pasajes = db.pasajes.Find(id);
            if (pasajes == null)
            {
                return HttpNotFound();
            }
            return View(pasajes);
        }

        // GET: pasajes/Create
        public ActionResult Create()
        {
            // Obtener los nombres de la tabla Mensajeros
            var mensajerosNombres = db.Mensajeros.Select(m => m.Nombre).ToList();

            // Pasar los nombres a la vista
            ViewBag.MensajerosNombres = mensajerosNombres;

            return View();
        }


        // POST: pasajes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,zona_de_ruta,fecha,sobres_entregados,sobres_recogidos,monto,descripcion")] pasajes pasajes, string NombreSeleccionado)
        {
            if (ModelState.IsValid)
            {
                // Asigna el nombre seleccionado al modelo
                pasajes.nombre = NombreSeleccionado;

                db.pasajes.Add(pasajes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // En caso de un modelo no válido, vuelve a mostrar la vista con el mensaje de error
            return View(pasajes);
        }


        [Authorize(Roles = "Jefa,Administrador")]
        // GET: pasajes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pasajes pasajes = db.pasajes.Find(id);
            if (pasajes == null)
            {
                return HttpNotFound();
            }
            return View(pasajes);
        }

        [Authorize(Roles = "Jefa,Administrador")]
        // POST: pasajes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,zona_de_ruta,fecha,sobres_entregados,sobres_recogidos,monto,descripcion")] pasajes pasajes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pasajes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pasajes);
        }

        [Authorize(Roles = "Administrador, Jefa")]
        // GET: pasajes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pasajes pasajes = db.pasajes.Find(id);
            if (pasajes == null)
            {
                return HttpNotFound();
            }
            return View(pasajes);
        }

        [Authorize(Roles = "Jefa,Administrador")]
        // POST: pasajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pasajes pasajes = db.pasajes.Find(id);
            db.pasajes.Remove(pasajes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrador, Jefa, Empleado")]
        // GET: pasajes/Search
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Jefa, Empleado")]
        public ActionResult Search(string nombre, DateTime fechaInicio, DateTime fechaFin)
        {
            // Llamar al procedimiento almacenado y obtener los resultados
            var results = db.Database.SqlQuery<pasajes>("exec sp_GetPasajesByNombreAndFecha @Nombre, @FechaInicio, @FechaFin",
                new SqlParameter("Nombre", nombre),
                new SqlParameter("FechaInicio", fechaInicio),
                new SqlParameter("FechaFin", fechaFin)).ToList();

            // Renderiza la vista Index como una vista parcial para PDF
            var pdfResult = new ViewAsPdf("VerPasajes", results)
            {
                FileName = "resultados_busqueda.pdf", // Nombre del archivo PDF
                PageSize = Size.A4,
                PageMargins = { Left = 10, Right = 10, Top = 10, Bottom = 10 },
            };

            return pdfResult;
        }


        [Authorize(Roles = "Administrador, Jefa")]
        public ActionResult VerPasajes()
        {
            // Aquí puedes obtener la lista de pasajes que deseas mostrar en la vista VerPasajes.
            var pasajesList = db.pasajes.ToList();

            return View("VerPasajes", pasajesList);
        }

    }
}
