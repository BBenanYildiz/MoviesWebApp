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
    }
}
