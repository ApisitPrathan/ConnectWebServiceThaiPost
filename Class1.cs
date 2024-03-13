using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.IO;
using System.Net;
using ConnectWebServiceThaiPost.Model;
using Newtonsoft.Json;
using System.Configuration;

namespace ConnectWebServiceThaiPost
{
    public class ConnectThaiPost
    {
  

        public string Username = ConfigurationManager.AppSettings["Username"].ToString(); 
        public string Password = ConfigurationManager.AppSettings["Password"].ToString();  
        public string token = GetToken();


        private static string static_Token = ConfigurationManager.AppSettings["Static_Token"].ToString();
        private static string URLGet_Item = ConfigurationManager.AppSettings["URLGet_Item"].ToString();
        private static string URLGet_Token = ConfigurationManager.AppSettings["URLGet_Token"].ToString();
        //private const string DATA = @"{
                                                
        //                               ""status"": ""all"",
        //                               ""language"": ""TH"",
        //                               ""barcode"": ['EK068839668TH']
        //                               }";


        public System.Data.DataTable GetDataFromThaiPost(string Barcode)
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();
            string xmlKey = rsa.ToXmlString(false);
            string xmlPrivKey = rsa.ToXmlString(true);

            th.co.thailandpost.track.TrackandTraceService sParams = new th.co.thailandpost.track.TrackandTraceService();

            sParams.Url = @"https://trackapisoap.thailandpost.co.th/TTPOSTWebService/TrackandTrace.wsdl";           
             
            sParams.PublicKeySoapHeaderValue = new th.co.thailandpost.track.PublicKeySoapHeader();
            sParams.PublicKeySoapHeaderValue.PublicXmlKey = xmlKey;
            

            th.co.thailandpost.track.TrackItem _ItemData = sParams.GetItems(Username, Password, "TH", Barcode);

            string TrackStatus = string.Empty;
            string CountNumber = string.Empty;
            string CountBarcode = string.Empty;


            if (_ItemData.ItemsData.Length > 0)
            {
                TrackStatus = _ItemData.TrackCountData.TrackStatus;
                CountNumber = _ItemData.TrackCountData.CountNumber;
                CountBarcode = _ItemData.TrackCountData.TrackCountLimit;
            }

