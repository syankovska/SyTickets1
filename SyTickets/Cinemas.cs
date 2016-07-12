using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Configuration;

using SyTickets.ODataService;

namespace SyTickets
{
    public class Cinemas
    {
        private Uri uri;

        public Cinemas()
        {
            ////   Configuration configuration = ConfigurationManager.(ConfigurationUserLevel.PerUserRoamingAndLocal);
            //   ClientSection client = ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;

            //  // ServiceModelSectionGroup serviceModelSectionGroup = ServiceModelSectionGroup.GetSectionGroup(configuration);
            // //  ClientSection clientSection = serviceModelSectionGroup.Client;
            //   var el = client.Endpoints[0];
            //   Uri myUri = new Uri(el.Address.ToString());
            //   string endPoint = "http://" + myUri.Host + "/WSVistaWebClient/OData.svc";

            //   uri = new Uri(@endPoint);


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
            }
            catch (Exception)
            {
                uri = new Uri("http://dc1-vweb2-win12.multiplex.ua/WSVistaWebClient/OData.svc");
            }


        }

        public List<SyCinema> GetCinemas()
        {
            var container = new ConnectODataEntities(uri);
            List<SyTickets.ODataService.Cinema> lsCinemasRaw =
                (from cinema in container.Cinemas
                 select cinema).ToList();

            return GetSyCinemas(lsCinemasRaw);
        }

        public string GetCinemaByCinemaID(string cinemaId)
        {
            var container = new ConnectODataEntities(uri);

        
            SyTickets.ODataService.Cinema cinema=
                (from c in container.Cinemas
                 select c).Where(x => x.ID == cinemaId).FirstOrDefault();
           
            if (cinema !=null)
                    return cinema.Name;
            else return "";
        }

        public List<SyCinema> GetCinemasByCity(string city)
        {
            if (city != null)
            {
                var container = new ConnectODataEntities(uri);
                List<SyTickets.ODataService.Cinema> lsCinemasRaw =
                    (from cinema in container.Cinemas
                     select cinema).Where(x => x.City == city).ToList();

                return GetSyCinemas(lsCinemasRaw);
            }
            else return GetCinemas();
        }

        public List<SyCinema> GetCinemasByCityAndScheduledFilmId(string city, string scheduledFilmId)
        {
            if (scheduledFilmId != null)
            {
                Films films = new Films();

                List<SyFilm> syFilms = films.GetFilmsByScheduledFilmId(scheduledFilmId);

                if (syFilms != null)
                { 
                var container = new ConnectODataEntities(uri);
                List<SyTickets.ODataService.Cinema> lsCinemasRaw =
                    (from cinema in container.Cinemas
                     select cinema).Where(x => x.City == city).ToList();
                    List<SyCinema> syCinemas = GetSyCinemas(lsCinemasRaw);
                    List<SyCinema> ls = (from firstItem in syCinemas 
                              join secondItem in syFilms
                              on firstItem.ID equals secondItem.CinemaId
                              select firstItem).ToList();


                    return ls;
                }
                else return GetCinemasByCity(city);
            }
            else return GetCinemasByCity(city);
        }


        
        public List<string> GetCities()
        {
            var container = new ConnectODataEntities(uri);
            List<Cinema> lsCinema =
                (from cinema in container.Cinemas
                 select cinema).ToList();


            var distinctCities = lsCinema
          .GroupBy(p => new { p.City })
          .Select(g => g.First())
          .ToList();

            List<string> lsDistinctCities =  new List<string>();
            foreach (var c in distinctCities)
               lsDistinctCities.Add(c.City);
             
            return lsDistinctCities;
        }

        private List<SyCinema> GetSyCinemas(List<SyTickets.ODataService.Cinema> lsCinemasRaw)
        {
            List<SyCinema> lsCinemas =
              (from Cinema in lsCinemasRaw

               select new SyCinema()
               {
                   ID = Cinema.ID
                        ,
                   Name = Cinema.Name
                        ,
                   NameAlt = Cinema.NameAlt
                        ,
                   Address1 = Cinema.Address1
                        ,
                   Address2 = Cinema.Address2
                        ,
                   PhoneNumber = Cinema.PhoneNumber
                        ,
                   City = Cinema.City
                        ,
                   Description = Cinema.Description
                        ,
                   HOPK = Cinema.HOPK
                   ,
                   LoyaltyCode = Cinema.LoyaltyCode
                   ,
                   ParkingInfo = Cinema.ParkingInfo
                   ,
                   PublicTransport=Cinema.ParkingInfo
               }).ToList();
            return lsCinemas;
        }
    }
}
