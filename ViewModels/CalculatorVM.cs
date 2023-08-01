using Calculator.CalcModel;
using Calculator.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Text;

namespace Calculator.ViewModels
{
    internal class CalculatorVM : BindableBase
    {
        private readonly CalculatorModel _model;


        public DelegateCommand<string> UpdateExpression { get; }
        public DelegateCommand<string> RemoveExpression { get; }
        public DelegateCommand<string> ClearExpression { get; }
        public DelegateCommand Calculate { get; }
        public DelegateCommand<string> GetMathConst { get; }
        public DelegateCommand AngleUnitChange { get; }
        public DelegateCommand<string> MathFunction { get; }
        public DelegateCommand<History> GetHistory { get; }
        public StringBuilder Expression => _model.Expression;
        public StringBuilder Result => _model.Result;
        public ObservableCollection<History> Histories => _model.Histories;


        public CalculatorVM()
        {
            _model = new CalculatorModel();
            _model.PropertyChanged += (s, e) => RaisePropertyChanged(e.PropertyName);

            UpdateExpression = new DelegateCommand<string>(str => _model.AddCharInExpression(char.Parse(str)));
            RemoveExpression = new DelegateCommand<string>(str => _model.RemoveCharInExpression());
            ClearExpression = new DelegateCommand<string>(str => _model.ClearExpression());
            MathFunction = new DelegateCommand<string>(str => _model.MathFunction(str));
            GetMathConst = new DelegateCommand<string>(str => _model.GetMathConst(str));
            AngleUnitChange = new DelegateCommand(() => _model.AngleUnitChange());
            GetHistory = new DelegateCommand<History>(history => _model.ClearExpression(history));
            Calculate = new DelegateCommand(() => _model.Calculate());
        }
    }
}
