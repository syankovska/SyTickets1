using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using RestSharp.Deserializers;
using SyTickets.TicketingService;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace SyTickets
{
    public class RestAddTickets
    {
        private Uri uri;

        public RestAddTickets()
        {

            try
            {
                ClientSection client = ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;
                ChannelEndpointElement el = null;

                for (int i = 0; i < client.Endpoints.Count; i++)
                {
                    if (client.Endpoints[i].Address.ToString().Contains("TicketingService"))
                        el = client.Endpoints[i];
                }

                Uri myUri = new Uri(el.Address.ToString());
                string endPoint = "http://" + myUri.Host + "/WSVistaWebClient/RESTTicketing.svc";

                uri = new Uri(@endPoint);
            }
            catch (Exception)
            {
                uri = new Uri("http://dc1-vweb2-win12.multiplex.ua/WSVistaWebClient/RESTTicketing.svc");
            }

             }



        public SySeatLayoutData GetRestAddTickets(string cinemaid, string sessionid, List<SyTicketType> syTicketTypes,
            bool processOrderValue ,
            bool userSelectedSeatingSupported,
            bool  skipAutoAllocation)
        {
            SySeatLayoutData sySeatLayoutData = new SySeatLayoutData();

            if (syTicketTypes != null && syTicketTypes.Count > 0)
            {
                sySeatLayoutData = GetRestAddTicketsAttempt(cinemaid, sessionid, syTicketTypes, processOrderValue,
                userSelectedSeatingSupported, skipAutoAllocation );
            }

          

            if (sySeatLayoutData.UserSessionId == null)
            {
                var tt = new RestTicketTypes();
                List<SyRestTicketType> restTicketType = tt.GetTicketTypes(cinemaid, sessionid);

                if (restTicketType != null)
                {
                    SyTicketType syTicketType = new SyTicketType();
                    syTicketType.PriceInCents = restTicketType[0].PriceInCents;
                    syTicketType.Qty = 1;
                    syTicketType.TicketTypeCode = restTicketType[0].TicketTypeCode;

                    List<SyTicketType> syTicketTypesOverride = new List<SyTicketType>();
                    syTicketTypesOverride.Add(syTicketType);
                    sySeatLayoutData = GetRestAddTicketsAttempt(cinemaid, sessionid, syTicketTypesOverride, processOrderValue,
                    userSelectedSeatingSupported, skipAutoAllocation);
                    if (sySeatLayoutData.UserSessionId != null)
                    {
                        sySeatLayoutData.ErrorDescription = "Please select your seats:";
                        sySeatLayoutData.TotalValueCents = 0;
                        sySeatLayoutData.TotalOrderCount = 0;
                        Order order = new Order();
                        var str = order.SyCancelOrder(sySeatLayoutData.UserSessionId);
                        foreach (var a in sySeatLayoutData.Areas)
                        {
                            for (int i = 0; i < a.RowCount; i++)
                            {
                                foreach (var seat in a.Rows[i].Seats) {
                                    if (seat.Status == 4)
                                        seat.Status = 0;
                                }

                            }
                        }


                    }

                    else  sySeatLayoutData.ErrorDescription = "Can not get order, check your selection";
                }
            }
                        return (sySeatLayoutData);
        }

       
       
        public SySeatLayoutData GetRestAddTicketsAttempt(string cinemaid, string sessionid, List<SyTicketType> syTicketTypes,
            bool processOrderValue,
            bool userSelectedSeatingSupported,
            bool skipAutoAllocation)
        {
     
            var atrequest = new SyAddTicketsRequest();
            atrequest.UserSessionId = Guid.NewGuid().ToString("N");
            atrequest.CinemaId = cinemaid;
            atrequest.SessionId = sessionid;
            atrequest.TicketTypes = syTicketTypes;
            atrequest.ReturnOrder = true;
            atrequest.ReturnSeatData = true;
            atrequest.ProcessOrderValue = processOrderValue;
            atrequest.UserSelectedSeatingSupported = userSelectedSeatingSupported;
            atrequest.SkipAutoAllocation = skipAutoAllocation;
            atrequest.IncludeSeatNumbers = true;
            atrequest.ReturnSeatDataFormat = 2;
          //  atrequest.BookingMode = 0;


        var client = new RestSharp.RestClient(uri);
            var request = new RestRequest("/order/tickets", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(atrequest);


            SySeatLayoutData sySeatLayoutData;
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == (HttpStatusCode)200)
            {
                var rt = new AddTicketResponse();
                JsonDeserializer deserializer = new JsonDeserializer();
                rt = deserializer.Deserialize<AddTicketResponse>(response);

                if ((rt.Result == 0)|| (rt.Result == 1) && rt.AvailableSeats>0)
                {
                    sySeatLayoutData = new SySeatLayoutData();
                    sySeatLayoutData.BoundaryLeft = rt.SeatLayoutData.BoundaryLeft;
                    sySeatLayoutData.BoundaryTop = rt.SeatLayoutData.BoundaryTop;
                    sySeatLayoutData.BoundaryRight = rt.SeatLayoutData.BoundaryRight;
                    sySeatLayoutData.ScreenStart = rt.SeatLayoutData.ScreenStart;
                    sySeatLayoutData.ScreenWidth = rt.SeatLayoutData.ScreenWidth;
                    sySeatLayoutData.TotalValueCents = rt.Order.TotalValueCents;
                    sySeatLayoutData.TotalOrderCount = rt.Order.TotalOrderCount;
                    sySeatLayoutData.UserSessionId = rt.Order.UserSessionId;
                    sySeatLayoutData.VistaBookingNumber = rt.Order.VistaBookingNumber;
                    sySeatLayoutData.ErrorDescription = Convert.ToString(rt.ErrorDescription);
                    sySeatLayoutData.Result = rt.Result;

                    List<AdtAreaCategory> lsAreaCategoriaRaw =
                        (from ac in rt.SeatLayoutData.AreaCategories
                         select ac).ToList();

                    sySeatLayoutData.AreaCategories = syAreaCategories(lsAreaCategoriaRaw);


                    List<AdtArea> lsAreaRaw =
                        (from ac in rt.SeatLayoutData.Areas
                        select ac).ToList();

                    foreach (var a in lsAreaRaw)
                    {
                        SyArea area = new SyArea();
                        area.AreaCategoryCode = a.AreaCategoryCode;
                        area.ColumnCount = a.ColumnCount;
                        area.Description = a.Description;
                        area.HasSofaSeatingEnabled = a.HasSofaSeatingEnabled;
                        area.Height = a.Height;
                        area.IsAllocatedSeating = a.IsAllocatedSeating;
                        area.Left = a.Left;
                        area.Number = a.Number;
                        area.NumberOfSeats = a.NumberOfSeats;
                        area.RowCount = a.RowCount;
                        area.Top = a.Top;
                        area.Width = a.Width;
                        foreach (var ar in a.Rows)
                        {
                            SyRow row = new SyRow();
                            row.PhysicalName = ar.PhysicalName;
                            foreach (var ars in ar.Seats)
                            {
                                SySeat seat = new SySeat();
                                seat.Status = ars.Status;
                                seat.Id = ars.Id;
                                SyPosition sp = new SyPosition();
                                sp.AreaNumber = ars.Position.AreaNumber;
                                sp.ColumnIndex = ars.Position.ColumnIndex;
                                sp.RowIndex = ars.Position.RowIndex;

                                seat.Position = sp;

                                row.Seats.Add(seat);
                            }
                            area.Rows.Add(row);
                        }
                        sySeatLayoutData.Areas.Add(area);
                    }

        }
                else
                {
                    sySeatLayoutData = new SySeatLayoutData();
                    sySeatLayoutData.ErrorDescription = Convert.ToString(rt.ErrorDescription) ;
                    sySeatLayoutData.Result = rt.Result;
                    sySeatLayoutData.AvailableSeats = rt.AvailableSeats;
                    sySeatLayoutData.ExtendedResultCode = rt.ExtendedResultCode;

                }
            }
            else
            {
                sySeatLayoutData = new SySeatLayoutData();
                sySeatLayoutData.ErrorDescription = response.StatusCode + response.StatusDescription;

            }

            return (sySeatLayoutData);
        }
        


       


        private List<SyAreaCategory> syAreaCategories(List<AdtAreaCategory> lsAreaCategoriaRaw)
        {
            List<SyAreaCategory> lsAreaCategories =
              (from t in lsAreaCategoriaRaw
                    select new SyAreaCategory()
                {
                    AreaCategoryCode = t.AreaCategoryCode,
                    SeatsAllocatedCount = t.SeatsAllocatedCount,
                    SeatsNotAllocatedCount = t.SeatsNotAllocatedCount,
                    SeatsToAllocate = t.SeatsToAllocate
                    ,SelectedSeats = t.SelectedSeats
                }).ToList();
         return lsAreaCategories;
        }
    }
}

