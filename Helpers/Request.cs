using DataLayer.Libs.Helpers.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpers.Helpers
{
    public class Request
    {
        public static void Create(string host, Dictionary<string, string> requestParams, string method, DownloadListener listener)
        {
            SimpleConnector connector = new SimpleConnector();
            connector.SetServerName(host);
            connector.SetParams(requestParams);
            connector.OnRequestComplete += listener;
            if(method==SimpleConnector.METHOD_GET)
            {
                connector.SendGet();
            } else {
                connector.SendPost();
            }
        }

        public static void Get(string host, Dictionary<string, string> requestParams, DownloadListener listener)
        {
            Create(host, requestParams, SimpleConnector.METHOD_GET, listener);
        }

        public static void Post(string host, Dictionary<string, string> requestParams, DownloadListener listener)
        {
            Create(host, requestParams, SimpleConnector.METHOD_POST, listener);
        }
    }
}
