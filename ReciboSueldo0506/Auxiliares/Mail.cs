using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;

using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;

using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;


namespace ReciboSueldo0506.Auxiliares
{
    public class Mail
    {
        
        internal  void EnviarMail(string telusuario,string mailusuario, string subject, byte[] recibo )
        {

            Random r = new Random();
            string pass = r.Next(100000, 999999).ToString();
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();
            string folder = WebConfigurationManager.AppSettings["tempFileUrl"].ToString();
            byte[] hashedBytes = provider.ComputeHash(recibo);

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());

            output.ToString();
            string url = folder + output.ToString() ;
            Directory.CreateDirectory(url);
            File.WriteAllBytes(url + "/recibo.PDF", recibo);

            List<int> pages = new List<int>();
            if (File.Exists(url + "/recibo.PDF"))
            {


                try { File.Delete(folder + "/recibo.zip"); } catch { }
                using (ZipFile zip = new ZipFile())
                {

                    zip.Password = pass;
                    zip.AddDirectory(url);
                    zip.Save(folder + "/recibo.zip");
                }


               
               



                
                Attachment data = new Attachment(folder + "/recibo.zip", MediaTypeNames.Application.Octet);



                string mail = WebConfigurationManager.AppSettings["mail"].ToString();
                string passMail = WebConfigurationManager.AppSettings["passMail"].ToString();
                MailMessage email = new MailMessage();
                email.To.Add(new MailAddress(mailusuario));
                email.From = new MailAddress(mail);
                email.Subject = "Recibo de sueldo " + subject;
                email.Body = "Adjuntamos su recibo de sueldo \n Por wp llega la clave";
                email.IsBodyHtml = true;
                email.Priority = MailPriority.Normal;
                email.Attachments.Add(data);
                SmtpClient smtp = new SmtpClient();
                smtp.Host = WebConfigurationManager.AppSettings["hostMail"].ToString();
                smtp.Port = Int32.Parse(WebConfigurationManager.AppSettings["portMail"].ToString());
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(mail, passMail);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;
                string outputMail = null;


                var accountSid = WebConfigurationManager.AppSettings["twilioiaccountSid"].ToString();
                var authToken = WebConfigurationManager.AppSettings["twilioiauthToken"].ToString();
                TwilioClient.Init(accountSid, authToken);

                var messageOptions = new CreateMessageOptions(
                    new PhoneNumber("whatsapp:+549"+ telusuario));
                messageOptions.From = new PhoneNumber(WebConfigurationManager.AppSettings["twilioiNumber"].ToString());
                messageOptions.Body = "Recibo de sueldo " + subject + "tu password es: " + pass;

                var message = MessageResource.Create(messageOptions);


                try
                {
                    smtp.Send(email);
                    email.Dispose();
                    outputMail = "Corre electrónico fue enviado satisfactoriamente.";
                }
                catch (Exception ex)
                {
                    outputMail = "Error enviando correo electrónico: " + ex.Message;
                }
            }
            Console.WriteLine(output);
        }
    }
}