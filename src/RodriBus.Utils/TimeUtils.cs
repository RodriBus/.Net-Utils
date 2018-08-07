using System;
using System.Collections.Generic;
using System.Text;

namespace RodriBus.Utils
{
    /// <summary>
    /// Contains utils related to common time operations.
    /// </summary>
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

        /// <summary>
        /// Calculates the age of a given birthday related to a reference date.
        /// If the reference is lower than the birthday the age returned resembles a negative age.
        /// </summary>
        /// <remarks>
        /// The calculation does not count days but years, as in a legal way of considering peoples ages.
        /// With this in mind, at the exact day of your birthday at 0h 0m 0s you will get one year older.
        /// </remarks>
        /// <param name="reference">The reference date</param>
        /// <param name="birthday">The birthday date</param>
        /// <returns></returns>
        public static int GetAge(DateTime reference, DateTime birthday)
        {
            var age = reference.Year - birthday.Year;
            if (reference < birthday.AddYears(age)) age--;

            return age;
        }
    }
}
