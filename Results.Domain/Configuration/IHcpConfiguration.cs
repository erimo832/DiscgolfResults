namespace Results.Domain.Configuration
{
    internal interface IHcpConfiguration
    {
        int HcpDecimals { get; set; }
        int RoundsForHcp { get; set; }
    }
}
