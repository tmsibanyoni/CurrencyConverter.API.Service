namespace CurrencyConverter.Repository.Model
{
    public class CurrencyModel
    {
        public int Id { get; set; }
        public string Base { get; set; }
        public string Target { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public double TargetCurrency { get; set; }
    }
}