using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Concreate
{
    public class EmailSettings
    {
        public string MailToAdress = "order@example.com";
        public string MailFromAdress = "partsstore@example.com";
        public bool UseSs1 = true;
        public string Username = "MySntpUsername";
        public string Password = "MySntpPassword";
        public string ServerName = "sntp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\parts_store_emails";


    }

    public class EmailOrderProcess : IOrderProcessor
  {
        private EmailSettings emailSettings;
        public EmailOrderProcess(EmailSettings settings)
        {
            emailSettings = settings;
        }


        public void ProcessorOrder(Cart cart, ShippingDetails shippingDetails)
        {

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSs1;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                StringBuilder body = new StringBuilder()
                .AppendLine("Новый заказ оброботан")
                .AppendLine("---")
                .AppendLine("Товары:");
                
                foreach(var line in cart.Lines)
                {
                    var subtotal = line.Part.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (итого: {2:c}", line.Quantity, line.Part.Name, subtotal);
                }

                body.AppendFormat("Общая стоимость:{0:c}", cart.ComputerTotalValue())
                    .AppendLine("---")
                    .AppendLine("Доставка")
                    .AppendLine(shippingDetails.Name)
                    .AppendLine(shippingDetails.Line1)
                    .AppendLine(shippingDetails.Line2 ?? "")
                    .AppendLine(shippingDetails.Line3 ?? "")
                    .AppendLine(shippingDetails.City)
                    .AppendLine(shippingDetails.Country)
                    .AppendLine("---")
                    .AppendFormat("Деревянная обрешетка: {0}", shippingDetails.GiftWrap ? "Да" : "Нет");

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAdress,
                    emailSettings.MailToAdress,
                    "Новый заказ отправлен!",
                    body.ToString()
                    );

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }

        }
    }
}
