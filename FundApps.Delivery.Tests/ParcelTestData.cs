using System;
using System.Collections.Generic;
using System.Text;

namespace FundApps.Delivery.Tests
{
    public static class ParcelTestData
    {
        public static IReadOnlyCollection<ParcelSpecification> ParcelTypes = new List<ParcelSpecification>
        {
            new ParcelSpecification("Small", 10, 3),
            new ParcelSpecification("Medium", 50, 8),
            new ParcelSpecification("Large", 100, 15),
            new ParcelSpecification("Extra Large", int.MaxValue, 25),
        };
    }
}
