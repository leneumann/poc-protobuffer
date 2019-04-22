using System;

namespace src
{
    public static class Extensions
    {
        public static double ToUnixTime(this DateTime input) => input.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

        public static string ToFormatedString(this TimeSpan input) => String.Format("{0:00}:{1:00}:{2:00}.{3:000}", input.Hours, input.Minutes, input.Seconds, input.Milliseconds);
    }
}