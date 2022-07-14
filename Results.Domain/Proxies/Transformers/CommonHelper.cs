namespace Results.Domain.Proxies.Transformers
{
    internal static class CommonHelper
    {
        public static string GetRoundNumber(string fileName)
        {
            var start = fileName.LastIndexOf('_') + 1;
            var end = fileName.LastIndexOf('.');

            return fileName.Substring(start, end - start);
        }

        public static DateTime GetRoundTime(string fileName)
        {
            return DateTime.Parse(fileName.Substring(0, 10));
        }
    }
}