            System.Data.DataTable dt = new System.Data.DataTable();
            if (_ItemData.TrackCountData.TrackStatus == "Successfull")
            {
                
                dt.Columns.Add("Barcode", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("Location", typeof(string));
                dt.Columns.Add("DateTime", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("DeliveryStatus", typeof(string));
                dt.Columns.Add("DeliveryDateTime", typeof(string));
                dt.Columns.Add("Signature", typeof(string));
                dt.Columns.Add("PostCode", typeof(string));
                dt.Columns.Add("TrackStatus", typeof(string));
                dt.Columns.Add("CountNumber", typeof(string));
                dt.Columns.Add("TrackCountLimit", typeof(string));
                for (int i = 0; i < _ItemData.ItemsData.Length; i++)
                {
                    dt.Rows.Add
                        (
                            _ItemData.ItemsData[i].Barcode,
                            _ItemData.ItemsData[i].StatusName,
                            _ItemData.ItemsData[i].Location,
                            _ItemData.ItemsData[i].DateTime,
                            _ItemData.ItemsData[i].Description,
                            _ItemData.ItemsData[i].DeliveryStatus,
                            _ItemData.ItemsData[i].DeliveryDateTime,
                            _ItemData.ItemsData[i].Signature,
                            _ItemData.ItemsData[i].PostCode,
                            TrackStatus,
                            CountNumber,
                            CountBarcode
                        );
                }
            }

            
            
            return dt;

        }

        public System.Data.DataTable GetDataFromThaiPost(string Barcode, string userName , string password)
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();
            string xmlKey = rsa.ToXmlString(false);
            string xmlPrivKey = rsa.ToXmlString(true);

            th.co.thailandpost.track.TrackandTraceService sParams = new th.co.thailandpost.track.TrackandTraceService();

            sParams.Url = @"https://trackapisoap.thailandpost.co.th/TTPOSTWebService/TrackandTrace.wsdl";

            sParams.PublicKeySoapHeaderValue = new th.co.thailandpost.track.PublicKeySoapHeader();
            sParams.PublicKeySoapHeaderValue.PublicXmlKey = xmlKey;

            th.co.thailandpost.track.TrackItem _ItemData = sParams.GetItems(userName, password, "TH", Barcode);

            string TrackStatus = string.Empty;
            string CountNumber = string.Empty;
            string CountBarcode = string.Empty;


            if (_ItemData.ItemsData.Length > 0)
            {
                TrackStatus = _ItemData.TrackCountData.TrackStatus;
                CountNumber = _ItemData.TrackCountData.CountNumber;
                CountBarcode = _ItemData.TrackCountData.TrackCountLimit;
            }

            System.Data.DataTable dt = new System.Data.DataTable();
            if (_ItemData.TrackCountData.TrackStatus == "Successfull")
            {

                dt.Columns.Add("Barcode", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("Location", typeof(string));
                dt.Columns.Add("DateTime", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("DeliveryStatus", typeof(string));
                dt.Columns.Add("DeliveryDateTime", typeof(string));
                dt.Columns.Add("Signature", typeof(string));
                dt.Columns.Add("PostCode", typeof(string));
                dt.Columns.Add("TrackStatus", typeof(string));
                dt.Columns.Add("CountNumber", typeof(string));
                dt.Columns.Add("TrackCountLimit", typeof(string));
                for (int i = 0; i < _ItemData.ItemsData.Length; i++)
                {
                    dt.Rows.Add
                        (
                            _ItemData.ItemsData[i].Barcode,
                            _ItemData.ItemsData[i].StatusName,
                            _ItemData.ItemsData[i].Location,
                            _ItemData.ItemsData[i].DateTime,
                            _ItemData.ItemsData[i].Description,
                            _ItemData.ItemsData[i].DeliveryStatus,
                            _ItemData.ItemsData[i].DeliveryDateTime,
                            _ItemData.ItemsData[i].Signature,
                            _ItemData.ItemsData[i].PostCode,
                            TrackStatus,
                            CountNumber,
                            CountBarcode
                        );
                }
            }



            return dt;

        }

        public DataTable GetDataFromThaiPost_RestAPI(string token,string trackItem)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Barcode", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Location", typeof(string));
            dt.Columns.Add("DateTime", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("DeliveryStatus", typeof(string));
            dt.Columns.Add("DeliveryDateTime", typeof(string));
            dt.Columns.Add("Signature", typeof(string));
            dt.Columns.Add("PostCode", typeof(string));
            //dt.Columns.Add("TrackStatus", typeof(string));
            dt.Columns.Add("CountNumber", typeof(string));
            dt.Columns.Add("TrackCountLimit", typeof(string));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URLGet_Item);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = trackItem.Length;
            request.Headers.Add("Authorization", "Token " + token);

            using (Stream webStream = request.GetRequestStream())
            using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            {
                requestWriter.Write(trackItem);
            }

            try
            {
                
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();

                    var deptObj = JsonConvert.DeserializeObject<dynamic>(response);
                    if (deptObj.message == "successful")
                    {
                        var track_count = deptObj.response.track_count;
                        foreach (var data in deptObj.response.items)
                        {
                            foreach (var att in data.Value)
                            {
                                dt.Rows.Add
                                    (
                                        att.barcode, //Barcode
                                        att.status_description, //StatusName
                                        att.location, //Location
                                        att.status_date, //DateTime
                                        att.delivery_description, //Description
                                        att.delivery_status, //DeliveryStatus
                                        att.delivery_datetime, //DeliveryDateTime
                                        att.receiver_name, //Signature
                                        att.postcode, //PostCode
                                                      //track_count.track_date,
                                        track_count.count_number, //CountNumber
                                        track_count.track_count_limit //CountBarcode
                                    );
                            }
                            //Console.WriteLine($"data: {att.barcode}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(e.Message);
            }
            return dt;
        }
        private static string GetToken()
        {
            string token = string.Empty;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URLGet_Token);
            request.Method = "POST";
            request.ContentType = "application/json";
            //request.ContentLength = DATA.Length;
            request.Headers.Add("Authorization", "Token " + static_Token);

            using (Stream webStream = request.GetRequestStream())
            using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            {
                requestWriter.Write(string.Empty);
            }

            try
            {
                
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    var Obj = JsonConvert.DeserializeObject<dynamic>(response);
                    token = Obj.token;
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(e.Message);
            }
            return token;
        }
    }
}
