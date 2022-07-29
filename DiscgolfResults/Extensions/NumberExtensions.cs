namespace DiscgolfResults.Extensions
{
    public static class NumberExtensions
    {
        public static double ToPercent(this double value, int decimals = -1)
        {
            if (decimals < 0)
                return value * 100.0;

            return Math.Round((value * 100.0), decimals);
        }

        public static int ToInt(this double value)
        {
            return Convert.ToInt32(value);
        }
    }
}
