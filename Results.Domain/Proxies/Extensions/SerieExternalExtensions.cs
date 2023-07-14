using Results.Domain.Model;
using Results.Domain.Proxies.Contracts;

namespace Results.Domain.Proxies.Extensions
{
    internal static class SerieExternalExtensions
    {
        public static IList<Serie> ToInternal(this IList<SerieExternal> series)
        {
            var result = new List<Serie>();

            foreach (var serie in series)
            {
                result.Add(new Serie
                {
                    SerieId = serie.SerieId,
                    Name = serie.Name,
                    RoundsToCount = serie.RoundsToCount,
                });
            }

            return result;
        }
    }
}
