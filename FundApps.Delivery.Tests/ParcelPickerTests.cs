using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FundApps.Delivery.Tests
{
    public class ParcelPickerTests
    {
        private readonly ParcelPicker _picker = new ParcelPicker(ParcelTestData.ParcelTypes);


        [Theory]

        [InlineData(9, 0, 0, "Small")]
        [InlineData(0, 9, 0, "Small")]
        [InlineData(0, 0, 9, "Small")]

        [InlineData(10, 0, 0, "Medium")]
        [InlineData(0, 10, 0, "Medium")]
        [InlineData(0, 0, 10, "Medium")]
        [InlineData(49, 0, 0, "Medium")]
        [InlineData(0, 49, 0, "Medium")]
        [InlineData(0, 0, 49, "Medium")]

        [InlineData(50, 0, 0, "Large")]
        [InlineData(0, 50, 0, "Large")]
        [InlineData(0, 0, 50, "Large")]
        [InlineData(99, 0, 0, "Large")]
        [InlineData(0, 99, 0, "Large")]
        [InlineData(0, 0, 99, "Large")]

        [InlineData(100, 0, 0, "Extra Large")]
        [InlineData(0, 100, 0, "Extra Large")]
        [InlineData(0, 0, 100, "Extra Large")]
        public void ShouldPickCorrectSizeParcel(int x, int y, int z, string expectedType)
        {
            var input = new ParcelPickerInput(x, y, z);

            var parcelType = _picker.Pick(input);

            Assert.Equal(expectedType, parcelType.Name);
        }
    }
}
