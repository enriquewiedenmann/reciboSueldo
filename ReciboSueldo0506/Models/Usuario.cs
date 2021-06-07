using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ReciboSueldo0506.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string UserName { get; set; }
        public int IdEmpleado { get; set; }
        public Empleado Empleado { get; set; }

        internal string sendPass()
        {
            Random r = new Random();
            string pass = r.Next(100000, 999999).ToString();
            var accountSid = WebConfigurationManager.AppSettings["twilioiaccountSid"].ToString();
            var authToken = WebConfigurationManager.AppSettings["twilioiauthToken"].ToString();
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
            new PhoneNumber("whatsapp:+549" + Empleado.Celular));
            messageOptions.From = new PhoneNumber(WebConfigurationManager.AppSettings["twilioiNumber"].ToString());
            messageOptions.Body = "tu password es: " + pass;

            var message = MessageResource.Create(messageOptions);

            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();
            byte[] inputBytes = Encoding.UTF8.GetBytes(pass);
            byte[] hashedBytes = provider.ComputeHash(inputBytes);

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());

            return output.ToString();

        }
    }
}