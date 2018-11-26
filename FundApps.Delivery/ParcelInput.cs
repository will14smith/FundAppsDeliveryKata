namespace FundApps.Delivery
{
    public class ParcelInput
    {
        public ParcelInput(int x, int y, int z, int weight)
        {
            X = x;
            Y = y;
            Z = z;
            Weight = weight;
        }

        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public int Weight { get; }
    }
}
