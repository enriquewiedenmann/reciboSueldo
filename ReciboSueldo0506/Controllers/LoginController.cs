using ReciboSueldo0506.DAO;
using ReciboSueldo0506.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ReciboSueldo0506.Controllers
{
    public class LoginController : Controller
    {
        private ReciboSueldoContext db = new ReciboSueldoContext();
        // GET: Login
        public ActionResult Index()
        {
            try
            { string hash = System.Web.HttpContext.Current.Session[System.Web.HttpContext.Current.Session["USER"].ToString()].ToString();

                return View();
            }
            catch { return RedirectToAction("../Home/Index"); }
        }

        public string SendPass(string userName)
        {

            UsuariosController u = new UsuariosController();
            string token = u.SendPass(userName);
            return "ok";
        }

        public ActionResult SignIn(string userName, string Password)
        {
            try
            {
                SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();
                string hash = System.Web.HttpContext.Current.Session[userName].ToString();

                byte[] inputBytes = Encoding.UTF8.GetBytes(Password);
                byte[] hashedBytes = provider.ComputeHash(inputBytes);

                StringBuilder output = new StringBuilder();

                for (int i = 0; i < hashedBytes.Length; i++)
                    output.Append(hashedBytes[i].ToString("x2").ToLower());
                string A = output.ToString();

                if (A.Equals(hash))
                {
                    System.Web.HttpContext.Current.Session["USER"] = userName;
                    return RedirectToAction("../Empleados/Index");
                }
                else
                {
                    return RedirectToAction("../Home/Index");
                }
            }
            catch
            {
                return RedirectToAction("../Home/Index");
            }

        }
       
    }
}