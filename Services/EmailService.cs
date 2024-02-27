using System.Net.Mail;
using System.Net;
using XPTechnicalInterview.Domain;
using XPTechnicalInterview.Repositories;

namespace XPTechnicalInterview.Services
{
    public class EmailService
    {
        private string user;
        private string password;
        private readonly FinancialProductRepository financialProductRepository;
        private readonly IConfiguration configuration;
        public EmailService(FinancialProductRepository _FinancialProductRepository, IConfiguration _Configuration)
        {
            financialProductRepository = _FinancialProductRepository;
            configuration = _Configuration;
            user = configuration["Settings:EmailUser"];
            password = configuration["Settings:EmailPassword"];
        }
        public void SendEmail(Email email)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(user, password),
                EnableSsl = true
            };
            var products = financialProductRepository.ListByExpirationDate(email.DaysToExpire);

            var htmlBody = "<html lang='pt'><meta charset='utf-8'><head> <style> body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size: 14px; } table { border-collapse: collapse; width: 100%; } th, td { padding: 8px; text-align: left; border-bottom: 1px solid #ddd; } th { background-color: #f2f2f2; font-weight: bold; } </style></head> </meta><body> <h2>Financial Products about to expire</h2> <table> <thead> <tr> <th>Name</th> <th>Expiration Date</th> </tr> </thead> <tbody>";

            foreach (var product in products)
            {
                var aux = String.Format("<tr><td>{0}</td><td>{1}</td></tr>", product.Name, product.DueDate.ToString("dd/MM/yyyy HH:mm"));
                htmlBody += aux;
            }

            htmlBody += "</tbody> </table></body></html>";
            var mailMessage = new MailMessage
            {
                From = new MailAddress(user),
                Subject = "Financial Products about to expire",
                Body = htmlBody,
                IsBodyHtml = true
            };
            var Recipients = email.Recipients.Split(";");
            foreach (var x in  Recipients)
            {
                mailMessage.To.Add(x);
            }

            smtpClient.Send(mailMessage);
        }
    }
}
