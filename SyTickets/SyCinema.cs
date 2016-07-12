using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SyTickets
{
    [DataContract]
    public class SyCinema
    {
        [DataMember]
        public string ID { get; set; }

        //[DataMember] public string CinemaNationalId { get; set; }
        [DataMember] public string Name { get; set; }
        [DataMember] public string NameAlt { get; set; }
        [DataMember] public string PhoneNumber { get; set; }
        //[DataMember] public string EmailAddress { get; set; }
        [DataMember] public string Address1 { get; set; }
        [DataMember] public string Address2 { get; set; }
        [DataMember] public string City { get; set; }
        //[DataMember] public string Latitude { get; set; }
        //[DataMember] public string Longitude { get; set; }
        [DataMember] public string ParkingInfo { get; set; }
        [DataMember] public string LoyaltyCode { get; set; }
     //   [DataMember] public bool IsGiftStore { get; set; }
        [DataMember] public string Description { get; set; }
        //[DataMember] public object DescriptionAlt { get; set; }
        [DataMember] public string PublicTransport { get; set; }
        //[DataMember] public string CurrencyCode { get; set; }
        //[DataMember] public bool AllowPrintAtHomeBookings { get; set; }
        //[DataMember] public bool AllowOnlineVoucherValidation { get; set; }
        //[DataMember] public bool DisplaySofaSeats { get; set; }
        //[DataMember] public string TimeZoneId { get; set; }
        [DataMember] public string HOPK { get; set; }
        //[DataMember] public List<object> NameTranslations { get; set; }
        //[DataMember] public List<object> DescriptionTranslations { get; set; }
        //[DataMember] public List<object> ParkingInfoTranslations { get; set; }
        //[DataMember] public List<object>  PublicTransportTranslations { get; set; }
        //[DataMember] public object ServerName { get; set; }
    }

}


