using System;
using System.Collections.Generic;
using System.Text;

namespace RodriBus.Utils
{
    public static class TimeUtils
    {
        /// <summary>
        /// Converts <see cref="DateTime"/> instance into UNIX timestamp (total seconds elapsed since 1970-01-01 at 00:00:00:000).
        /// </summary>
        /// <remarks>It uses UTC for time calculations.</remarks>
        /// <param name="dateTime">The <see cref="DateTime"/> instance</param>
        /// <returns>Time in seconds</returns>
        public static int DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (int)(TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Utc) -
                   new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }

        /// <summary>
        /// Converts a UNIX timestamp into a <see cref="DateTime"/> instance.
        /// </summary>
        /// <remarks>It uses UTC for time calculations.</remarks>
        /// <param name="unixTimeStamp">Time in seconds</param>
        /// <returns>The <see cref="DateTime"/> instance</returns>
        public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }
    }
}
