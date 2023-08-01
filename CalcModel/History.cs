using System;

namespace Calculator.CalcModel
{
    public class History
    {
        public Guid Id { get; set; }
        public string Expression { get; set; }
        public string Result { get; set; }

        public History(string expression, string result)
        {
            Id = Guid.NewGuid();
            Expression = expression;
            Result = result;
        }
    }
}
