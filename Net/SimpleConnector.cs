using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace DataLayer.Libs.Helpers.Net
{
    public delegate void DownloadListener(string response);
    public class SimpleConnector
    {
        /**
         * Method GET
         */
        public const string METHOD_GET = "GET";

        /**
         * Method POST
         */
        public const string METHOD_POST = "POST";

        /**
         * Extra params
         */
        public List<KeyValuePair<string, string>> ExtraParams;

        /**
         * Request params
         */
        protected Dictionary<string, string> Params;

        public DownloadListener OnRequestComplete;

        protected string ServerName;
        protected Uri UriObject;

        public string Method = "POST";

        public SimpleConnector()
        {
            this.Params = new Dictionary<string, string>();
            this.ExtraParams = new List<KeyValuePair<string, string>>();
        }

        public void SetServerName(string _server_name)
        {
            this.ServerName = _server_name;
        }

        public string GetServerName()
        {
            return this.ServerName;
        }

        public void Send()
        {
            WebRequest request = WebRequest.Create(this.GetServerName());
            request.Method = this.Method;
            request.BeginGetRequestStream(streamCallback, request);
        }

        public void SendGet()
        {
            this.Method = "GET";
            this.Send();
        }

        public void SendPost()
        {
            this.Method = "POST";
            this.Send();
        }

        private void streamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

            string postData = StringHelper.URLEncode(this.Params);

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postData);

            using (Stream postStream = request.EndGetRequestStream(asynchronousResult))
            {
                postStream.Write(byteArray, 0, postData.Length);
            }

            request.BeginGetResponse(new AsyncCallback(responseCallback), request);
        }

        private void responseCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
            Stream streamResponse = response.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);
            string responseString = streamRead.ReadToEnd();

            this.OnRequestComplete.Invoke(responseString);
        }

        protected List<KeyValuePair<string, string>> PrepareParams()
        {
            for (int i = 0; i < this.Params.Keys.Count; i++)
            {
                var key = this.Params.Keys.ElementAt(i);
                var value = this.Params[key].ToString();

                this.ExtraParams.Add(new KeyValuePair<string, string>(key, value));
            }

            return this.ExtraParams;
        }

        //protected string PrepareRequest()
        //{
        //    string address = "";
        //    for (int i = 0; i < this.Params.Keys.Count; i++)
        //    {
        //        var key = this.Params.Keys.ElementAt(i);
        //        var value = this.Params[key];

        //        if (address != "")
        //        {
        //            address += "&";
        //        }

        //        address += key + "=" + value;
        //    }


        //    return address;
        //}

        public void SetParams(Dictionary<string, string> _params)
        {
            this.Params = _params;
        }

        public Dictionary<string, string> GetParams()
        {
            return this.Params;
        }

        public void AddParam(string _index, string _value)
        {
            this.Params.Add(_index, _value);
        }

        public void RemoveParam(string _index)
        {
            this.Params.Remove(_index);
        }
    }
}