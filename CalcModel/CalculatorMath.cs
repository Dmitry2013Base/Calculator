using System;
using System.Linq;

namespace Calculator.CalcModel
{
    public partial class Calculator
    {
        private double Sum(double number1, double number2) => number1 + number2;

        private double Subtract(double number1, double number2) => number1 - number2;

        private double Multiplier(double number1, double number2) => number1 * number2;

        private double Division(double number1, double number2) => number1 / number2;

        private double Degree(double number1, double number2) => Math.Pow(number1, number2);

        private double Sin(double number, bool angleUnit) => angleUnit ? Math.Sin(number * Math.PI / 180.0) : Math.Sin(number);

        private double Cos(double number, bool angleUnit) => angleUnit ? Math.Cos(number * Math.PI / 180.0) : Math.Cos(number);

        private double Tan(double number, bool angleUnit) => angleUnit ? Math.Tan(number * Math.PI / 180.0) : Math.Tan(number);

        private double Factorial(double number, bool angleUnit) => (number < 0) ? double.NaN : Enumerable.Range(1, (int)number).Aggregate(1, (f, i) => f * i);

        private double Abs(double number, bool angleUnit) => Math.Abs(number);

        private void ParenthesesOpen() => coefficient += 100;

        private void ParenthesesClose() => coefficient -= 100;
    }
}
