using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp.Deserializers;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace SyTickets
{
    public class RestTicketTypes
    {
        private Uri uri;

        public RestTicketTypes()
        {
            //Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            //ServiceModelSectionGroup serviceModelSectionGroup = ServiceModelSectionGroup.GetSectionGroup(configuration);
            //ClientSection clientSection = serviceModelSectionGroup.Client;
            //var el = clientSection.Endpoints[0];
            //Uri myUri = new Uri(el.Address.ToString());
            //string endPoint = "http://" + myUri.Host + "/WSVistaWebClient/RESTData.svc";

            //uri = new Uri(@endPoint);


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
                string endPoint = "http://" + myUri.Host + "/WSVistaWebClient/RESTData.svc";

                uri = new Uri(@endPoint);
            }
            catch (Exception)
            {
                uri = new Uri("http://dc1-vweb2-win12.multiplex.ua/WSVistaWebClient/RESTData.svc");
            }
       }

        public List<SyRestTicketType> GetTicketTypes(string cinemaid, string sessionid)
        {
            var client = new RestSharp.RestClient(uri);
            var request = new RestRequest("/cinemas/{cinemaId}/sessions/{sessionId}/tickets", Method.GET);
            request.AddUrlSegment("cinemaId", cinemaid);
            request.AddUrlSegment("sessionId", sessionid);

            IRestResponse response = client.Execute(request);
            var rt = new RestTickets();
            JsonDeserializer deserializer = new JsonDeserializer();
            rt = deserializer.Deserialize<RestTickets>(response);


               List<RestTicket> lsTicketRaw =
                (from t in rt.Tickets
                 select t).ToList();

            return syRestTicketTypes(lsTicketRaw);
        }

        public List<SyRestTicketType> GetAddTickes(string cinemaid, string sessionid)
        {
            var client = new RestSharp.RestClient(uri);
            var request = new RestRequest("/cinemas/{cinemaId}/sessions/{sessionId}/tickets", Method.GET);
            request.AddUrlSegment("cinemaId", cinemaid);
            request.AddUrlSegment("sessionId", sessionid);

            IRestResponse response = client.Execute(request);
            var rt = new RestTickets();
            JsonDeserializer deserializer = new JsonDeserializer();
            rt = deserializer.Deserialize<RestTickets>(response);


            List<RestTicket> lsTicketRaw =
             (from t in rt.Tickets
              select t).ToList();

            return syRestTicketTypes(lsTicketRaw);
        }
        private List<SyRestTicketType> syRestTicketTypes(List<RestTicket> lsTicketRaw)
        {
            List<SyRestTicketType> lsRestTicketTypes =
              (from t in lsTicketRaw

               select new SyRestTicketType()
               {
                   CinemaId = t.CinemaId
                   ,AreaCategoryCode = t.AreaCategoryCode
                   ,Description = t.Description
                   ,DiscountsAvailable = Convert.ToString(t.DiscountsAvailable)
                   ,IsChildOnlyTicket = t.IsChildOnlyTicket
                   ,LoyaltyBalanceTypeId = Convert.ToString(t.LoyaltyBalanceTypeId)
                   ,LoyaltyPointsCost = Convert.ToString(t.LoyaltyPointsCost)
                   ,LoyaltyQuantityAvailable = t.LoyaltyQuantityAvailable
                   ,LoyaltyRecognitionId = Convert.ToString(t.LoyaltyRecognitionId)
                   ,PriceInCents = t.PriceInCents
                   ,QuantityAvailablePerOrder = t.QuantityAvailablePerOrder
                   ,SalesChannels=t.SalesChannels
                   ,TicketTypeCode = t.TicketTypeCode
               }).ToList();

            List<SyRestTicketType> lsRestTicketTypesWWW =
            lsRestTicketTypes.Where(x => x.SalesChannels.Any(y => y.Equals("WWW"))).ToList();
            return lsRestTicketTypesWWW;
        }
    }
}

