using Movies.Core.Helper;
using Movies.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Service.Services
{
    public class MoviesService
    {
        /// <summary>
        /// Seçilen Filmi verilen mail adresine gönderir
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mailAdress"></param>
        /// <returns></returns>
        public static string SharedMail(int id, string mailAdress)
        {
            //Bir Result geri dönüş nesnesi oluşturulmalı
            try
            {
                var validationIDResult = CustomValidation.IsValidID(id);
                if (!validationIDResult.IsValid)
                    throw new DirectoryNotFoundException(validationIDResult.Message);

                var validationEmailResult = CustomValidation.IsValidEmail(mailAdress);
                if (!validationEmailResult.IsValid)
                    throw new DirectoryNotFoundException(validationEmailResult.Message);

                //Filmin Detayları Çekilecek

                //Burda Gelen Datalar İle Doldurulacak Alan
                MailSendInformationModel mailInformation = new MailSendInformationModel
                {
                    Imbdpoint = "3",
                    MovieDate = "1999/20/03",
                    MovieName = "Leyla İle Mecnun",
                    MailAdress = "pakcan.emre@gmail.com"
                };

              var mailResult = MailHelper.SendMailInformation(mailInformation);
                if (!mailResult)
                    throw new DirectoryNotFoundException("E-Posta Gönderilirken Bir Hata İle Karşılaşıldı.");


                return "İşlem Başarılı"; //Burası Result İle Dönmeli
            }
            catch (Exception ex)
            {
                //Buraya Log atmak lazım
                throw ex;
            }
        }
    }
}
