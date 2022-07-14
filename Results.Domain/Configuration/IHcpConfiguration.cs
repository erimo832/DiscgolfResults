namespace Results.Domain.Configuration
{
    internal interface IHcpConfiguration
    {
        double SlopeFactor { get; set; }
        int HcpDecimals { get; set; }
        int RoundsForHcp { get; set; }
    }
}
