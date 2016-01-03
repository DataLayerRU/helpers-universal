using System.Collections.Generic;

namespace DataLayer.Libs
{
    public class Session
    {
        private static Dictionary<string, object> flashes = new Dictionary<string, object>();

        public static void SetFlash(string key, object value)
        {
            if (flashes.ContainsKey(key))
            {
                flashes[key] = value;
            }
            else
            {
                flashes.Add(key, value);
            }
        }

        public static object GetFlash(string key, bool remove)
        {
            object result = null;

            if (flashes.ContainsKey(key))
            {
                result = flashes[key];

                if (remove)
                {
                    flashes.Remove(key);
                }
            }

            return result;
        }

        public static object GetFlash(string key)
        {
            return GetFlash(key, true);
        }
    }
}
