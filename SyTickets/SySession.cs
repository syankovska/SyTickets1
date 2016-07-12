using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SyTickets
{
    [DataContract]
    public class SySession
    {

        [DataMember]
        public string ID { get; set; }

        [DataMember]
        public string CinemaId { get; set; }

        [DataMember]
        public string ScheduledFilmId { get; set; }

        [DataMember]
        public string SessionId { get; set; }

        [DataMember]
        public DateTime Showtime { get; set; }

     //   [DataMember]
     //   public bool IsAllocatedSeating { get; set; }

     //   [DataMember]
     //   public bool AllowChildAdmits { get; set; }

        [DataMember]
        public int SeatsAvailable { get; set; }

//        [DataMember]
  //      public string EventId { get; set; }

        [DataMember]
        public List<string> SessionAttributesNames
        {
            get; set; }

        [DataMember]
        public DateTime SessionBusinessDate { get; set; }

        [DataMember]
        public string CinemaName { get; set; }

        [DataMember]
        public string ScheduledFilmTitle { get; set; }

    }

 }
