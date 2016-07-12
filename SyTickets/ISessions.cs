using System;
using System.Collections.Generic;
using System.ServiceModel;


namespace SyTickets
{
    [ServiceContract]
    public interface ISessions
    {

        [OperationContract]  [FaultContract(typeof(FaultException))]
        List<SySession> GetAllSessions();

        [OperationContract]  [FaultContract(typeof(FaultException))]
        List<SySession> GetSessionsByCinema(string cinemaId);

        [OperationContract]  [FaultContract(typeof(FaultException))]
        List<SySession> GetSessionsBySessionBusinessDate(DateTime sessionBusinessDate);
        
        [OperationContract]  [FaultContract(typeof(FaultException))]
        List<SySession> GetSessionsByCinemaAndSessionBusinessDate(string cinemaId, DateTime sessionBusinessDate);

        [OperationContract]  [FaultContract(typeof(FaultException))]
        List<SySession> GetSessionsByScheduledFilmAndSessionBusinessDate(string scheduledFilmId,
            DateTime sessionBusinessDate);

        [OperationContract]  [FaultContract(typeof(FaultException))]
        List<SySession> GetSessionsByScheduledFilmAndSessionBusinessDateAndCity(string scheduledFilm, DateTime sessionBusinessDate, string city);
        [OperationContract]  [FaultContract(typeof(FaultException))]
        List<SyFilm> GetAllFilms();

        [OperationContract]  [FaultContract(typeof(FaultException))]
        List<SyFilm> GetFilmsByCinema(string cinemaId);

        [OperationContract]  [FaultContract(typeof(FaultException))]
        List<SyRestTicketType> GetTicketTypes(string cinemaid, string sessionid);

        [OperationContract]  [FaultContract(typeof(FaultException))]
        SySeatLayoutData GetRestAddTickets(string cinemaid, string sessionid, List<SyTicketType> syTicketTypes, bool processOrderValue = false,
            bool userSelectedSeatingSupported = true,
            bool skipAutoAllocation = true);

        [OperationContract]  [FaultContract(typeof(FaultException))]
        string CancelOrder(string userSessionId);

        [OperationContract]  [FaultContract(typeof(FaultException))]
        SyCompleteOrderResponse CompleteOrder(string userSessionId, int paymentValueCents, string bookingNotes, bool unpaidBooking,
                             string customerEmail, string customerPhone, string customerName);

        [OperationContract]  [FaultContract(typeof(FaultException))]
        string SySetSelectedSeats(string userSessionId, string cinemaId, string sessionId, List<SySelectedSeat> sySelectedSeats);
        [OperationContract]  [FaultContract(typeof(FaultException))]
        TaslinkOrderResponse GetTaslinkOrder(string amount, string uri);

        [OperationContract]  [FaultContract(typeof(FaultException))]
        TaslinkStatusResponse GetTaslinkStatus(string oid);

        [OperationContract]  [FaultContract(typeof(FaultException))]
        TaslinkReverseResponse GetTaslinkReverse(string oid);

        [OperationContract]  [FaultContract(typeof(FaultException))]
        List<SyCinema> GetCinemas();

        [OperationContract]  [FaultContract(typeof(FaultException))]
        string GetCinemaByCinemaID(string cinemaId);

        [OperationContract]  [FaultContract(typeof(FaultException))]
        List<SyCinema> GetCinemasByCity(string city);

        [OperationContract]  [FaultContract(typeof(FaultException))]
        string GetFilmTitleByScheduledFilmId(string scheduledFilmId);

        [OperationContract]
        [FaultContract(typeof(FaultException))]
        List<SyFilm> GetFilmsByScheduledFilmId(string scheduledFilmId);

        [OperationContract]  [FaultContract(typeof(FaultException))]
        List<DateTime> GetDistinctSessionDate();

        [OperationContract]  [FaultContract(typeof(FaultException))]
        System.IO.MemoryStream GeneratePdf(string printStream, int totalOrderCount);

        [OperationContract]  [FaultContract(typeof(FaultException))]
        List<string> GetCities();

        [OperationContract]  [FaultContract(typeof(FaultException))]
        List<SyFilm> GetFilmsByCity(string city);

        [OperationContract]  [FaultContract(typeof(FaultException))]
         List<DateTime> GetDistinctSessionDateByCity(string city);

        [OperationContract] [FaultContract(typeof(FaultException))]
        List<SyCinema> GetCinemasByCityAndScheduledFilmId(string city, string scheduledFilmId);


    }
}
