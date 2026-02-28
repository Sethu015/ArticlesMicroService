namespace Blocks.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToUnixEpochDate(this DateTime dateTime)
        {
            var unixEpochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)Math.Round((dateTime.ToUniversalTime() - unixEpochStart).TotalSeconds);
        }
    }
}
