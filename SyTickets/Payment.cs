using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;

namespace SyTickets
{
    class Payment
    {
        private Uri uri;

        public enum Errors_ErrorCode
        {
   
[Description(" Удачное выполнение транзакции")]
            ErrorCode0 = 0,
[Description(" Удачное выполнение транзакции")]
            ErrorCode1 = 1,
[Description(" Транзакция выполнена, требуется дополнительная идентификация")]
            ErrorCode3 = 3,
[Description(" Административная транзакция выполнена успешно")]
            ErrorCode7 = 7,
[Description(" Финансовая транзакция не выполнена")]
            ErrorCode50 = 50,
[Description(" Код ошибки - 20. Неизвестная системная ошибка.")]
            ErrorCode20 = 20,
[Description(" Карта клиента просрочена")]
            ErrorCode51 = 51,
[Description(" Превышено число попыток ввода PIN")]
            ErrorCode52 = 52,
[Description(" Не удалось маршрутизировать транзакцию")]
            ErrorCode53 = 53,
[Description(" Транзакция имеет некорректные атрибуты или данная операция не разрешена")]
            ErrorCode55 = 55,
[Description(" Запрашиваемая операция не поддерживается хостом")]
            ErrorCode56 = 56,
[Description(" Карта клиента имеет статус 'потеряна' или 'украдена'")]
            ErrorCode57 = 57,
[Description(" Карта клиента имеет неправильный статус")]
            ErrorCode58 = 58,
[Description(" Карта клиента имеет ограниченные возможности")]
            ErrorCode59 = 59,
[Description(" Не найден вендор с указанным номером счета")]
            ErrorCode60 = 60,
[Description(" Неверное количество информационных полей для заданного вендора")]
            ErrorCode61 = 61,
[Description(" Неверный формат информационного поля платежа")]
            ErrorCode62 = 62,
[Description(" Не найден prepaid-код")]
            ErrorCode63 = 63,
[Description(" Track2 карты клиента содержит неверную информацию")]
            ErrorCode64 = 64,
[Description(" Неверный формат сообщения")]
            ErrorCode69 = 69,
[Description(" Невозможно авторизовать")]
            ErrorCode74 = 74,
[Description(" Неверный PAN карты")]
            ErrorCode75 = 75,
[Description(" На счете не хватает средств")]
            ErrorCode76 = 76,
[Description(" Произошло дублирование транзакции")]
            ErrorCode78 = 78,
[Description(" Превышение количества использований карты клиента")]
            ErrorCode82 = 82,
[Description(" Невозможно выдать баланс")]
            ErrorCode85 = 85,
[Description(" Превышение лимита по сумме")]
            ErrorCode95 = 95,
[Description(" Невозможно провести транзакцию")]
            ErrorCode100 = 100,
[Description(" Невозможно авторизовать – необходимо позвонить издателю карты")]
            ErrorCode101 = 101,
[Description(" Данный тип карт не поддерживается")]
            ErrorCode105 = 105,
[Description(" Неправильный счет клиента")]
            ErrorCode200 = 200,
[Description(" Неправильный PIN")]
            ErrorCode201 = 201,
[Description(" Некорректная сумма")]
            ErrorCode205 = 205,
[Description(" Неверный код транзакции")]
            ErrorCode209 = 209,
[Description(" Неверное значение CAVV")]
            ErrorCode210 = 210,
[Description(" Неверное значение CVV2")]
            ErrorCode211 = 211,
[Description(" Не найдена оригинальная транзакция для слипа")]
            ErrorCode212 = 212,
[Description(" Слип принимается повторно")]
            ErrorCode213 = 213,
[Description(" Ошибка формата")]
            ErrorCode800 = 800,
[Description(" Не найдена оригинальная транзакция для реверса")]
            ErrorCode801 = 801,
[Description(" Неверная операция закрытия периода")]
            ErrorCode809 = 809,
[Description(" Произошел тайм-аут")]
            ErrorCode810 = 810,
[Description(" Системная ошибка")]
            ErrorCode811 = 811,
[Description(" Неправильный идентификатор терминала")]
            ErrorCode820 = 820,
[Description(" Был послан последний пакет - прогрузка успешно завершена")]
            ErrorCode880 = 880,
[Description(" Предыдущий этап прогрузки был успешно выполнен –имеются еще данные")]
            ErrorCode881 = 881,
[Description(" Прогрузка терминала остановлена. Необходимо позвонить в процессинговый центр")]
            ErrorCode882 = 882,
[Description(" Получена неверная криптограмма в транзакции")]
            ErrorCode897 = 897,
[Description(" Получен неверный MAC")]
            ErrorCode898 = 898,
[Description(" Ошибка синхронизации")]
            ErrorCode899 = 899,
[Description(" Превышено число попыток ввода PIN. Требуется захват карты.")]
            ErrorCode900 = 900,
[Description(" Карта просрочена, требуется захват карты.")]
            ErrorCode901 = 901,
[Description(" Требуется захват карты")]
            ErrorCode909 = 909,
[Description(" Административная транзакция не поддерживается")]
            ErrorCode959 = 959,
[Description(" Системная ошибка. POS не отвечает")]
            ErrorCodemin1  = -1 
        }
        public Payment()
        {
            uri = new Uri("http://192.168.31.11:8008/Exec.php");
        }
        public TaslinkOrderResponse GetTaslinkOrder(string amount, string uriUI)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";

