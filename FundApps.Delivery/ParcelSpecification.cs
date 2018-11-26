using System;
using System.Collections.Generic;
using System.Text;

namespace FundApps.Delivery
{
    public class ParcelSpecification
    {
        public ParcelSpecification(string name, int maxDimension, decimal price)
        {
            Name = name;
            MaxDimension = maxDimension;
            Price = price;
        }

        public string Name { get; }
        public int MaxDimension { get; }
        public decimal Price { get; }
    }
}
