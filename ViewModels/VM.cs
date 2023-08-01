namespace Calculator.ViewModels
{
    internal class VM
    {
        public CalculatorVM Calculator { get; }
        public InterfaceVM InterfaceVM { get; }

        public VM()
        {
            Calculator = new CalculatorVM();
            InterfaceVM = new InterfaceVM();
        }
    }
}
