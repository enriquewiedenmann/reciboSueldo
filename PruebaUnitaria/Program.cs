using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace PruebaUnitaria
{
    class Program
    {
     
    static void Main(string[] args) 
    {
                var accountSid = "AC3be4a958e0828cb2d049dcf1fbe6b2c8";
                var authToken = "a9bc450e7c4c0b292ac358d108c0e0b6";
                TwilioClient.Init(accountSid, authToken);

                var messageOptions = new CreateMessageOptions(
                    new PhoneNumber("whatsapp:+5491131024372"));
                messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
                messageOptions.Body = "conectaste con CCCCC";
                
                var message = MessageResource.Create(messageOptions);
                Console.WriteLine(message.Body);

                Console.WriteLine(message.Sid);
            Console.ReadLine();


        }
    }
}
