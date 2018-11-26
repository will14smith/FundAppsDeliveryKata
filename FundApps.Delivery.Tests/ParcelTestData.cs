using System;
using System.Collections.Generic;
using System.Text;

namespace FundApps.Delivery.Tests
{
    public static class ParcelTestData
    {
        public static IReadOnlyCollection<ParcelSpecification> ParcelTypes = new List<ParcelSpecification>
        {
            new ParcelSpecification("Small", 10, 1, 3, 2),
            new ParcelSpecification("Medium", 50, 3, 8, 2),
            new ParcelSpecification("Large", 100, 6, 15, 2),
            new ParcelSpecification("Extra Large", int.MaxValue, 10, 25, 2),
            new ParcelSpecification("Heavy", int.MaxValue, 50, 50, 1),
        };
    }
}
