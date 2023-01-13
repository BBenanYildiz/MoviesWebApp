using Movies.Core.ViewModels;
using System.Net;
using System.Net.Mail;

namespace Movies.Core.Helper
{
    public class MailHelper
    {
        public static bool SenAdMail(MailHelperModel model)
        {
            using (var client = new SmtpClient(model.smtp, model.smtpPort)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(model.mailId, model.mailPass),
            })
            {
                var message = new MailMessage
                {
                    From = new MailAddress(model.mailId, model.FromDisplayName),
                    Subject = model.Subject,
                    IsBodyHtml = true,
                    Body = model.Body,
                };
                if (model.AttachFileNames != null && model.AttachFileNames.All(x => File.Exists(x)))
                    model.AttachFileNames.ToList().ForEach(x => message.Attachments.Add(new Attachment(x)));

                model.To.ToList().ForEach(x => message.To.Add(x));

                try
                {
                    client.Send(message);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool SendMailInformation(MailSendInformationModel mailInformation)
        {
            #region MailTemplate

            string bodyDetail = "Merhaba... <br>" + "<strong>" + mailInformation.MovieName + "</strong>" + " bu film tam sana göre " + mailInformation.MovieDate + " tarihinde vizyona girmiş. IMBD puanıda seninle paylaşıyorum. " + mailInformation.Imbdpoint + " umarım beğenerek izlersin.";

            #endregion

            var mailModel = new MailHelperModel
            {
                mailId = "movieapp34@gmail.com",
                mailPass = "vplnftbogdqxykop",
                smtp = "smtp.gmail.com",
                smtpPort = 587,
                Subject = "Film Önerisi",
                Body = bodyDetail,
                To = new string[] { mailInformation.MailAdress },
                FromDisplayName = "Film Önerisi"
            };

            if (MailHelper.SenAdMail(mailModel))
                return true;
            else
                return false;
        }

        public static void SystemInformationMail(string exception = "", string message = "", string subject = "Sistem Bilgilendirmesi")
        {
            #region MailTemplate

            string bodyDetail = "Sistem Tarafından Gönderilmiş Maildir. <br>" +
                   "Mesaj: " + message + "<br>" +
                   "Tarih: " + DateTime.Now.ToString() + "<br> +" +
                   "Exception: " + exception + "<br>";

            #endregion

            var mailModel = new MailHelperModel
            {
                mailId = "movieapp34@gmail.com",
                mailPass = "vplnftbogdqxykop",
                smtp = "smtp.gmail.com",
                smtpPort = 587,
                Subject = subject,
                Body = bodyDetail,
                To = new string[] { "bbenanyildiz@gmail.com" },  
                FromDisplayName = "Sistem Bilgilendirmesi"
            };
        }
    }
}