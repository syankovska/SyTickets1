using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SyTickets
{
    [DataContract]
    public class SySeatLayoutData
    {
        [DataMember]
        public List<SyAreaCategory> AreaCategories { get; set; }
        [DataMember]
        public List<SyArea> Areas { get; set; }
        [DataMember]
        public int BoundaryLeft { get; set; }
        [DataMember]
        public int BoundaryRight { get; set; }
        [DataMember]
        public int BoundaryTop { get; set; }
        [DataMember]
        public double ScreenStart { get; set; }
        [DataMember]
        public int ScreenWidth { get; set; }
        [DataMember]
        public int TotalOrderCount { get; set; }
        [DataMember]
        public int TotalValueCents { get; set; }
        [DataMember]
        public string UserSessionId { get; set; }
        [DataMember]
        public int VistaBookingNumber { get; set; }
        [DataMember]
        public string ErrorDescription { get; set; }
        [DataMember]
        public int Result { get; set; }
        [DataMember]
        public int AvailableSeats { get; set; }
        [DataMember]
        public int ExtendedResultCode { get; set; }
        public SySeatLayoutData()
        {
            Areas = new List<SyArea>();
        }
    }


    [DataContract]
    public class SyAreaCategory
    {
        [DataMember]
        public string AreaCategoryCode { get; set; }
        [DataMember]
        public int SeatsAllocatedCount { get; set; }
        [DataMember]
        public int SeatsNotAllocatedCount { get; set; }
        [DataMember]
        public int SeatsToAllocate { get; set; }
        [DataMember]
        public List<SyPosition> SelectedSeats { get; set; }
    }

    [DataContract]
    public class SyPosition
    {
        [DataMember]
        public int AreaNumber { get; set; }
        [DataMember]
        public int ColumnIndex { get; set; }
        [DataMember]
        public int RowIndex { get; set; }
    }

    [DataContract]
    public class SyArea
    {
        [DataMember]
        public string AreaCategoryCode { get; set; }
        [DataMember]
        public int ColumnCount { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string DescriptionAlt { get; set; }
        [DataMember]
        public bool HasSofaSeatingEnabled { get; set; }
        [DataMember]
        public int Height { get; set; }
        [DataMember]
        public bool IsAllocatedSeating { get; set; }
        [DataMember]
        public double Left { get; set; }
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public int NumberOfSeats { get; set; }
        [DataMember]
        public int RowCount { get; set; }
        [DataMember]
        public List<SyRow> Rows { get; set; }
        [DataMember]
        public int Top { get; set; }
        [DataMember]
        public int Width { get; set; }

        public SyArea()
        {
            Rows = new List<SyRow>();
        }
    }

    [DataContract]
    public class SyRow
    {
        [DataMember]
        public string PhysicalName { get; set; }
        [DataMember]
        public List<SySeat> Seats { get; set; }

        public SyRow()
        {
            Seats = new List<SySeat>();
        }
    }

    [DataContract]
    public class SySeat
    {
        [DataMember]
        public SyPosition Position { get; set; }
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public string Id { get; set; }
    }
}
