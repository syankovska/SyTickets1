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

    }
}