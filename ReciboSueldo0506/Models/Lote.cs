using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using ReciboSueldo0506.Auxiliares;
using ReciboSueldo0506.Controllers;
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
    public class Lote
    {
        [Display(Name = "Id")]
        public  int IdLote { get; set; }
        [Display(Name = "Mes")]
        public  string Mes { get; set; }
        [Display(Name = "Año")]
        public  string Anio { get; set; }
        [Display(Name = "Concepto")]
        public  string Concepto { get; set; }
        [Display(Name = "Fecha de Alta")]
        public  DateTime FecAlta { get; set; }

        [Display(Name = "Archivo Lote")]
        public Byte[] ArchivoLote { get; set; }

        public  List<Recibo> Recibos { get; set; }

        public String GetDescripcion ()
        {
            return String.Format("{0}-{1}-{2}", this.Anio, this.Mes, this.Concepto);
        }

        internal string CrearArchivo()
        {
            string respuesta = "../../imagenes/noDisponible.html";
            if (this.ArchivoLote != null) { 
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();
            string folder = WebConfigurationManager.AppSettings["tempFileUrl"].ToString();
            byte[] hashedBytes = provider.ComputeHash(this.ArchivoLote);

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());

            output.ToString();
            string url = folder + output.ToString() + ".PDF";

            File.WriteAllBytes(url, this.ArchivoLote);
                respuesta = "../../TempFiles/" + output.ToString() + ".PDF";
            }
            return respuesta;

        }


        public void GenerarRecibosLote(List<Empleado> empleados)
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();
            string folder = WebConfigurationManager.AppSettings["tempFileUrl"].ToString();
            byte[] hashedBytes = provider.ComputeHash(this.ArchivoLote);

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());

            output.ToString();
            string url = folder + output.ToString() + ".PDF";

            File.WriteAllBytes(url, this.ArchivoLote);

            List<int> pages = new List<int>();
            if (File.Exists(url))
            {
                PdfReader pdfReader = new PdfReader(url);
                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();

                    string currentPageText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                    foreach (Empleado e in empleados) {
                        if (currentPageText.Contains(e.Cuil))
                        {
                            Document document = new Document();
                            PdfCopy copy = new PdfCopy(document, new FileStream(folder + output.ToString() +"_"+ page + ".PDF", FileMode.Create));
                            document.Open();
                            copy.AddPage(copy.GetImportedPage(pdfReader, page));
                            document.Close();
                            byte[] bytes = System.IO.File.ReadAllBytes(folder + output.ToString() + "_" + page + ".PDF");
                            HttpPostedFileBase objFile = (HttpPostedFileBase)new MemoryPostedFile(bytes);

                            RecibosController rc = new RecibosController();
                            Recibo r = new Recibo();
                            r.IdLote = this.IdLote;
                            r.IdEmpleado = e.IdEmpleado;
                            rc.Create(r, objFile);

                        }
                    }
                }
                pdfReader.Close();
            }
         


        }

        internal void EnviarLote()
        {
            foreach(Recibo r in this.Recibos)
            {
                Mail m = new Mail();
                    m.EnviarMail(r.Empleado.Celular,r.Empleado.Mail, this.Anio + " - " + this.Mes + " - " + this.Concepto, r.Archivo);
            }
        }
    }

    public class MemoryPostedFile : HttpPostedFileBase
    {
        private readonly byte[] fileBytes;

        public MemoryPostedFile(byte[] fileBytes, string fileName = null)
        {
            this.fileBytes = fileBytes;
            this.FileName = fileName;
            this.InputStream = new MemoryStream(fileBytes);
        }

        public override int ContentLength => fileBytes.Length;

        public override string FileName { get; }

        public override Stream InputStream { get; }
    }
}