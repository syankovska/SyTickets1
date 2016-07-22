using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SyTickets
{
    [DataContract]
public class SyAddTicketsRequest
{
    public SyAddTicketsRequest()
    {
        TicketTypes = new List<SyTicketType>();
    }
    [DataMember]
    public string UserSessionId { get; set; }

    [DataMember]
    public string CinemaId { get; set; }

    [DataMember]
    public string SessionId { get; set; }

    [DataMember]
    public List<SyTicketType> TicketTypes;

    
    [DataMember]
    public bool ReturnOrder { get; set; }

    [DataMember]
    public bool ReturnSeatData { get; set; }

    [DataMember]
    public bool ProcessOrderValue { get; set; }

    [DataMember]
    public bool UserSelectedSeatingSupported { get; set; }

    [DataMember]
    public bool SkipAutoAllocation { get; set; }
    [DataMember]
    public bool IncludeSeatNumbers { get; set; }
    [DataMember]
    public int ReturnSeatDataFormat { get; set; }
      //  [DataMember]
      //public int BookingMode { get; set; }
    }

[DataContract]
public class SyTicketType
{
    [DataMember]
    public string TicketTypeCode { get; set; }

    [DataMember]
    public int Qty { get; set; }

    [DataMember]
    public int PriceInCents { get; set; }
}
}

