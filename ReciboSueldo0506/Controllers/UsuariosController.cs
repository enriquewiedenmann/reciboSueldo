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
    public class UsuariosController : Controller
    {
        private ReciboSueldoContext db = new ReciboSueldoContext();

        // GET: Usuarios
        public ActionResult Index()
        {
            try
            { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();


                var usuarios = db.Usuarios.Include(u => u.Empleado);
            return View(usuarios.ToList());
            }
            catch { return RedirectToAction("../Home/Index"); }
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
                try
                { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();


                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
                }
                catch { return RedirectToAction("../Home/Index"); }
            }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
                    try
                    { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();


                        ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Nombre");
            return View();
                    }
                    catch { return RedirectToAction("../Home/Index"); }
                }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUsuario,IdEmpleado,UserName")] Usuario usuario)
        {
            try
            { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();


                if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Nombre", usuario.IdEmpleado);
            return View(usuario);
            }
            catch { return RedirectToAction("../Home/Index"); }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {

                try
                { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();


                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Nombre", usuario.IdEmpleado);
            return View(usuario);
                }
                catch { return RedirectToAction("../Home/Index"); }
            }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUsuario,IdEmpleado,UserName")] Usuario usuario)
        {
            try
            { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();


                if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Nombre", usuario.IdEmpleado);
            return View(usuario);
            }
            catch { return RedirectToAction("../Home/Index"); }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
                try
                { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();


                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
                }
                catch { return RedirectToAction("../Home/Index"); }
            }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();


                Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
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


        public string SendPass(string userName)
        {
            var usuarios = db.Usuarios.Include(u => u.Empleado);
            Usuario uu = (from us in usuarios where us.UserName.Equals(userName) select us).FirstOrDefault();
            if(uu != null)
            {
                string hash  = uu.sendPass();
                System.Web.HttpContext.Current.Session[userName] = hash;
                return "ok"; 
            }
            return null;
        }
    }
}
