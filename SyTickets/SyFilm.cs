using System;
using System.Runtime.Serialization;

namespace SyTickets
{
    [DataContract]
    public class SyFilm
    {
        [DataMember]
        public string ID { get; set; }

        [DataMember]
        public string ScheduledFilmId { get; set; }

        [DataMember]
        public string CinemaId { get; set; }

        //[DataMember]
        //public bool HasFutureSessions { get; set; }
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public DateTime? OpeningDate { get; set; }

        //[DataMember]
        //public string FilmHOPK { get; set; }
        //[DataMember]
        //public string FilmHOCode { get; set; }
        //[DataMember]
        //public string ShortCode { get; set; }
        //[DataMember]
        //public string RunTime { get; set; }
        [DataMember]
        public string TrailerUrl { get; set; }

  //      [DataMember]
   //     public object CinemaName { get; set; }

        [DataMember]
        public bool AllowTicketSales { get; set; }
    }

}
