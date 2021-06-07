using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace ReciboSueldo0506.Models
{
    public class Recibo
    {
        [Display(Name = "Id")]
        public  int IdRecibo { get; set; }
        public byte[] Archivo { get; set; }
       
        public  Empleado Empleado { get; set; }
        public  Lote Lote { get; set; }

        public int IdEmpleado { get; set; }
        public int IdLote { get; set; }

        internal string CrearArchivo()
        {
            string respuesta = "../../imagenes/noDisponible.html";
            if(this.Archivo!=null){
                SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();
                string folder = WebConfigurationManager.AppSettings["tempFileUrl"].ToString();
                byte[] hashedBytes = provider.ComputeHash(this.Archivo);

                StringBuilder output = new StringBuilder();

                for (int i = 0; i < hashedBytes.Length; i++)
                    output.Append(hashedBytes[i].ToString("x2").ToLower());

                output.ToString();
                string url = folder + output.ToString() + ".PDF";

                File.WriteAllBytes(url, this.Archivo);
                respuesta = "../../TempFiles/" + output.ToString() + ".PDF";
            }
            return respuesta;
        }
    }
}