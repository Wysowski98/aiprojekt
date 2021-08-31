using Database;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Policy;
using System.Text;

namespace Services
{
    public class MailSenderService : IMailSenderService
    {
        private readonly FlowerlyDbContext _context;
        public MailSenderService(FlowerlyDbContext context)
        {
            _context = context;
        }

        public void SendMail(int myflowerId, string jobId)
        {
            var flower = _context.MyFlowers.Where(x => x.Id == myflowerId).Include(x => x.User).Include(y => y.Flower).Include(y => y.IrrigationDates).FirstOrDefault();
            var item = flower.IrrigationDates.Where(x => x.ScheduledJobId == jobId).FirstOrDefault();
            if (item != null && item.IsCompleted)
            {
                item.IsCompleted = false;
                _context.MyFlowers.Update(flower);
                _context.SaveChanges();
            }

            if (item.History == null)
            {
                item.History = new List<IrrigationHistory>();
                item.History.Add(new IrrigationHistory { IrrigationDate = DateTime.Now, IsCompleted = false, MyFlower = flower, User = flower.User });
            }
            else
            {
                item.History.Add(new IrrigationHistory { IrrigationDate = DateTime.Now, IsCompleted = false, MyFlower = flower, User = flower.User });
            }

            _context.IrrigationDates.Update(item);
            _context.SaveChanges();

            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");
            var config = builder.Build();

            var smtpClient = new SmtpClient(config["Smtp:Host"])
            {
                Port = int.Parse(config["Smtp:Port"]),
                Credentials = new NetworkCredential(config["Smtp:Username"], config["Smtp:Password"]),
                EnableSsl = true,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
            };

            var irrigId = _context.IrrigationDates.FirstOrDefault(x => x.Id == item.Id).History.Last().Id;

            var callbackurl = $"api/MyFlowers/emailConfirmation/{jobId}/{irrigId}";

            var mailMessage = new MailMessage
            {
                From = new MailAddress(config["Smtp:Username"]),
                Subject = "Podlej swoj kawiat",
                Body = $"<h1>Czesc, {flower.User.DisplayName} <br> Podlej swojego kwiatka: {flower.Flower.Name} </h1> <a href={config["Host:Development"]}{callbackurl}>click here</a>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(flower.User.Email);
            smtpClient.Send(mailMessage);
        }
    }
}
