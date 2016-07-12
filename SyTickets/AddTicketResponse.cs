using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SyTickets
{
    [DataContract] public class AdtTicket
    {
        [DataMember] public string AltDescription { get; set; }
        [DataMember] public string Barcode { get; set; }
        [DataMember] public string Description { get; set; }
        [DataMember] public object DescriptionTranslations { get; set; }
        [DataMember] public object DiscountPriceCents { get; set; }
        [DataMember] public List<object> DiscountsAvailable { get; set; }
        [DataMember] public string Id { get; set; }
        [DataMember] public bool IsTicketPackage { get; set; }
        [DataMember] public string LoyaltyRecognitionId { get; set; }
        [DataMember] public int LoyaltyRecognitionSequence { get; set; }
        [DataMember] public object PackageId { get; set; }
        [DataMember] public object PackageTickets { get; set; }
        [DataMember] public int PriceCents { get; set; }
        [DataMember] public object PromotionInstanceGroupNumber { get; set; }
        [DataMember] public object PromotionTicketTypeId { get; set; }
        [DataMember] public string SeatData { get; set; }
        [DataMember] public string SeatNumber { get; set; }
        [DataMember] public string SeatRowId { get; set; }
        [DataMember] public string TicketTypeCode { get; set; }
        [DataMember] public string TicketTypeHOPK { get; set; }
    }

    [DataContract] public class AdtSession
    {
        [DataMember] public bool AllocatedSeating { get; set; }
        [DataMember] public string AltFilmClassification { get; set; }
        [DataMember] public string AltFilmTitle { get; set; }
        [DataMember] public string CinemaId { get; set; }
        [DataMember] public string FilmClassification { get; set; }
        [DataMember] public string FilmTitle { get; set; }
        [DataMember] public List<object> FilmTitleTranslations { get; set; }
        [DataMember] public bool SeatsAllocated { get; set; }
        [DataMember] public int SessionId { get; set; }
        [DataMember] public DateTime ShowingRealDateTime { get; set; }
        [DataMember] public List<AdtTicket> Tickets { get; set; }
    }

    [DataContract] public class AdtOrder
    {
        [DataMember] public int BookingFeeValueCents { get; set; }
        [DataMember] public string CinemaId { get; set; }
        [DataMember] public object Concessions { get; set; }
        [DataMember] public object Customer { get; set; }
        [DataMember] public DateTime LastUpdated { get; set; }
        [DataMember] public List<AdtSession> Sessions { get; set; }
        [DataMember] public int TotalOrderCount { get; set; }
        [DataMember] public object TotalTicketFeeValueInCents { get; set; }
        [DataMember] public int TotalValueCents { get; set; }
        [DataMember] public string UserSessionId { get; set; }
        [DataMember] public int VistaBookingNumber { get; set; }
        [DataMember] public int VistaTransactionNumber { get; set; }
    }

    [DataContract] public class AdtAreaCategory
    {
        [DataMember] public string AreaCategoryCode { get; set; }
        [DataMember] public int SeatsAllocatedCount { get; set; }
        [DataMember] public int SeatsNotAllocatedCount { get; set; }
        [DataMember] public int SeatsToAllocate { get; set; }
        [DataMember] public List<SyPosition> SelectedSeats { get; set; }
    }

    [DataContract] public class AdtPosition
    {
        [DataMember] public int AreaNumber { get; set; }
        [DataMember] public int ColumnIndex { get; set; }
        [DataMember] public int RowIndex { get; set; }
    }

    [DataContract] public class AdtSeat
    {
        [DataMember] public string Id { get; set; }
        [DataMember] public int OriginalStatus { get; set; }
        [DataMember] public AdtPosition Position { get; set; }
        [DataMember] public int Priority { get; set; }
        [DataMember] public int SeatStyle { get; set; }
        [DataMember] public object SeatsInGroup { get; set; }
        [DataMember] public int Status { get; set; }
    }

    [DataContract] public class AdtRow
    {
        [DataMember] public string PhysicalName { get; set; }
        [DataMember] public List<AdtSeat> Seats { get; set; }
    }

    [DataContract] public class AdtArea
    {
        [DataMember] public string AreaCategoryCode { get; set; }
        [DataMember] public int ColumnCount { get; set; }
        [DataMember] public string Description { get; set; }
        [DataMember] public string DescriptionAlt { get; set; }
        [DataMember] public bool HasSofaSeatingEnabled { get; set; }
        [DataMember] public int Height { get; set; }
        [DataMember] public bool IsAllocatedSeating { get; set; }
        [DataMember] public double Left { get; set; }
        [DataMember] public int Number { get; set; }
        [DataMember] public int NumberOfSeats { get; set; }
        [DataMember] public int RowCount { get; set; }
        [DataMember] public List<AdtRow> Rows { get; set; }
        [DataMember] public int Top { get; set; }
        [DataMember] public int Width { get; set; }
    }

    [DataContract] public class AdtSeatLayoutData : IEnumerable
    {
        [DataMember] public List<AdtAreaCategory> AreaCategories { get; set; }
        [DataMember] public List<AdtArea> Areas { get; set; }
        [DataMember] public int BoundaryLeft { get; set; }
        [DataMember] public int BoundaryRight { get; set; }
        [DataMember] public int BoundaryTop { get; set; }
        [DataMember] public double ScreenStart { get; set; }
        [DataMember] public int ScreenWidth { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    [DataContract] public class AdtSessionStatus
    {
        [DataMember] public int MaximumNumberOfSeatsPerRow { get; set; }
        [DataMember] public int NumberOfSeatsAvailable { get; set; }
        [DataMember] public int SessionId { get; set; }
    }

    [DataContract] public class AddTicketResponse
    {
        [DataMember] public object ErrorDescription { get; set; }
        [DataMember] public int Result { get; set; }
        [DataMember] public string AreaSummaryData { get; set; }
        [DataMember] public int AvailableSeats { get; set; }
        [DataMember] public int ExtendedResultCode { get; set; }
        [DataMember] public int MaxSeatsPerRow { get; set; }
        [DataMember] public AdtOrder Order { get; set; }
        [DataMember] public List<object> RedemptionsRemainingForVouchers { get; set; }
        [DataMember] public string SeatData { get; set; }
        [DataMember] public int SeatDataLength { get; set; }
        [DataMember] public AdtSeatLayoutData SeatLayoutData { get; set; }
        [DataMember] public bool SeatsNotAllocated { get; set; }
        [DataMember] public List<AdtSessionStatus> SessionStatuses { get; set; }
    }
}