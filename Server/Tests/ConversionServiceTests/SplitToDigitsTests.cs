using API.Services;

namespace Tests.ConversionServiceTests
{
    [TestClass]
    public class SplitToDigitsTests
    {
        [TestMethod]
        [DataRow(0.01, 0, 0, 0, 1)]
        [DataRow(0.5, 0, 0, 0, 50)]
        [DataRow(1, 0, 0, 1, 0)]
        [DataRow(15, 0, 0, 15, 0)]
        [DataRow(215, 0, 0, 215, 0)]
        [DataRow(10_215, 0, 10, 215, 0)]
        [DataRow(50_10_215, 5, 10, 215, 0)]
        [DataRow(999_999_999.99, 999, 999, 999, 99)]
        public void WhenSplitInput_ThenReturnExpected(
            double input, int millions, int thousand, int ones, int fractions)
        {
            var service = new ConversionService();
            var obj = service.SplitToDigits(Convert.ToDecimal(input));

            Assert.AreEqual(millions, obj.Millions);
            Assert.AreEqual(thousand, obj.Thousands);
            Assert.AreEqual(ones, obj.Ones);
            Assert.AreEqual(fractions, obj.Fractions);
        }
    }
}