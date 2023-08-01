using Calculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.CalcModel
{
    public partial class Calculator : ICalculator
    {
        private int coefficient = 0;

        public List<MathOperation> Operations { get; set; }
        private List<Sign> Signs { get; }
        private List<double> Numbers { get; }


        public Calculator()
        {
            Operations = new List<MathOperation>()
            {
                new MathOperation('+', Sum, 1, true),
                new MathOperation('-', Subtract, 1, true, true),
                new MathOperation('*', Multiplier, 2, true),
                new MathOperation('/', Division, 2, true),
                new MathOperation('^', Degree, 2, true),
                new MathOperation('(', ParenthesesOpen, 3, false, true),
                new MathOperation(')', ParenthesesClose, 3, false),
                new MathOperation('=', null, 0, false),
                new MathOperation("Cos", Cos),
                new MathOperation("Sin", Sin),
                new MathOperation("Tan", Tan),
                new MathOperation("Factorial", Factorial),
                new MathOperation("Abs", Abs),
            };
            Signs = new List<Sign>();
            Numbers = new List<double>();
        }

        public StringBuilder Calculate(StringBuilder expression)
        {
            try
            {
                GetPriority(ChangeFormat(expression));

                Signs.Remove(Signs.Last());
                Signs.OrderByDescending(e => e.Index).ToList().ForEach(sign =>
                {
                    int index = Signs.IndexOf(sign);

                    double result = Operations
                        .First(i => i.Symbol == sign.Symbol).Feature
                        .Invoke((index == 0) ? 0 : Numbers[index - 1], Numbers[index]);

                    if (index != 0) Signs.Remove(sign);

                    Numbers.Insert((index == 0) ? 0 : index - 1, result);
                    Numbers.RemoveAt(index + 1);

                    if (index != 0) Numbers.RemoveAt(index);
                });

                expression.Clear();
                expression.Append($"{Numbers.FirstOrDefault()}");
                return new StringBuilder(expression.ToString());
            }
            catch (ArgumentOutOfRangeException)
            {
                return new StringBuilder("Превышен предел допустимых значений");
            }
        }

        private void GetPriority(string str)
        {
            Signs.Clear();
            Numbers.Clear();

            string number = String.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                if (Operations.Select(e => e.Symbol).Contains(str[i]))
                {
                    Sign sign = new Sign(str[i], Operations.First(e => e.Symbol == str[i]).Index);

                    if (sign.Index == 3)
                    {
                        Operations.First(e => e.Symbol == sign.Symbol).Parentheses.Invoke();
                    }
                    else
                    {
                        sign.Index += coefficient;
                        Signs.Add(sign);
                    }

                    if (double.TryParse(number, out double parse))
                    {
                        Numbers.Add(parse);
                        number = String.Empty;
                    }
                }
                else
                {
                    number += str[i];
                }
            }
        }

        private string ChangeFormat(StringBuilder expression)
        {
            expression.Append('=');
            expression.Replace("(-", "(0-");
            expression.Replace(")(", ")*(");
            if (expression[0] != '-') expression.Insert(0, "+");
            return expression.ToString();
        }
    }
}
