using Calculator.CalcModel;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Interfaces
{
    public interface ICalculator
    {
        List<MathOperation> Operations { get; set; }

        StringBuilder Calculate(StringBuilder expression);
    }
}
