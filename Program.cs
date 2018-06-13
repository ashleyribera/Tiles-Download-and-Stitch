using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            string to = "jlwilson@forsythco.com";
            string from = "jlwilson@forsythco.com";
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Using the new SMTP client.";
            message.Body = @"Using this new feature, you can send an e-mail message from an application very easily.";

            SmtpClient c = new SmtpClient("fcvsmtp.int.forsythco.com", 25);
            c.EnableSsl = true;
            c.SendCompleted += C_SendCompleted;
            X509CertificateCollection coll = c.ClientCertificates;

            c.SendMailAsync(message);
            Console.ReadLine();
        }

        private static void C_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            SmtpClient c = (SmtpClient)sender;
            Console.WriteLine(c.ServicePoint.Certificate.GetSerialNumberString());

            Console.WriteLine(c.ServicePoint.Certificate.Subject);
           
        }
    }
}
