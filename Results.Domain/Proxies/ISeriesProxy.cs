using Results.Domain.Model;

namespace Results.Domain.Proxies
{
    public interface ISeriesProxy
    {
        IList<Serie> GetSeries();
    }
}
