using System;
using System.Collections.Generic;
using System.ServiceModel;


namespace SyTickets
{
    [ServiceContract]
    public interface IFilms
    {

        [OperationContract]
        List<SyFilm> GetAllFilms();

        [OperationContract]
        List<SyFilm> GetFilmsByCinema(string cinemaId);
      
    }
}
