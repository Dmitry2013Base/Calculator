using Calculator.CalcModel;
using Calculator.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace Calculator.Models
{
    internal class CalculatorModel : BindableBase
    {
        private const char point = ',';
        private const string defaultValue = "0";


        public StringBuilder Expression;
        public StringBuilder Result;
        public ObservableCollection<History> Histories;
        public bool AngleUnit;
        private readonly ICalculator _calculator;
        private bool checkPoint = false;

        public CalculatorModel()
        {
            Expression = new StringBuilder(defaultValue, 32);
            Result = new StringBuilder(defaultValue, 16);
            Histories = new ObservableCollection<History>();
            _calculator = new CalcModel.Calculator();
        }


        public void AddCharInExpression(char str)
        {
            if (CheckFunctionInExpression(Expression.ToString())) ClearExpression();

            if (Expression.Length == 1 && Expression.ToString() == defaultValue)
            {
                if (str != point)
                {
                    var nextOperation = _calculator.Operations.FirstOrDefault(e => e.Symbol == str);
                    if (nextOperation != null && !nextOperation.FirstPosition) return;
                    Expression.Remove(0, 1);
                }
                else
                {
                    checkPoint = true;
                }
            }
            else
            {
                var nextOperation = _calculator.Operations.FirstOrDefault(e => e.Symbol == str);

                if (nextOperation != null)
                {
                    var lastOperation = _calculator.Operations.FirstOrDefault(e => e.Symbol == Expression[Expression.Length - 1]);

                    if (lastOperation != null)
                    {
                        if (nextOperation.Index == 3)
                        {
                            if (!CheckParentheses(str)) return;
                        }
                        else
                        {
                            if (lastOperation.FirstPosition && !nextOperation.FirstPosition) return;
                            if (nextOperation.Interchangeable && lastOperation.Interchangeable) Expression.Remove(Expression.Length - 1, 1);
                        }
                    }
                    else
                    {
                        if (!CheckParentheses(str)) return;
                        if (str == '(' || (_calculator.Operations.Select(e => e.Symbol).Contains(str) && Expression[Expression.Length - 1] == point)) return;
                    }
                    checkPoint = false;
                }
                else
                {
                    if (checkPoint && str == point) return;
                    if (str == point) checkPoint = true;
                }
            }

            Expression.Append(str);
            RaisePropertyChanged("Expression");
        }

        public void RemoveCharInExpression()
        {
            if (Expression.Length == 1)
            {
                Expression.Replace(Expression[0].ToString(), "0");
            }
            else if (Expression.Length > 1)
            {
                if (Expression[Expression.Length - 1] == ',')
                {
                    checkPoint = false;
                }
                Expression.Remove(Expression.Length - 1, 1);
            }
            else return;
            RaisePropertyChanged("Expression");
        }

        public void ClearExpression(History history = null)
        {
            Expression.Clear();
            Result.Clear();
            Expression.Append((history == null) ? defaultValue : history.Expression);
            Result.Append((history == null) ? defaultValue : history.Result);
            RaisePropertyChanged("Result");
            RaisePropertyChanged("Expression");
        }

        public void Calculate()
        {
            if (Expression.Length != 0 && !CheckFunctionInExpression(Expression.ToString()))
            {
                Expression.Replace(".", point.ToString());
                string expression = Expression.ToString();
                Result = _calculator.Calculate(Expression);
                Histories.Insert(0, new History(expression, Result.ToString()));
                if (Histories.Count > 50) Histories.RemoveAt(Histories.Count - 1);
                RaisePropertyChanged("Histories");
                RaisePropertyChanged("Result");
            }
        }

        public void MathFunction(string str)
        {
            Result.Clear();
            string expression = Expression.ToString();

            if (!CheckFunctionInExpression(expression))
            {
                double number = double.Parse(double.TryParse(Expression.ToString(), out double num) ? num.ToString() : _calculator.Calculate(Expression).ToString());
                Result.Append(_calculator.Operations.First(e => e.Function == str).AfterAction.Invoke(number, AngleUnit));
                Expression.Clear();
                Expression.Append($"{str}({expression})=");
                RaisePropertyChanged("Expression");
                RaisePropertyChanged("Result");
            }
        }

        public void GetMathConst(string str)
        {
            if (Expression.Length == 1 && Expression.ToString() == defaultValue)
            {
                Expression.Remove(0, 1);
            }
            else
            {
                if (Expression[Expression.Length - 1] == ')' || _calculator.Operations.FirstOrDefault(e => e.Symbol == Expression[Expression.Length - 1]) == null) return;
            }

            if (str == "Pi") Expression.Append(Math.PI);
            else if (str == "E") Expression.Append(Math.E);

            checkPoint = true;
            RaisePropertyChanged("Expression");
        }

        public void AngleUnitChange()
        {
            AngleUnit = !AngleUnit;
            RaisePropertyChanged("AngleUnit");
        }

        private bool CheckFunctionInExpression(string expression)
        {
            bool checkFunction = false;

            _calculator.Operations.ForEach(e =>
            {
                if (e.Function != null && expression.Contains(e.Function))
                {
                    checkFunction = true;
                }
            });
            return checkFunction;
        }

        private bool CheckParentheses(char str)
        {
            int count1 = Expression.ToString().Count(e => e == '(');
            int count2 = Expression.ToString().Count(e => e == ')');

            return str != ')' || count1 >= count2 + 1 && count1 != 0;
        }
    }
}
