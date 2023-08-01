using Prism.Mvvm;
using System.Windows;

namespace Calculator.Models
{
    internal class InterfaceModel : BindableBase
    {
        public bool Topmost;
        public Visibility HistoryVisible = Visibility.Collapsed;
        public string Angle = "RAD";

        public void TopmostChange()
        {
            Topmost = !Topmost;
            RaisePropertyChanged("Topmost");
        }

        public void HistoryChangeVisible()
        {
            HistoryVisible = (HistoryVisible == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
            RaisePropertyChanged("HistoryVisible");
        }

        public void AngleUnitChange()
        {
            Angle = (Angle == "RAD") ? "DEG" : "RAD";
            RaisePropertyChanged("Angle");
        }
    }
}
