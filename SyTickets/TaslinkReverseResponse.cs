using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SyTickets
{
    [DataContract]
    public class TaslinkReverseResponse
    {
        [DataMember]
        public string oid { get; set; }

        [DataMember]
        public string tranid { get; set; }

        [DataMember]
        public string revrespcode { get; set; }

        [DataMember]
        public string sign { get; set; }
    }
}

