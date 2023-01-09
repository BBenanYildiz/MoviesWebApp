using Movies.Core.ViewModels;
using System.Net;
using System.Net.Mail;

namespace Movies.Core.Helper
{
    public class MailHelper
    {
        //E-postayı gönderiyoruz.
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

        //E-posta için gerekli bilgileri dolduruyoruz.
        public static bool SendMailInformation(MailSendInformationModel mailInformation)
        {
            #region MailTemplate

            string bodyDetail = "Merhaba... <br>" + "<strong>" + mailInformation.MovieName + "</strong>" + " bu film tam sana göre " + mailInformation.MovieDate + " tarihinde vizyona girmiş. IMBD puanıda seninle paylaşıyorum. " + mailInformation.Imbdpoint + " umarım beğenerek izlersin.";

            #endregion

            var mailModel = new MailHelperModel
            {
                mailId = "pakcan.emre@gmail.com",
                mailPass = "zxgvbyxbgjdpkmzn",
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
    }
}