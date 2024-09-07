using CurrencyConverter.Repository.Interface;
using CurrencyConverter.Repository.Model;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CurrencyConverter.Repository.Domain
{
    public class DomainManager(ILogger<DomainManager> logger,
                              IRedisRepository redisRepository,
                              IOpenExchangeRatesService openExchangeRates,
                              IMySqlRepository mySqlRepository) : IDomainManager
    {
        public readonly ILogger<DomainManager> _logger = logger;
        public readonly IRedisRepository _redisRepository = redisRepository;
        public readonly IOpenExchangeRatesService _openExchangeRates = openExchangeRates;
        public readonly IMySqlRepository _mySqlRepository = mySqlRepository;

        public async Task<CurrencyModel> Convert(string source, string target, double amount)
        {
            var key = $"Latest-{DateTime.Now.ToString("yyyyMMdd")}";
            ExchangeRatesResponseModel exchangeRates = null;
            CurrencyModel targetCurrency = null;

            var result = _redisRepository.GetValue(key);

            if (result == null)
            {
                exchangeRates = _openExchangeRates.LatestRates().Result;
                _redisRepository.SetValue(key, JsonSerializer.Serialize(exchangeRates));
            }
            else
            {
                try
                {
                    exchangeRates = JsonSerializer.Deserialize<ExchangeRatesResponseModel>(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            double value;
            if (exchangeRates.Rates.TryGetValue(target.ToUpper(), out value))
            {
                var finalValue = amount * value;

                targetCurrency = new CurrencyModel()
                {
                    Base = source.ToUpper(),
                    Rate = value,
                    Target = target.ToUpper(),
                    Amount = amount,
                    TargetCurrency = finalValue
                };

                await mySqlRepository.Create(targetCurrency);

                return targetCurrency;
            }
            else
            {
                logger.LogError("Something went wrong when trying to convert Base to Target.");
            }

            return targetCurrency;
        }
        public async Task<List<CurrencyModel>> GetHistoryRates()
        {
            var history = await mySqlRepository.GetAll();

            if(history.Count == 0)
                logger.LogInformation("No history of currency rates");

            return  history;
        }
    }
}