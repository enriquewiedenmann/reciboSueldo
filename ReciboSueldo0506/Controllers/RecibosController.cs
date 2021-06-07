using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using ReciboSueldo0506.DAO;
using ReciboSueldo0506.Models;

namespace ReciboSueldo0506.Controllers
{
    public class RecibosController : Controller
    {
        private ReciboSueldoContext db = new ReciboSueldoContext();

        // GET: Recibos
        public ActionResult Index()
        {
            try
            { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();


                var recibos = db.Recibos.Include(r => r.Empleado).Include(r => r.Lote);
            return View(recibos.ToList());
            }
            catch { return RedirectToAction("../Home/Index"); }

        }

        // GET: Recibos/Details/5
        public ActionResult Details(int? id)
        {
                try
                { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();


                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recibos = db.Recibos.Include(r => r.Empleado).Include(r => r.Lote);
            Recibo recibo = (from rr in recibos where rr.IdRecibo == id select rr).FirstOrDefault();
         

          
            ViewData["urlFile"] = recibo.CrearArchivo();
            if (recibo == null)
            {
                return HttpNotFound();
            }
            return View(recibo);
                }
                catch { return RedirectToAction("../Home/Index"); }
            }

        // GET: Recibos/Create
        public ActionResult Create()
        {
                    try
                    { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();


                        ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Apellido","Nombre");
            ViewBag.IdLote = new SelectList(db.Lotes, "IdLote","Mes");
           
            return View();
                    }
                    catch { return RedirectToAction("../Home/Index"); }
                }

        // POST: Recibos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRecibo,Archivo,IdEmpleado,IdLote")] Recibo recibo, HttpPostedFileBase ArchivoFile)
        {
            try
            { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();


                recibo.Archivo = new byte[ArchivoFile.ContentLength];
            ArchivoFile.InputStream.Read(recibo.Archivo, 0, ArchivoFile.ContentLength);
            if (ModelState.IsValid)
            {
               
                db.Recibos.Add(recibo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Nombre", recibo.IdEmpleado);
            ViewBag.IdLote = new SelectList(db.Lotes, "IdLote", "Mes", recibo.IdLote);
            return View(recibo);
            }
            catch { return RedirectToAction("../Home/Index"); }
        }

        // GET: Recibos/Edit/5
        public ActionResult Edit(int? id)
        {
                try
                { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();


                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recibo recibo = db.Recibos.Find(id);
            if (recibo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Nombre", recibo.IdEmpleado);
            ViewBag.IdLote = new SelectList(db.Lotes, "IdLote", "Mes", recibo.IdLote);
            ViewData["urlFile"] = recibo.CrearArchivo();
            return View(recibo);
                }
                catch { return RedirectToAction("../Home/Index"); }
            }

        // POST: Recibos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRecibo,Archivo,IdEmpleado,IdLote")] Recibo recibo)
        {
            try
            { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();


                if (ModelState.IsValid)
            {
                db.Entry(recibo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Nombre", recibo.IdEmpleado);
            ViewBag.IdLote = new SelectList(db.Lotes, "IdLote", "Mes", recibo.IdLote);
            return View(recibo);
            }
            catch { return RedirectToAction("../Home/Index"); }
        }

        // GET: Recibos/Delete/5
        public ActionResult Delete(int? id)
        {
                try
                { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();


                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recibos = db.Recibos.Include(r => r.Empleado).Include(r => r.Lote);
            Recibo recibo = (from rr in recibos where rr.IdRecibo == id select rr).FirstOrDefault();
             ViewData["urlFile"] = recibo.CrearArchivo();
            if (recibo == null)
            {
                return HttpNotFound();
            }
            return View(recibo);
                }
                catch { return RedirectToAction("../Home/Index"); }
            }

        // POST: Recibos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();


                Recibo recibo = db.Recibos.Find(id);
            db.Recibos.Remove(recibo);
            db.SaveChanges();
            return RedirectToAction("Index");
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