            string tranid;
            string termname = "000000020000001";
            string type = "GetOrder";

            //  string currUrl= Convert.ToString(System.ServiceModel.OperationContext.Current.RequestContext.RequestMessage.Headers.To);
            string currUrl = uriUI;
        //    string http = currUrl.Substring(0, currUrl.IndexOf("//") + 2);
            currUrl = currUrl.Replace("frmPayment", "callback");
            string back_url = currUrl;
           // string back_url = "test";

            string pass = "ASLINK_ORDER_REQ";
            string sign;

            tranid = Guid.NewGuid().ToString("N");
            tranid = System.Text.RegularExpressions.Regex.Replace(tranid, "[^0-9]", "");
            tranid = "t" + tranid.Substring(0, 8);

            sign = Reverse(tranid) + Reverse(termname) + Reverse(type) + Reverse(pass);


            byte[] encodedPassword = new UTF8Encoding().GetBytes(sign.ToUpper());
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encodedSign = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();




            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                string json = "{" + String.Format("\"tranid\":\"{0}\"," +
                                "\"termname\":\"{1}\"," +
                                "\"type\":\"{2}\"," +
                                "\"amount\":\"{3}\"," +
                             "\"back_url\":\"{4}\"," +
                                "\"sign\":\"{5}\"", tranid, termname, type, amount, back_url, encodedSign) + "}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            HttpWebResponse httpResponse = (HttpWebResponse)webRequest.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TaslinkOrderResponse));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(result));
            TaslinkOrderResponse taslinkOrderResponse = (TaslinkOrderResponse)ser.ReadObject(stream);

            // md5 (strtoupper(strrev($tranid). strrev($oid). strrev($PASSWORD) ));
            string signResponse = Reverse(tranid) + Reverse(taslinkOrderResponse.oid.ToString()) + Reverse(pass);
            encodedPassword = new UTF8Encoding().GetBytes(signResponse.ToUpper());
            hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encodedSignResp = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();


            return taslinkOrderResponse;
        }


        public TaslinkStatusResponse GetTaslinkStatus(string oid)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";

            string termname = "000000020000001";
            string type = "GetStatus";
            string pass = "ASLINK_ORDER_REQ";
            string sign;

            sign = Reverse(type) + Reverse(termname) + Reverse(oid) + Reverse(pass);


            byte[] encodedPassword = new UTF8Encoding().GetBytes(sign.ToUpper());
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encodedSign = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();


            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                string json = "{" + String.Format("\"termname\":\"{0}\"," +
                                "\"type\":\"{1}\"," +
                                "\"oid\":\"{2}\"," +
                                "\"sign\":\"{3}\"", termname, type, oid, encodedSign) + "}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            HttpWebResponse httpResponse = (HttpWebResponse)webRequest.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TaslinkStatusResponse));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(result));
            TaslinkStatusResponse taslinkStatusResponse = (TaslinkStatusResponse)ser.ReadObject(stream);

            //md5(strtoupper(strrev($oid).strrev($respcode).strrev($tranid).strrev($PASSWORD)));
            if (taslinkStatusResponse.oid != null)
            { 
            string signResponse = Reverse(taslinkStatusResponse.oid.ToString()) + Reverse(taslinkStatusResponse.respcode.ToString()) + Reverse(taslinkStatusResponse.tranid.ToString()) + Reverse(pass);
            encodedPassword = new UTF8Encoding().GetBytes(signResponse.ToUpper());
            hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encodedSignResp = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            }
            string defDesc = "";
            taslinkStatusResponse.respcode = GetDescription((Errors_ErrorCode)Convert.ToInt32(taslinkStatusResponse.respcode), defDesc);
            return taslinkStatusResponse;
        }

        public static string GetDescription(object enumValue, string defDesc)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            if (null != fi)
            {
                object[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return defDesc;
        }

        public TaslinkReverseResponse GetTaslinkReverse(string oid)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";

            string termname = "000000020000001";
            string type = "Reverse";
            string pass = "TASLINK_REV_REQ";
            string sign;

            sign = Reverse(type) + Reverse(termname) + Reverse(oid) + Reverse(pass);

            byte[] encodedPassword = new UTF8Encoding().GetBytes(sign.ToUpper());
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encodedSign = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();


            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                string json = "{" + String.Format("\"termname\":\"{0}\"," +
                                "\"type\":\"{1}\"," +
                                "\"oid\":\"{2}\"," +
                                "\"sign\":\"{3}\"", termname, type, oid, encodedSign) + "}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }



            HttpWebResponse httpResponse = (HttpWebResponse)webRequest.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TaslinkStatusResponse));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(result));
            TaslinkReverseResponse taslinkReverseResponse = (TaslinkReverseResponse)ser.ReadObject(stream);

            return taslinkReverseResponse;
         }

        private string Reverse(string text)
        {
            if (text == null) return null;

            // this was posted by petebob as well 
            char[] array = text.ToCharArray();
            Array.Reverse(array);
            return new String(array);
        }

    }
}