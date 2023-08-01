using Calculator.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows;

namespace Calculator.ViewModels
{
    internal class InterfaceVM : BindableBase
    {
        private readonly InterfaceModel _model;
       

        public DelegateCommand UpdateTopmost { get; }
        public DelegateCommand UpdateHistoryVisible { get; }
        public DelegateCommand UpdateAngleUnit { get; }


        public bool Topmost => _model.Topmost;
        public Visibility HistoryVisible => _model.HistoryVisible;
        public string Angle => _model.Angle;


        public InterfaceVM()
        {
            _model = new InterfaceModel();
            _model.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };

            UpdateTopmost = new DelegateCommand(() => _model.TopmostChange());
            UpdateHistoryVisible = new DelegateCommand(() => _model.HistoryChangeVisible());
            UpdateAngleUnit = new DelegateCommand(() => _model.AngleUnitChange());
        }
    }
}
