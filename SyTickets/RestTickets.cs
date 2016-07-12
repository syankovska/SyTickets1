using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SyTickets
{
    [DataContract]
    public class PackageContent
    {
        public List<object> Concessions { get; set; }
        public List<object> Tickets { get; set; }
    }

    [DataContract]
    public class RestTicket
    {
        [DataMember]
        public string AreaCategoryCode { get; set; }
        [DataMember]
        public string CinemaId { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string DescriptionAlt { get; set; }
        [DataMember]
        public List<object> DescriptionTranslations { get; set; }
        [DataMember]
        public object DiscountsAvailable { get; set; }
        [DataMember]
        public int DisplaySequence { get; set; }
        [DataMember]
        public string HOPK { get; set; }
        [DataMember]
        public string HeadOfficeGroupingCode { get; set; }
        [DataMember]
        public bool IsAvailableAsLoyaltyRecognitionOnly { get; set; }
        [DataMember]
        public bool IsAvailableForLoyaltyMembersOnly { get; set; }
        [DataMember]
        public bool IsChildOnlyTicket { get; set; }
        [DataMember]
        public bool IsComplimentaryTicket { get; set; }
        [DataMember]
        public bool IsDynamicallyPriced { get; set; }
        [DataMember]
        public bool IsPackageTicket { get; set; }
        [DataMember]
        public bool IsRedemptionTicket { get; set; }
        [DataMember]
        public bool IsThirdPartyMemberTicket { get; set; }
        [DataMember]
        public string LongDescription { get; set; }
        [DataMember]
        public string LongDescriptionAlt { get; set; }
        [DataMember]
        public List<object> LongDescriptionTranslations { get; set; }
        [DataMember]
        public object LoyaltyBalanceTypeId { get; set; }
        [DataMember]
        public object LoyaltyPointsCost { get; set; }
        [DataMember]
        public int LoyaltyQuantityAvailable { get; set; }
        [DataMember]
        public object LoyaltyRecognitionId { get; set; }
        [DataMember]
        public object MaxServiceFeeInCents { get; set; }
        [DataMember]
        public PackageContent PackageContent { get; set; }
        [DataMember]
        public string PriceGroupCode { get; set; }
        [DataMember]
        public int PriceInCents { get; set; }
        [DataMember]
        public string ProductCodeForVoucher { get; set; }
        [DataMember]
        public int QuantityAvailablePerOrder { get; set; }
        [DataMember]
        public List<string> SalesChannels { get; set; }
        [DataMember]
        public int SurchargeAmount { get; set; }
        [DataMember]
        public string ThirdPartyMembershipName { get; set; }
        [DataMember]
        public string TicketTypeCode { get; set; }
        [DataMember]
        public object TotalTicketFeeAmountInCents { get; set; }
    }

    [DataContract]
    public class RestTickets
    {
        [DataMember]
        public int ResponseCode { get; set; }
        [DataMember]
        public List<RestTicket> Tickets { get; set; }
    }
}
