using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vadar.Models;

namespace Vadar.Controllers
{
    [Authorize(Roles = "Administrador, Jefa")]

    public class MensajerosController : Controller
    {
        private oficinaModels db = new oficinaModels();

        // GET: Mensajeros
        public ActionResult Index()
        {
            return View(db.Mensajeros.ToList());
        }

        // GET: Mensajeros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensajeros mensajeros = db.Mensajeros.Find(id);
            if (mensajeros == null)
            {
                return HttpNotFound();
            }
            return View(mensajeros);
        }

        public ActionResult Create()
        {
            var emails = db.AspNetUsers.Select(u => u.Email).ToList();
            ViewBag.Emails = new SelectList(emails);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Email")] Mensajeros mensajero, string selectedEmail)
        {
            if (ModelState.IsValid)
            {
                mensajero.Email = selectedEmail;

                db.Mensajeros.Add(mensajero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var emails = db.AspNetUsers.Select(u => u.Email).ToList();
            ViewBag.Emails = new SelectList(emails, selectedEmail);
            return View(mensajero);
        }

        // GET: Mensajeros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensajeros mensajeros = db.Mensajeros.Find(id);
            if (mensajeros == null)
            {
                return HttpNotFound();
            }
            return View(mensajeros);
        }

        // POST: Mensajeros/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Email")] Mensajeros mensajeros)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mensajeros).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mensajeros);
        }

        // GET: Mensajeros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensajeros mensajeros = db.Mensajeros.Find(id);
            if (mensajeros == null)
            {
                return HttpNotFound();
            }
            return View(mensajeros);
        }

        // POST: Mensajeros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mensajeros mensajeros = db.Mensajeros.Find(id);
            db.Mensajeros.Remove(mensajeros);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
