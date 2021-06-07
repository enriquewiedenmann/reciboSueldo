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
    public class EstadoEmpleadosController : Controller
    {
        private ReciboSueldoContext db = new ReciboSueldoContext();

        // GET: EstadoEmpleados
        public ActionResult Index()
        {
            try
            { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                return View(db.EstadoEmpelados.ToList());
            }
            catch { return RedirectToAction("../Home/Index"); }
        }

        // GET: EstadoEmpleados/Details/5
        public ActionResult Details(int? id)
        {
                try
                { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoEmpleado estadoEmpleado = db.EstadoEmpelados.Find(id);
            if (estadoEmpleado == null)
            {
                return HttpNotFound();
            }
            return View(estadoEmpleado);
                }
                catch { return RedirectToAction("../Home/Index"); }
            }

        // GET: EstadoEmpleados/Create
        public ActionResult Create()
        {
                    try
                    { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                        return View();
                    }
                    catch { return RedirectToAction("../Home/Index"); }
                }

        // POST: EstadoEmpleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEstadoEmpleado,DescEstadoEmpleado")] EstadoEmpleado estadoEmpleado)
        {
            try
            { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                if (ModelState.IsValid)
            {
                db.EstadoEmpelados.Add(estadoEmpleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadoEmpleado);
            }
            catch { return RedirectToAction("../Home/Index"); }
        }

        // GET: EstadoEmpleados/Edit/5
        public ActionResult Edit(int? id)
        {
                try
                { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoEmpleado estadoEmpleado = db.EstadoEmpelados.Find(id);
            if (estadoEmpleado == null)
            {
                return HttpNotFound();
            }
            return View(estadoEmpleado);
                }
                catch { return RedirectToAction("../Home/Index"); }
            }

        // POST: EstadoEmpleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEstadoEmpleado,DescEstadoEmpleado")] EstadoEmpleado estadoEmpleado)
        {
            try
            { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                if (ModelState.IsValid)
            {
                db.Entry(estadoEmpleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadoEmpleado);
            }
            catch { return RedirectToAction("../Home/Index"); }
        }

        // GET: EstadoEmpleados/Delete/5
        public ActionResult Delete(int? id)
        {
                try
                { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoEmpleado estadoEmpleado = db.EstadoEmpelados.Find(id);
            if (estadoEmpleado == null)
            {
                return HttpNotFound();
            }
            return View(estadoEmpleado);
                }
                catch { return RedirectToAction("../Home/Index"); }
            }

        // POST: EstadoEmpleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                EstadoEmpleado estadoEmpleado = db.EstadoEmpelados.Find(id);
            db.EstadoEmpelados.Remove(estadoEmpleado);
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
