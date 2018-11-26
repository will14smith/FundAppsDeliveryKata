using System;
using System.Collections.Generic;
using System.Text;

namespace FundApps.Delivery
{
    public class ParcelPickerInput
    {
        public ParcelPickerInput(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; }
        public int Y { get; }
        public int Z { get; }
    }
}
