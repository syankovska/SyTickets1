using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyTickets.TicketingService;

namespace SyTickets
{
    public class TicketsRoutings
    {
        public string SyCancelOrder(string userSessionId)
        {

            TicketingServiceClient client = new TicketingServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress("http://dc1-viis-win12.multiplex.ua/WSVistaWebClient/TicketingService.asmx");


            CancelOrderRequest request = new CancelOrderRequest();
            request.UserSessionId = userSessionId;

            CancelOrderResponse respone = client.CancelOrder(request);



            return respone.;
        }
    }
}



//var request = new CompleteOrderRequest();


//request.UserSessionId = "ef022cdfa69d461d9ea86dd4c0264f4e";

//            PaymentInfo pi = new PaymentInfo();
//pi.PaymentValueCents = 9000;
//            pi.PaymentSystemId = "-";
//            pi.PaymentTenderCategory = "CREDIT";
//            pi.BillFullOutstandingAmount = true;
//            pi.CardBalance = 0;
//            pi.SaveCardToWallet = false;

//            request.PaymentInfo = new PaymentInfo();
//request.PaymentInfo = pi;
           



//            request.CustomerEmail = "yankovskaya@gmail.com";
//            request.GeneratePrintStream = true;
//            request.ReturnPrintStream = true;
//            request.UnpaidBooking = false;
//            request.PrintTemplateName = "WWW_P@H";
//            request.BookingMode = 0;
//            request.PrintStreamType = 0;
//            request.GenerateConcessionVoucherPrintStream = false;


//            CompleteOrderResponse respone = client.CompleteOrder(request);