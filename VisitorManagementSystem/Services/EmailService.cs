using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.InkML;
using MailKit.Net.Smtp;
using MimeKit;
using Org.BouncyCastle.Asn1.Cmp;
using System.Globalization;
using VisitorManagementSystem.Data;
using VisitorManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace VisitorManagementSystem.Services
{
    public class EmailService
    {
      
        private readonly IServiceScopeFactory _scopeFactory;

        public EmailService(IServiceScopeFactory scopeFactory)
        {
         
            _scopeFactory = scopeFactory;
        }


        public async Task SendEmailAsync()
        {
           
            // Create a new instance of VMSDbContext using the factory
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VMSDbContext>();
                var eData = await context.Employee.ToListAsync();

                var mailMessage = new MimeMessage();
                mailMessage.From.Add(new MailboxAddress("VMS", "RehlkoVMS@rehlko.com"));
                foreach (var e in eData.Where(x => x.Role.ToLower() == "admin"))
                {
                    mailMessage.To.Add(new MailboxAddress("VMS", e.EmpEmail));
                }

                mailMessage.Subject = $"Today count of visitors.";

                // Calculate visitor counts
                var counts = await CalculateCountsAsync(context); // Pass the context to the method
                var countCheckin = counts.Item1;
                var countCheckout = counts.Item2;
                var inCompany = counts.Item3;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $@"
                <html>
                    <body>
                        <p>Hello,</p>
                        <h2><b>Today's visitors count  till 5:30 PM</b></h2>
                        <h2>Check-ins: {countCheckin}</h2>
                        <h2>Check-outs: {countCheckout}</h2>
                        <h2>Currently in Company: {inCompany}</h2>
                        <p>
                            <a href='https://inauw028.kohlerco.com:8054/dashboard'>Click Here To Checkout</a>
                        </p>
                        <p style='margin-top:1rem'>
                            Thank You
                        </p>
                    </body>
                </html>
                ";

                mailMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("mailhost.kohlerco.com", 25, MailKit.Security.SecureSocketOptions.StartTls);
                    // await client.AuthenticateAsync("your_email@example.com", "your_password");
                    await client.SendAsync(mailMessage);
                    await client.DisconnectAsync(true);
                }
            }
        }

        private async Task<(int, int, int)> CalculateCountsAsync(VMSDbContext vmsDbContext)
        {
            var todayDate = DateOnly.FromDateTime(DateTime.Now.Date);
            var vTransactions = await vmsDbContext.VisitorTransactions.Where(v => v.Date == todayDate).ToListAsync();

            int countCheckout = vTransactions.Count(v => v.CheckOut is not null);
            int countCheckin = vTransactions.Count(v => v.CheckIn is not null);
            int inCompany = countCheckin - countCheckout;

            return (countCheckin, countCheckout, inCompany);
        }
    }
}

//namespace VisitorManagementSystem.Services
//{
//    public class EmailService
//    {
//        private readonly VMSDbContext _vmsDbContext;

//        // Constructor to inject the VMSDbContext
//        public EmailService(VMSDbContext vmsDbContext)
//        {
//            _vmsDbContext = vmsDbContext;
//        }

//        public async Task SendEmailAsync()
//        {
//            List<EmpData>? eData = await _vmsDbContext.Employee.ToListAsync();

//            var mailMessage = new MimeMessage();
//            mailMessage.From.Add(new MailboxAddress("VMS", "RehlkoVMS@rehlko.com"));
//            foreach (var e in eData.Where(x => x.Role == "Admin"))
//            {
//                mailMessage.To.Add(new MailboxAddress("VMS", e.EmpEmail));
//            }

//            mailMessage.Subject = $"Today count of visitors.";

//            // Calculate visitor counts
//            var counts = await CalculateCountsAsync(); // Await the async method
//            var countCheckin = counts.Item1;
//            var countCheckout = counts.Item2;
//            var inCompany = counts.Item3;

//            var bodyBuilder = new BodyBuilder();
//            bodyBuilder.HtmlBody = $@"
//    <html>
//        <body>
//            <p>Hello,</p>
//            <p>Today's count of visitors till 5:00 PM:</p>
//            <h2>Check-ins: {countCheckin}</h2>
//            <h2>Check-outs: {countCheckout}</h2>
//            <h2>Currently in Company: {inCompany}</h2>
//            <p>
//                <a href='https://inauw028.kohlerco.com:8054/dashboard'>Click Here To Checkout</a>
//            </p>
//            <p style='margin-top:1rem'>
//                Thank You
//            </p>
//        </body>
//    </html>
//    ";

//            mailMessage.Body = bodyBuilder.ToMessageBody();

//            using (var client = new SmtpClient())
//            {
//                await client.ConnectAsync("mailhost.kohlerco.com", 25, MailKit.Security.SecureSocketOptions.StartTls);
//                // await client.AuthenticateAsync("rupali.awati@kohler.com", "joewomthjbxxgepm");
//                await client.SendAsync(mailMessage);
//                await client.DisconnectAsync(true);
//            }
//        }

//        private async Task<(int, int, int)> CalculateCountsAsync()
//        {
//            var todayDate = DateOnly.FromDateTime(DateTime.Now.Date);
//            var vTransactions = await _vmsDbContext.VisitorTransactions.Where(v => v.Date == todayDate).ToListAsync();

//            int countCheckout = vTransactions.Count(v => v.CheckOut is not null);
//            int countCheckin = vTransactions.Count(v => v.CheckIn is not null);
//            int inCompany = countCheckin - countCheckout;

//            return (countCheckin, countCheckout, inCompany);
//        }
//    }
//}
