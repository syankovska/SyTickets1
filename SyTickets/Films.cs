using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Configuration;
using SyTickets.ODataService;

namespace SyTickets
{
    public class Films 
    {
        private Uri uri;

        public Films()
        {
            try
            {
                ClientSection client = ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;
                ChannelEndpointElement el = null;

                for (int i = 0; i < client.Endpoints.Count; i++)
                {
                    if (client.Endpoints[i].Address.ToString().Contains("TicketingService"))
                        el = client.Endpoints[i];
                }

                Uri myUri = new Uri(el.Address.ToString());
                string endPoint = "http://" + myUri.Host + "/WSVistaWebClient/OData.svc";

                uri = new Uri(@endPoint);


                //System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\test.txt");
                //file.WriteLine(el.ToString());
                //file.WriteLine(el.Address);
                //file.WriteLine(el.Binding);
                //file.WriteLine(client.Endpoints.Count);
                //file.Close();
            }
            catch (Exception)
            {
                uri = new Uri("http://dc1-vweb2-win12.multiplex.ua/WSVistaWebClient/OData.svc");
            }
        }
            

        

        public List<SyFilm> GetAllFilms()
        {
            var container = new ConnectODataEntities(uri);
            List<SyTickets.ODataService.ScheduledFilm> lsFilmsRaw =
                (from film in container.ScheduledFilms
                    select film).ToList();


           var distinctFilms = lsFilmsRaw
         .GroupBy(p => new { p.ScheduledFilmId, p.Title })
         .Select(g => g.First())
         .ToList();

            List<SyFilm> lsFilms =
             (from film in distinctFilms

              select new SyFilm()
              {
                 ScheduledFilmId = film.ScheduledFilmId
                       ,
                  Title = film.Title
              }).ToList();
           
            return lsFilms;
        }

        public List<SyFilm> GetFilmsByCity(string city)
        {
            if (city != null)
            {
                var container = new ConnectODataEntities(uri);

                Cinemas c = new Cinemas();
                List<SyCinema> syCinemas = c.GetCinemasByCity(city);

                List<SyTickets.ODataService.ScheduledFilm> lsFilmRaw =
                (from film in container.ScheduledFilms
                 select film).ToList();

                List<SyFilm> syFilms = GetSyFilms(lsFilmRaw);


                var ls = (from firstItem in syFilms
                          join secondItem in syCinemas
                          on firstItem.CinemaId equals secondItem.ID
                          select firstItem).ToList();

                List<SyFilm> distinctFilms = ls
            .GroupBy(p => new { p.ScheduledFilmId, p.Title })
            .Select(g => g.First())
            .ToList();


                return distinctFilms;
            }
            else return GetAllFilms();
        }

     
        public List<SyFilm> GetFilmsByCinema(string cinemaId)
        {
            var container = new ConnectODataEntities(uri);
            List<SyTickets.ODataService.ScheduledFilm> lsFilmsRaw =
                (from film in container.ScheduledFilms
                 where film.CinemaId == cinemaId
                 select film).ToList();

            return GetSyFilms(lsFilmsRaw);
        }

        private List<SyFilm> GetSyFilms(List<SyTickets.ODataService.ScheduledFilm> lsFilmsRaw)
        {
            List<SyFilm> lsFilms =
              (from film in lsFilmsRaw

               select new SyFilm()
               {
                   ID = film.ID
                        ,CinemaId = film.CinemaId
                        //,CinemaName = film.CinemaName
                        ,AllowTicketSales = film.AllowTicketSales
                        ,OpeningDate = film.OpeningDate
                        ,ScheduledFilmId = film.ScheduledFilmId
                        ,Title = film.Title
               }).ToList();
            return lsFilms;
        }

        public string GetFilmTitleByScheduledFilmId(string scheduledFilmId)
        {
            var container = new ConnectODataEntities(uri);

        
            SyTickets.ODataService.ScheduledFilm film=
                (from c in container.ScheduledFilms
                 select c).Where(x => x.ScheduledFilmId == scheduledFilmId).FirstOrDefault();
           
            if (film != null)
                    return film.Title;
            else return "";
        }

        public List<SyFilm> GetFilmsByScheduledFilmId(string scheduledFilmId)
        {
            var container = new ConnectODataEntities(uri);


            List<SyTickets.ODataService.ScheduledFilm> films =
                (from c in container.ScheduledFilms
                 select c).Where(x => x.ScheduledFilmId == scheduledFilmId).ToList();

            return GetSyFilms(films);
         
        }
    }
}
    