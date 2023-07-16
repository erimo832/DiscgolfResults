namespace Results.Domain.Configuration
{
    internal class HcpConfiguration : IHcpConfiguration
    {
        public int HcpDecimals { get; set; } = 1;
        public int RoundsForHcp { get; set; } = 18;
    }
}
