//using DYV.Services.Options;
//using Microsoft.Extensions.Options;
//using SendGrid;
//using SendGrid.Helpers.Mail;
//using System.Net;
//using System.Net.Mail;
//using System.Text.Encodings.Web;
//using System.Threading.Tasks;

//namespace DYV.Services
//{
//    public class SendGridMailer : IEmailProvider
//    {
//        readonly SendGridOptions _options;

//        public SendGridMailer(IOptions<SendGridOptions> optionsAccessor)
//        {
//            _options = optionsAccessor.Value;
//        }

//        public async Task SendEmailAsync(string email, string subject, string message, string templateId)
//        {
//            //var msg = new SendGridMessage();
//            //msg.AddTo(email);
//            //msg.From = new MailAddress("admin@mymoviesapp", "Admin");
//            //msg.Subject = subject;
//            //msg.Text = message;
//            //msg.Html = message;

//            //var cred = new NetworkCredential(_options.SendGridUser, _options.SendGridPass);

//            //var tweb = new Web(cred);

//            //return tweb.DeliverAsync(msg);

//            //SendGridAPIClient sg = new SendGridAPIClient(_options.SendGridApiKey);

//            //Email from = new Email("do-not-reply@directyourvet.com", "DirectYourVet.com");
//            //Email to = new Email(email);
//            //Content content = new Content("text/html", message);
//            //Mail mail = new Mail(from, "Confirm your email with DirectYourVet", to, content);
//            //mail.TemplateId = templateId;

//            //dynamic response = await sg.client.mail.send.post(requestbody: mail.Get());
//        }
//    }
//}
