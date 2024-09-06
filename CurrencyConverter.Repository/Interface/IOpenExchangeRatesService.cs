using CurrencyConverter.Repository.Model;

namespace CurrencyConverter.Repository.Interface
{
    public  interface IOpenExchangeRatesService
    {
        Task<ExchangeRatesResponseModel> LatestRates();
        Task<double> ConvertCurrency(string @base, string target, double amount);
        Task<ExchangeRatesResponseModel> HistoricRates(DateTime date);
    }
}
