using System;

namespace DataLayer.Libs.Helpers
{
    public class DateHelpers
    {
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static int DateTimeToUnitTimeStamp(DateTime date_time)
        {
            return (Int32)(date_time.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
