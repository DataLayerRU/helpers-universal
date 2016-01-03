using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataLayer.Libs.Helpers
{
    public class StringHelper
    {
        public static Dictionary<string, string> ParseQueryString(string uri)
        {
            Dictionary<string, string> output = new Dictionary<string, string>();

            if (uri != "")
            {
                string substring = uri.Substring(((uri.LastIndexOf('?') == -1) ? 0 : uri.LastIndexOf('?') + 1));

                string[] pairs = substring.Split('&');

                foreach (string piece in pairs)
                {
                    string[] pair = piece.Split('=');
                    output.Add(pair[0], pair[1]);
                }
            }

            return output;
        }

        public static string RandomString(int size)
        {
            Random random = new Random((int)DateTime.Now.Ticks);

            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static string URLEncode(Dictionary<string, string> paramCollection)
        {
            string address = "";
            for (int i = 0; i < paramCollection.Keys.Count; i++)
            {
                var key = paramCollection.Keys.ElementAt(i);
                var value = paramCollection[key];

                if (address != "")
                {
                    address += "&";
                }

                address += key + "=" + value;
            }


            return address;
        }
    }
}
