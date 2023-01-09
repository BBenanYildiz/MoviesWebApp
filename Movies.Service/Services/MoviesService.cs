using AutoMapper;
using Movies.Core.Helper;
using Movies.Core.Model;
using Movies.Core.Repositories;
using Movies.Core.Services;
using Movies.Core.UnitOfWorks;
using NLayerApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Service.Services
{
    public class MoviesService : GenericService<Movie>, IMoviesService
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly IMapper _mapper;
        public MoviesService(IGenericRepository<Movie> repository,
            IUnitOfWork unitOfWork, IMoviesRepository moviesRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _moviesRepository = moviesRepository;
            _mapper = mapper;
        }

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
                    MailAdress = mailAdress
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
