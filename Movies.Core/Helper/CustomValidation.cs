using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Helper
{
    public static class CustomValidation
    {
        public static (bool IsValid, string Message) IsValidID(int id)
        {
            if (id == 0)
                return (false, "ID 0 olamaz");

            return (true, "Validasyon başarılı");
        }

        public static (bool IsValid, string Message) IsValidEmail(string mailAdress)
        {
            var result = new EmailAddressAttribute().IsValid(mailAdress);

            if (result == false)
                return (false, "Geçerli bir mail adresi giriniz.");

            return (true, "Validasyon başarılı");
        }

        public static (bool IsValid, string Message) IsValidPoint(int point)
        {
            if (point == 0)
                return (false, "Puan 0 olamaz");

            if (point <= 1 || point >= 10)
                return (false, "1 ile 10 Arasında bir tam sayı giriniz.");

            return (true, "Validasyon başarılı");
        }

        public static (bool IsValid, string Message) IsValidComment(string comment)
        {
            if (string.IsNullOrEmpty(comment))
                return (false, "İçerik giriniz.");

            return (true, "Validasyon başarılı");
        }

        public static (bool IsValid, string Message) IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return (false, "Şifre giriniz.");

            return (true, "Validasyon başarılı");
        }
    }
}
