using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SyTickets
{
    [DataContract]
    public class SySelectedSeat
    {
        [DataMember]
        public string TicketTypeCode { get; set; }

        [DataMember]
        public string AreaCategoryCode { get; set; }

        [DataMember]
        public int AreaNumber { get; set; }
        [DataMember]
        public int RowIndex { get; set; }
        [DataMember]
        public int ColumnIndex { get; set; }
    }
}


