using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SyTickets
{
    [DataContract]
    public class TaslinkStatusResponse
    {
        [DataMember]
        public string oid { get; set; }

        [DataMember]
        public string tranid { get; set; }

        [DataMember]
        public string amount { get; set; }

        [DataMember]
        public string approval_code { get; set; }

        [DataMember]
        public string reverse { get; set; }

        [DataMember]
        public string respcode { get; set; }

        [DataMember]
        public string sign { get; set; }
    }
}
