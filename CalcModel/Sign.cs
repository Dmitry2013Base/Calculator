namespace Calculator.CalcModel
{
    public class Sign
    {
        public int Index { get; set; }
        public char Symbol { get; set; }

        public Sign(char symbol, int index)
        {
            Symbol = symbol;
            Index = index;
        }
    }
}
