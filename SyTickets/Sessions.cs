using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel.Configuration;
using SyTickets.ODataService;

namespace SyTickets
{
    public class Sessions : ISessions
    {
        private Uri uri;

        public Sessions()
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
            }
            catch (Exception)
            {
                uri = new Uri("http://dc1-vweb2-win12.multiplex.ua/WSVistaWebClient/OData.svc");
            }

        }

        public List<SySession> GetAllSessions()
        {
            var container = new ConnectODataEntities(uri);
            List<SyTickets.ODataService.Session> lsSessionsRaw =
                      (from session in container.Sessions
                       select session).ToList(); 

            return GetSySessions(lsSessionsRaw);
        }

        public List<SySession> GetSessionsByCinema(string cinemaId)
        {
            var container = new ConnectODataEntities(uri);

            List<SyTickets.ODataService.Session> lsSessionsRaw =
           (from session in container.Sessions
            where session.CinemaId == cinemaId
            select session).ToList(); 

            return GetSySessions(lsSessionsRaw);
        }

        public List<SySession> GetSessionsBySessionBusinessDate(DateTime sessionBusinessDate)
        {
            var container = new ConnectODataEntities(uri);

            List<SyTickets.ODataService.Session> lsSessionsRaw =
           (from session in container.Sessions
            where session.SessionBusinessDate == sessionBusinessDate
            select session).ToList();

            return GetSySessions(lsSessionsRaw);
        }

        public List<SySession> GetSessionsByCinemaAndSessionBusinessDate(string cinemaId, DateTime sessionBusinessDate)
        {
            var container = new ConnectODataEntities(uri);

            List<SyTickets.ODataService.Session> lsSessionsRaw =
           (from session in container.Sessions
            where session.SessionBusinessDate == sessionBusinessDate && session.CinemaId == cinemaId
            select session).ToList();

            return GetSySessions(lsSessionsRaw);
        }

        public List<SySession> GetSessionsByScheduledFilmAndSessionBusinessDate(string scheduledFilm, DateTime sessionBusinessDate)
        {
            var container = new ConnectODataEntities(uri);

            List<SyTickets.ODataService.Session> lsSessionsRaw =
           (from session in container.Sessions
            where session.SessionBusinessDate == sessionBusinessDate && session.ScheduledFilmId == scheduledFilm
            select session).ToList();

            return GetSySessions(lsSessionsRaw);
        }

        public List<SySession> GetSessionsByScheduledFilmAndSessionBusinessDateAndCity(string scheduledFilm, DateTime sessionBusinessDate, string city)
        {
            var container = new ConnectODataEntities(uri);

            List<SyTickets.ODataService.Session> lsSessionsRaw =
           (from session in container.Sessions
            where session.SessionBusinessDate == sessionBusinessDate && session.ScheduledFilmId == scheduledFilm
            select session).ToList();
            List<SySession> sySessions = GetSySessions(lsSessionsRaw);
            Cinemas c = new Cinemas();
            List<SyCinema> syCinemas = c.GetCinemasByCity(city);
            var ls = (from firstItem in sySessions
                      join secondItem in syCinemas
                      on firstItem.CinemaId equals secondItem.ID
                      select firstItem).ToList();

            return ls;
        }
        private List<SySession> GetSySessions(List<SyTickets.ODataService.Session> lsSessionsRaw)
        {
            List<SySession> lsSessions =
              (from session in lsSessionsRaw

               select new SySession()
               {
                   ID = session.ID
                   ,CinemaId = session.CinemaId
                   ,ScheduledFilmId = session.ScheduledFilmId
                   ,SessionBusinessDate = session.SessionBusinessDate
                   ,SessionId = session.SessionId
                   ,SeatsAvailable = session.SeatsAvailable
                   ,Showtime = session.Showtime
                   ,SessionAttributesNames = session.SessionAttributesNames.ToList()
                   ,CinemaName = GetCinemaByCinemaID(session.CinemaId)
                   ,ScheduledFilmTitle =GetFilmTitleByScheduledFilmId(session.ScheduledFilmId)
               }).ToList();
            return lsSessions;
        }


        public List<SyFilm> GetAllFilms()
        {
            var f = new Films();
            return f.GetAllFilms();
        }

        public List<SyFilm> GetFilmsByCinema(string cinemaId)
        {
            var f = new Films();
            return f.GetFilmsByCinema(cinemaId);
        }

        public List<SyRestTicketType> GetTicketTypes(string cinemaid, string sessionid)
        {
            var tt = new RestTicketTypes();
            return tt.GetTicketTypes( cinemaid,  sessionid);
        }

        public SySeatLayoutData GetRestAddTickets(string cinemaid, string sessionid, List<SyTicketType> syTicketTypes, bool processOrderValue = false,
            bool userSelectedSeatingSupported = true,
            bool skipAutoAllocation = true)
        {
             var at = new RestAddTickets();
            return at.GetRestAddTickets(cinemaid, sessionid, syTicketTypes,  processOrderValue,
            userSelectedSeatingSupported, skipAutoAllocation);
        }

        public string CancelOrder(string userSessionId)
        {
            var o = new Order();
            return o.SyCancelOrder(userSessionId);
        }

        public SyCompleteOrderResponse CompleteOrder(string userSessionId, int paymentValueCents, string bookingNotes, bool unpaidBooking,
            string customerEmail, string customerPhone, string customerName)
        {
            var o = new Order();
            return o.SyCompleteOrder(userSessionId, paymentValueCents, bookingNotes, unpaidBooking, customerEmail, customerPhone, customerName);
        }

        public TaslinkOrderResponse GetTaslinkOrder(string amount, string uri)
        {
            var p = new Payment();
            return p.GetTaslinkOrder(amount,uri);
        }

        public TaslinkStatusResponse GetTaslinkStatus(string oid)
        {
            var p = new Payment();
            return p.GetTaslinkStatus(oid);
        }

        public TaslinkReverseResponse GetTaslinkReverse(string oid)
        {
            var p = new Payment();
            return p.GetTaslinkReverse(oid);
        }

        public List<SyCinema> GetCinemas()
        {
            var c = new Cinemas();
            return c.GetCinemas();
        }

        public string GetCinemaByCinemaID(string cinemaId)
        {
            var c = new Cinemas();
            return c.GetCinemaByCinemaID(cinemaId);
        }

        public string GetFilmTitleByScheduledFilmId(string scheduledFilmId)
        {
            var f = new Films();
            return f.GetFilmTitleByScheduledFilmId(scheduledFilmId);
        }

        public List<SyFilm> GetFilmsByScheduledFilmId(string scheduledFilmId)
        {
            var f = new Films();
            return f.GetFilmsByScheduledFilmId(scheduledFilmId);
        }
        public string SySetSelectedSeats(string userSessionId, string cinemaId, string sessionId, List<SySelectedSeat> sySelectedSeats)
        {
            var o = new Order();
            return o.SySetSelectedSeats(userSessionId,cinemaId,sessionId,sySelectedSeats);
        }

        public List<DateTime> GetDistinctSessionDate()
        {
            var container = new ConnectODataEntities(uri);
            List<SyTickets.ODataService.Session> lsSessionRaw =
                (from session in container.Sessions
                 select session).ToList();


            var distinctSessionDate = lsSessionRaw
          .GroupBy(p => new { p.Showtime.Date })
          .Select(g => g.First())
          .ToList();

            List<DateTime> lsSessionDates =
             (from session in distinctSessionDate
              select
                session.Showtime.Date
              ).ToList();

            return lsSessionDates;
        }

        public List<DateTime> GetDistinctSessionDateByCity(string city)
        {
            if (city != null)
            { 
            Cinemas c = new Cinemas();
            List<SyCinema> syCinemas = c.GetCinemasByCity(city);

    

            var container = new ConnectODataEntities(uri);

                List<Session> lsSessionRaw1 =
                    (from session in container.Sessions
                     select session).ToList();


                List<Session> lsSessionRaw =
                    (from session in lsSessionRaw1
                     join secondItem in syCinemas
                     on session.CinemaId equals secondItem.ID
                     select session).ToList();


                var distinctSessionDate = lsSessionRaw
          .GroupBy(p => new { p.Showtime.Date })
          .Select(g => g.First())
          .ToList();

            List<DateTime> lsSessionDates =
             (from session in distinctSessionDate
              select
                session.Showtime.Date
              ).OrderBy(x => x).ToList();

                return lsSessionDates;
            }
            else
                return GetDistinctSessionDate();


        }

        public System.IO.MemoryStream GeneratePdf( string printStream, int totalOrderCount)
        {
            var o = new Order();
           return  o.GeneratePdf(printStream,totalOrderCount);
        }

        public List<string> GetCities()
        {
            var c = new Cinemas();
            return c.GetCities();
        }
        
        public List<SyCinema> GetCinemasByCity(string city)
        {
            var c = new Cinemas();
            return c.GetCinemasByCity(city);
        }


        public List<SyFilm> GetFilmsByCity(string city)
        {
            var f = new Films();
            return f.GetFilmsByCity(city);
        }

        public List<SyCinema> GetCinemasByCityAndScheduledFilmId(string city, string scheduledFilmId)
        {
            var c = new Cinemas();
            return c.GetCinemasByCityAndScheduledFilmId(city, scheduledFilmId);
        }
    }
}
