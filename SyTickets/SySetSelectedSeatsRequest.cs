using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SyTickets
{
    [DataContract]
    public class SetSelectedSeatsRequest
    {
        public SetSelectedSeatsRequest()
        {
            SelectedSeats = new List<sySelectedSeats>();
        }
               
    

        [DataMember]
        public string UserSessionId { get; set; }

        [DataMember]
        public string CinemaId { get; set; }

        [DataMember]
        public string SessionId { get; set; }

        [DataMember]
        public bool ReturnOrder { get; set; }

        [DataMember]
        public List<sySelectedSeats> SelectedSeats;

   }

    [DataContract]
    public class sySelectedSeats
    {
        [DataMember]
        public string TicketTypeCode { get; set; }

        [DataMember]
        public string AreaCategoryCode { get; set; }

        [DataMember]
        public string AreaNumber { get; set; }
        [DataMember]
        public int RowIndex { get; set; }
        [DataMember]
        public int ColumnIndex { get; set; }
    }
}


