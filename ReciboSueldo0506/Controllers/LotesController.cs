using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReciboSueldo0506.DAO;
using ReciboSueldo0506.Models;

namespace ReciboSueldo0506.Controllers
{
    public class LotesController : Controller
    {
        private ReciboSueldoContext db = new ReciboSueldoContext();

        // GET: Lotes
        public ActionResult Index()
        {
            try
            { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                return View(db.Lotes.ToList());
            }
            catch { return RedirectToAction("../Home/Index"); }
        }

        // GET: Lotes/Details/5
        public ActionResult Details(int? id)
        {
                try
                { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var empleados = db.Empleados;
            var lotes = db.Lotes.Include(rr => rr.Recibos);
            Lote lote = (from ll in lotes where ll.IdLote == id select ll).FirstOrDefault();
            foreach(Recibo r in lote.Recibos)
            {
                r.Empleado = (from ee in empleados where r.IdEmpleado == ee.IdEmpleado select ee).FirstOrDefault();
            }

            if (lote == null)
            {
                return HttpNotFound();
            }
            ViewData["urlFile"] = lote.CrearArchivo();
            return View(lote);
                }
                catch { return RedirectToAction("../Home/Index"); }
            }

        // GET: Lotes/Create
        public ActionResult Create()
        {
                    try
                    { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();


                        return View();
                    }
                    catch { return RedirectToAction("../Home/Index"); }
                }

        // POST: Lotes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdLote,Mes,Anio,Concepto")] Lote lote, HttpPostedFileBase ArchivoFile)
        {
            try
            { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                lote.ArchivoLote = new byte[ArchivoFile.ContentLength];
            ArchivoFile.InputStream.Read(lote.ArchivoLote, 0, ArchivoFile.ContentLength);
            if (ModelState.IsValid)
            {
                lote.FecAlta = DateTime.Now;
                db.Lotes.Add(lote);
                db.SaveChanges();
                List<Empleado> e = db.Empleados.ToList();
                lote.GenerarRecibosLote(e);
                RedirectToAction("Details/" + lote.IdLote);
            }

            return View(lote);
            }
            catch { return RedirectToAction("../Home/Index"); }
        }





        // GET: Lotes/Edit/5
        public ActionResult Edit(int? id)
        {
                try
                { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lote lote = db.Lotes.Find(id);
            if (lote == null)
            {
                return HttpNotFound();
            }
            ViewData["urlFile"] = lote.CrearArchivo();
            return View(lote);
                }
                catch { return RedirectToAction("../Home/Index"); }
            }

        // POST: Lotes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdLote,Mes,Anio,Concepto,FecAlta")] Lote lote)
        {
            try
            { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                if (ModelState.IsValid)
            {
                db.Entry(lote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lote);
            }
            catch { return RedirectToAction("../Home/Index"); }
        }

        // GET: Lotes/Delete/5
        public ActionResult Delete(int? id)
        {
                try
                { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lote lote = db.Lotes.Find(id);
            if (lote == null)
            {
                return HttpNotFound();
            }
            ViewData["urlFile"] = lote.CrearArchivo();
            return View(lote);
                }
                catch { return RedirectToAction("../Home/Index"); }
            }

        // POST: Lotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                Lote lote = db.Lotes.Find(id);
            db.Lotes.Remove(lote);
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            catch { return RedirectToAction("../Home/Index"); }
        }


        public ActionResult EnviarLote(int? id)
        {
                try
                { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var empleados = db.Empleados;
            var lotes = db.Lotes.Include(rr => rr.Recibos);
            Lote lote = (from ll in lotes where ll.IdLote == id select ll).FirstOrDefault();
            foreach (Recibo r in lote.Recibos)
            {
                r.Empleado = (from ee in empleados where r.IdEmpleado == ee.IdEmpleado select ee).FirstOrDefault();
            }

            if (lote == null)
            {
                return HttpNotFound();
            }
            lote.EnviarLote();
            ViewData["urlFile"] = lote.CrearArchivo();
            return View(lote);
                }
                catch { return RedirectToAction("../Home/Index"); }
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
