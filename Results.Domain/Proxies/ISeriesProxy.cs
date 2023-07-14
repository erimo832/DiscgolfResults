using Results.Domain.Model;
using Results.Domain.Proxies.Contracts;

namespace Results.Domain.Proxies
{
    public interface ISeriesProxy
    {
        SeriesListExternal GetSeries();
    }
}
