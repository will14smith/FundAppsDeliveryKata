namespace FundApps.Delivery
{
    public class ParcelSpecification
    {
        public ParcelSpecification(string name, int maxDimension, int maxWeight, decimal price, int weightSurcharge)
        {
            Name = name;
            MaxDimension = maxDimension;
            MaxWeight = maxWeight;
            Price = price;
            WeightSurcharge = weightSurcharge;
        }

        public string Name { get; }
        public int MaxDimension { get; }
        public int MaxWeight { get; }
        public decimal Price { get; }
        public int WeightSurcharge { get; }
    }
}
