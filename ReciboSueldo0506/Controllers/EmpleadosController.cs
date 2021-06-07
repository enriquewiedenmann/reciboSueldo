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
    public class EmpleadosController : Controller
    {
        private ReciboSueldoContext db = new ReciboSueldoContext();

        // GET: Empleados
        public ActionResult Index()
        {
            try
            {
                string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                var empleados = db.Empleados.Include(e => e.Estado);
            return View(empleados.ToList());
         }catch{return RedirectToAction("../Home/Index");
    }
}

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
            }catch{return RedirectToAction("../Home/Index");}
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            try
            {
                string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                ViewBag.IdEstado = new SelectList(db.EstadoEmpelados, "IdEstadoEmpleado", "DescEstadoEmpleado");
            return View();
            }catch{return RedirectToAction("../Home/Index");}
        }

        // POST: Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEmpleado,Nombre,Apellido,Cuil,Mail,Celular,IdEstado")] Empleado empleado)
        {
            try
            {
                string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                if (ModelState.IsValid)
            {
                db.Empleados.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEstado = new SelectList(db.EstadoEmpelados, "IdEstadoEmpleado", "DescEstadoEmpleado", empleado.IdEstado);
            return View(empleado);
            }catch{return RedirectToAction("../Home/Index");}
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEstado = new SelectList(db.EstadoEmpelados, "IdEstadoEmpleado", "DescEstadoEmpleado", empleado.IdEstado);
            return View(empleado);
            }catch{return RedirectToAction("../Home/Index");}
        }

        // POST: Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEmpleado,Nombre,Apellido,Cuil,Mail,Celular,IdEstado")] Empleado empleado)
        {
            try
            {
                string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEstado = new SelectList(db.EstadoEmpelados, "IdEstadoEmpleado", "DescEstadoEmpleado", empleado.IdEstado);
            return View(empleado);
            }catch{return RedirectToAction("../Home/Index");}
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

        if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
            }catch{return RedirectToAction("../Home/Index");}
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

        Empleado empleado = db.Empleados.Find(id);
            db.Empleados.Remove(empleado);
            db.SaveChanges();
            return RedirectToAction("Index");
            }catch{return RedirectToAction("../Home/Index");}
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
