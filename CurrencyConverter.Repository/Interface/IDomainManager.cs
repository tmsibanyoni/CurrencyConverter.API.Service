using CurrencyConverter.Repository.Model;

namespace CurrencyConverter.Repository.Interface
{
    public interface IDomainManager
    {
        public Task<CurrencyModel> Convert(string source, string target, double amount);
        public Task<List<CurrencyModel>> GetHistoryRates();
    }
}
