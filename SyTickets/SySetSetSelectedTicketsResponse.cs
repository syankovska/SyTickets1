using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SyTickets
{
    [DataContract]
    public class SySetSelectedSeatsResponse
    {
        public SySetSelectedSeatsResponse()
        {
           // TicketTypes = new List<SyTicketType>();
        }
    }

        /*      <CinemaId>1001</CinemaId>
         <SessionId>4041</SessionId>
         <UserSessionId>ee93b1f639064934b23003ea0507a146</UserSessionId>
         <SeatData isNull = "false" />
         < ReturnOrder > True </ ReturnOrder >
         < SelectedSeats attr0="SelectedSeat1Array" isNull="false">


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
       }*/
    }


/*<SetSelectedSeats>
  <MethodParameters>
    <SetSelectedSeatsResponse>
      <Result>OK</Result>
      <Order>
        <UserSessionId>ee93b1f639064934b23003ea0507a146</UserSessionId>
        <CinemaId>1001</CinemaId>
        <TotalValueCents>29000</TotalValueCents>
        <BookingFeeValueCents>0</BookingFeeValueCents>
        <TotalOrderCount>4</TotalOrderCount>
        <Sessions attr0 = "SessionArray" isNull="false">
          <SessionArray0>
            <CinemaId>1001</CinemaId>
            <SessionId>4041</SessionId>
            <AllocatedSeating>True</AllocatedSeating>
            <SeatsAllocated>True</SeatsAllocated>
            <Tickets attr0 = "TicketArray" isNull="false">
              <TicketArray0>
                <Id>6594</Id>
                <TicketTypeCode>0001</TicketTypeCode>
                <TicketTypeHOPK>0001</TicketTypeHOPK>
                <PriceCents>5000</PriceCents>
                <Barcode isNull = "false" />
                < SeatData > 13 </ SeatData >
                < Description > GOOD </ Description >
                < AltDescription > GOOD </ AltDescription >
                < DescriptionTranslations isNull="true" />
                <IsTicketPackage>False</IsTicketPackage>
                <DiscountsAvailable attr0 = "ItemDiscountArray" isNull="false" />
                <PackageId isNull = "true" />
                < LoyaltyRecognitionId isNull="false" />
                <LoyaltyRecognitionSequence>0</LoyaltyRecognitionSequence>
                <PackageTickets isNull = "true" />
                < DiscountPriceCents isNull="true" />
                <PromotionTicketTypeId isNull = "true" />
                < PromotionInstanceGroupNumber isNull="true" />
                <SeatNumber>3</SeatNumber>
                <SeatRowId>1</SeatRowId>
              </TicketArray0>
              <TicketArray1>
                <Id>6595</Id>
                <TicketTypeCode>0001</TicketTypeCode>
                <TicketTypeHOPK>0001</TicketTypeHOPK>
                <PriceCents>5000</PriceCents>
                <Barcode isNull = "false" />
                < SeatData > 14 </ SeatData >
                < Description > GOOD </ Description >
                < AltDescription > GOOD </ AltDescription >
                < DescriptionTranslations isNull="true" />
                <IsTicketPackage>False</IsTicketPackage>
                <DiscountsAvailable attr0 = "ItemDiscountArray" isNull="false" />
                <PackageId isNull = "true" />
                < LoyaltyRecognitionId isNull="false" />
                <LoyaltyRecognitionSequence>0</LoyaltyRecognitionSequence>
                <PackageTickets isNull = "true" />
                < DiscountPriceCents isNull="true" />
                <PromotionTicketTypeId isNull = "true" />
                < PromotionInstanceGroupNumber isNull="true" />
                <SeatNumber>4</SeatNumber>
                <SeatRowId>1</SeatRowId>
              </TicketArray1>
              <TicketArray2>
                <Id>6596</Id>
                <TicketTypeCode>0003</TicketTypeCode>
                <TicketTypeHOPK>0003</TicketTypeHOPK>
                <PriceCents>9500</PriceCents>
                <Barcode isNull = "false" />
                < SeatData > 103 </ SeatData >
                < Description > SUPER LUX</Description>
                <AltDescription>SUPER LUX</AltDescription>
                <DescriptionTranslations isNull = "true" />
                < IsTicketPackage > False </ IsTicketPackage >
                < DiscountsAvailable attr0= "ItemDiscountArray" isNull= "false" />
                < PackageId isNull= "true" />
                < LoyaltyRecognitionId isNull= "false" />
                < LoyaltyRecognitionSequence > 0 </ LoyaltyRecognitionSequence >
                < PackageTickets isNull= "true" />
                < DiscountPriceCents isNull= "true" />
                < PromotionTicketTypeId isNull= "true" />
                < PromotionInstanceGroupNumber isNull= "true" />
                < SeatNumber > 3 </ SeatNumber >
                < SeatRowId > 10 </ SeatRowId >
              </ TicketArray2 >
              < TicketArray3 >
                < Id > 6597 </ Id >
                < TicketTypeCode > 0003 </ TicketTypeCode >
                < TicketTypeHOPK > 0003 </ TicketTypeHOPK >
                < PriceCents > 9500 </ PriceCents >
                < Barcode isNull= "false" />
                < SeatData > 104 </ SeatData >
                < Description > SUPER LUX</Description>
                <AltDescription>SUPER LUX</AltDescription>
                <DescriptionTranslations isNull = "true" />
                < IsTicketPackage > False </ IsTicketPackage >
                < DiscountsAvailable attr0= "ItemDiscountArray" isNull= "false" />
                < PackageId isNull= "true" />
                < LoyaltyRecognitionId isNull= "false" />
                < LoyaltyRecognitionSequence > 0 </ LoyaltyRecognitionSequence >
                < PackageTickets isNull= "true" />
                < DiscountPriceCents isNull= "true" />
                < PromotionTicketTypeId isNull= "true" />
                < PromotionInstanceGroupNumber isNull= "true" />
                < SeatNumber > 4 </ SeatNumber >
                < SeatRowId > 10 </ SeatRowId >
              </ TicketArray3 >
            </ Tickets >
            < FilmTitle > Він - дракон </ FilmTitle >
            < AltFilmTitle isNull= "false" />
            < FilmTitleTranslations attr0= "TranslationArray" isNull= "false" />
            < FilmClassification > R14 </ FilmClassification >
            < AltFilmClassification isNull= "false" />
            < ShowingRealDateTime > 16.05.2016 20:25:00</ShowingRealDateTime>
          </SessionArray0>
        </Sessions>
        <Concessions isNull = "true" />
        < Customer isNull= "true" />
        < VistaTransactionNumber > 0 </ VistaTransactionNumber >
        < VistaBookingNumber > 0 </ VistaBookingNumber >
        < LastUpdated > 16.05.2016 6:03:18</LastUpdated>
        <TotalTicketFeeValueInCents isNull = "true" />
      </ Order >
    </ SetSelectedSeatsResponse >
  </ MethodParameters >
</ SetSelectedSeats >*/
