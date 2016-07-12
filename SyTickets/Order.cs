using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using SyTickets.TicketingService;
using System.ServiceModel.Configuration;
using System.Configuration;
using XmlSerializer = System.Xml.Serialization.XmlSerializer;
using System.IO;
using System.Drawing;
using BarcodeLib;
using System.Drawing.Imaging;

namespace SyTickets
{
    public class Order
    {
        private Uri uri;
        public Order()
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

                uri = new Uri(el.Address.ToString());

            }
            catch (Exception)
            {
                uri = new Uri("http://dc1-vweb2-win12.multiplex.ua/WSVistaWebClient/TicketingService.svc");
            }
           
        }
        public string SyCancelOrder(string userSessionId)
        {

            TicketingServiceClient client = new TicketingServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(uri);


            CancelOrderRequest request = new CancelOrderRequest();
            request.UserSessionId = userSessionId;

            Response respone = client.CancelOrder(request);

            return respone.Result.ToString();
        }

        public SyCompleteOrderResponse SyCompleteOrder(string userSessionId, int paymentValueCents, string bookingNotes, bool unpaidBooking,
                                      string customerEmail, string customerPhone, string customerName)
        {

            TicketingServiceClient client = new TicketingServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(uri);

            var request = new CompleteOrderRequest();


            request.UserSessionId = userSessionId;
            request.BookingNotes = bookingNotes;
            PaymentInfo pi = new PaymentInfo();
            pi.PaymentValueCents = paymentValueCents;
            pi.PaymentSystemId = "-";
            pi.PaymentTenderCategory = "CREDIT";
            pi.BillFullOutstandingAmount = true;
            pi.CardBalance = 0;
            pi.CardNumber = "1111-1111-1111-1111";
            pi.SaveCardToWallet = false;
            pi.BillingValueCents = paymentValueCents;
            request.PaymentInfo = new PaymentInfo();
            request.PaymentInfo = pi;



  //          request.CustomerLanguageTag = 2;
            request.CustomerEmail = customerEmail;
            request.CustomerName = customerName;
            request.CustomerPhone = customerPhone;
            request.GeneratePrintStream = true;
            request.ReturnPrintStream = true;
            if (unpaidBooking)
                request.BookingMode = 1;
            else request.BookingMode = 0;
            request.PrintTemplateName = "WWW_P@H";
   
            request.BookingMode = 0;
            request.PrintStreamType = 1;
            request.GenerateConcessionVoucherPrintStream = false;


            CompleteOrderResponse respone = client.CompleteOrder(request);
            SyCompleteOrderResponse syCompleteOrderResponse = new SyCompleteOrderResponse();
            syCompleteOrderResponse.Result = respone.Result.ToString();
            syCompleteOrderResponse.PrintStream = respone.PrintStream;
            return (syCompleteOrderResponse);
        }

        public string SySetSelectedSeats(string userSessionId, string cinemaId, string sessionId, List<SySelectedSeat> sySelectedSeats)
        {

            TicketingServiceClient client = new TicketingServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(uri);

            var request = new SetSelectedSeatsRequest();


            request.UserSessionId = userSessionId;
            request.CinemaId = cinemaId;
            request.SessionId = sessionId;
            request.ReturnOrder = true;

            SelectedSeat1[] selectedSeats = new SelectedSeat1[sySelectedSeats.Count];

            int i=0;
            foreach (SySelectedSeat ss in sySelectedSeats)
            {
                SelectedSeat1 selectedSeat1 = new SelectedSeat1();
                selectedSeat1.AreaCategoryCode = ss.AreaCategoryCode;
                selectedSeat1.AreaNumber = ss.AreaNumber;
                selectedSeat1.ColumnIndex = ss.ColumnIndex;
                selectedSeat1.RowIndex = ss.RowIndex;
                selectedSeat1.AreaCategoryCode = ss.AreaCategoryCode;
                selectedSeats[i] = selectedSeat1;
                    i++;
            }
            request.SelectedSeats = selectedSeats;

            SetSelectedSeatsResponse respone = client.SetSelectedSeats(request);
            return respone.Result.ToString();
        }


        public System.IO.MemoryStream GeneratePdf (string printStream, int totalOrderCount) //System.IO.MemoryStream
        {
            printStream = printStream.Replace("|","");
            BarcodeLib.Barcode barcode = new BarcodeLib.Barcode()
            {
                   IncludeLabel = true,
                   Alignment = BarcodeLib.AlignmentPositions.CENTER,
                   Width = 300,
                   Height = 100,
                   RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                    BackColor = Color.White,
                    ForeColor = Color.Black,
             };

        Image img;
            string currDir = AppDomain.CurrentDomain.BaseDirectory;
           
                   Pdf pdf = new Pdf();

        XmlSerializer mySerializer =
        new XmlSerializer(typeof(Pdf));
        FileStream myFileStream =
        new FileStream(currDir + "\\templates\\PrintAtHome.xml", FileMode.Open);
        pdf = (Pdf)
            mySerializer.Deserialize(myFileStream);


            PdfDocument pdfDocument = new PdfDocument();
        pdfDocument.Info.Title = "Tickets";

            int currPageStart = 0;

            for (int t = 0; t<totalOrderCount; t++)
            { 
            PdfPage pdfPage = pdfDocument.AddPage();
        XGraphics graph = XGraphics.FromPdfPage(pdfPage);


        string[] printStreamSplit = printStream.Split('~');
        bool isNeedNewPage = true;
           
            for (int i = currPageStart; i<printStreamSplit.Length ; i++)
            {

                if (printStreamSplit[i].IndexOf("=")>0)
                { 
                string printStreamKey = printStreamSplit[i].Substring(0, printStreamSplit[i].IndexOf("=")).Trim();
        string printStreamValue = printStreamSplit[i].Substring(printStreamSplit[i].IndexOf("=") + 1);
                    
                        if (printStreamKey.Equals("txtBookingID"))
                    {
                        if (isNeedNewPage)
                            { 
                                isNeedNewPage = false;
                            }
                            else
                            {
                                isNeedNewPage = true;
                                currPageStart = i;
                                break;
                            }
                     }
                    
                                         
                     Textarea textArea = pdf.Page.Textarea.Find(item => item.Id.Contains(printStreamKey));
                    if (textArea != null)
                        if (printStreamKey.Equals("txtAddress") || printStreamKey.Equals("txtTransport"))
                        {
                            
                            printStreamValue = printStreamValue.Replace((char)92, '~');
                            printStreamValue = printStreamValue.Replace("~n", "~");

                            string[] printAddressSplit = printStreamValue.Split('~');

                            for (int j = 0; j<printAddressSplit.Length; j++)
                            {
                                Textarea subTextArea = pdf.Page.Textarea.Find(item => item.Id.Equals(printStreamKey + Convert.ToString(j + 1)));
                                if (subTextArea != null)
                                    subTextArea.Value = printAddressSplit[j];
                            }
                        }

                        else
                        {
                            textArea.Value = printStreamValue;
                        }
                     if (printStreamKey.Equals("bcdTicket"))  pdf.Page.Barcode.Value= printStreamValue.Trim();

                        PdfImage pdfImage = pdf.Page.Image.Find(item => item.Id.Contains(printStreamKey));
                        if (pdfImage != null)
                            pdfImage.Path = currDir+"\\"+printStreamValue.Replace("/","\\");
                    }
            }

          
           
            XGraphicsState state;
            foreach (PdfImage pdfImage in pdf.Page.Image)
            {

                state = graph.Save();
                XImage xImage = XImage.FromFile(pdfImage.Path);
                XRect rcImage = new XRect(Convert.ToDouble(pdfImage.X),
                Convert.ToDouble(pdfImage.Y), xImage.Width, xImage.Height);

                graph.DrawRectangle(XBrushes.Snow, rcImage);
                graph.DrawImage(xImage, rcImage);

                graph.Restore(state);
            }

            foreach (Textarea textArea in pdf.Page.Textarea)
            {
             
                state = graph.Save();
                XFont font = new XFont(textArea.Truetypefont, Convert.ToDouble(textArea.Fontsize),
                    XFontStyle.Regular);
                    graph.DrawString(textArea.Value, font, 
                    XBrushes.Black, 
                    new XRect(Convert.ToDouble(textArea.X), Convert.ToDouble(textArea.Y),
                    Convert.ToDouble(textArea.Width), Convert.ToDouble(textArea.Height)), XStringFormats.Center);
                graph.Restore(state);
            }

                Barcode barCode = pdf.Page.Barcode;
                img = barcode.Encode(TYPE.CODE128B, barCode.Value);
                    XImage xImageBC;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms, ImageFormat.Png);
                        xImageBC = XImage.FromStream (ms);
                     }

                

                    XRect rcImageBC = new XRect(Convert.ToDouble(barCode.X),
                        Convert.ToDouble(barCode.Y), Convert.ToDouble(barCode.WidthRatio) * Convert.ToDouble(xImageBC.Width),
                        Convert.ToDouble(barCode.Height));

                    graph.DrawRectangle(XBrushes.Snow, rcImageBC);
                    graph.DrawImage(xImageBC, rcImageBC); 

            }
            System.IO.MemoryStream stm = new System.IO.MemoryStream();
            pdfDocument.Save(stm);

         //  pdfDocument.Save("c:\\temp.pdf");


            return stm;
        }



    }
}



