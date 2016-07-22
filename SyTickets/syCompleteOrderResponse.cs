using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SyTickets
{
    [DataContract]
    public class SyCompleteOrderResponse
    {
        [DataMember]
        public string Result { get; set; }

        [DataMember]
        public string PrintStream { get; set; }
        [DataMember]
        public string VistaBookingId { get; set; }
        [DataMember]
        public string VistaBookingNumber { get; set; }
        [DataMember]
        public string VistaTransNumber { get; set; }
    }
}