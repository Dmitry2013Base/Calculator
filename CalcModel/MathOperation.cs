using System;

namespace Calculator.CalcModel
{
    public class MathOperation
    {
        public int Index { get; set; }
        public char Symbol { get; set; }
        public string Function { get; set; }
        public Func<double, double, double> Feature { get; set; }
        public Func<double, bool, double> AfterAction { get; set; }
        public Action Parentheses { get; set; }
        public bool Interchangeable { get; set; }
        public bool FirstPosition { get; set; }


        public MathOperation(string function, Func<double, bool, double> afterAction)
        {
            Function = function;
            AfterAction = afterAction;
        }

        public MathOperation(char symbol, Action parentheses, int index, bool interchangeable, bool firstPosition = false)
        {
            Symbol = symbol;
            Parentheses = parentheses;
            Index = index;
            Interchangeable = interchangeable;
            FirstPosition = firstPosition;
        }

        public MathOperation(char symbol, Func<double, double, double> feature, int index, bool interchangeable, bool firstPosition = false)
        {
            Symbol = symbol;
            Feature = feature;
            Index = index;
            Interchangeable = interchangeable;
            FirstPosition = firstPosition;
        }
    }
}
