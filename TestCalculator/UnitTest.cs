using System.Text;


namespace TestCalculator
{
    public class UnitTest
    {
        [Theory]
        [InlineData("1-(2,45-4)-5-(-1*2/3-(-(-5)+4))+81-26*1/2")]
        public void CalculatorTest(string value)
        {
            Calculator.CalcModel.Calculator calculator = new();

            string result =  calculator.Calculate(new StringBuilder(value)).ToString();

            Assert.Equal(Math.Round(double.Parse("75,21666667"), 5), Math.Round(double.Parse(result), 5));
        }

        [Theory]
        [InlineData("5/0")]
        [InlineData("-5/0")]
        public void InfinityTest(string value)
        {
            Calculator.CalcModel.Calculator calculator = new();

            string result = calculator.Calculate(new StringBuilder(value)).ToString();

            Assert.True(double.IsInfinity(double.Parse(result)));
        }

        [Theory]
        [InlineData("6,716566893818857e+16")]
        public void ErrorTest(string value)
        {
            Calculator.CalcModel.Calculator calculator = new();

            string result = calculator.Calculate(new StringBuilder(value)).ToString();

            Assert.Equal("ѕревышен предел допустимых значений", result);
        }
    }
}
