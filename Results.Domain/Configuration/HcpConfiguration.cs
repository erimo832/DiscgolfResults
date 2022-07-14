using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Domain.Configuration
{
    internal class HcpConfiguration : IHcpConfiguration
    {
        public double SlopeFactor { get; set; } = 0.8;
        public int HcpDecimals { get; set; } = 1;
        public int RoundsForHcp { get; set; } = 18;
    }
}
