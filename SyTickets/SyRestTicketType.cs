using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SyTickets
{
  
    [DataContract]
    public class SyRestTicketType
    {
        [DataMember]
        public string AreaCategoryCode { get; set; }
        [DataMember]
        public string CinemaId { get; set; }
        [DataMember]
        public string Description { get; set; }
        //      public string DescriptionAlt { get; set; }
        //      public List<object> DescriptionTranslations { get; set; }
        [DataMember]
        public string DiscountsAvailable { get; set; }
        //      public int DisplaySequence { get; set; }
        //      public string HOPK { get; set; }
        //      public string HeadOfficeGroupingCode { get; set; }
        //      public bool IsAvailableAsLoyaltyRecognitionOnly { get; set; }
        //      public bool IsAvailableForLoyaltyMembersOnly { get; set; }
        [DataMember]
        public bool IsChildOnlyTicket { get; set; }
        //      public bool IsComplimentaryTicket { get; set; }
        //      public bool IsDynamicallyPriced { get; set; }
        //      public bool IsPackageTicket { get; set; }
        //      public bool IsRedemptionTicket { get; set; }
        //      public bool IsThirdPartyMemberTicket { get; set; }
        //      public string LongDescription { get; set; }
        //      public string LongDescriptionAlt { get; set; }
        //      public List<object> LongDescriptionTranslations { get; set; }
        [DataMember]
        public string LoyaltyBalanceTypeId { get; set; }
        [DataMember]
        public string LoyaltyPointsCost { get; set; }
        [DataMember]
        public int LoyaltyQuantityAvailable { get; set; }
        [DataMember]
        public string LoyaltyRecognitionId { get; set; }
        //      public object MaxServiceFeeInCents { get; set; }
        //[DataMember]
        //public PackageContent PackageContent { get; set; }
        //       public string PriceGroupCode { get; set; }
        [DataMember]
        public int PriceInCents { get; set; }
        //       public string ProductCodeForVoucher { get; set; }
        [DataMember]
        public int QuantityAvailablePerOrder { get; set; }
        [DataMember]
        public List<string> SalesChannels { get; set; }
        //      public int SurchargeAmount { get; set; }
        //      public string ThirdPartyMembershipName { get; set; }
        [DataMember]
        public string TicketTypeCode { get; set; }
        //      public object TotalTicketFeeAmountInCents { get; set; }
    }
}
