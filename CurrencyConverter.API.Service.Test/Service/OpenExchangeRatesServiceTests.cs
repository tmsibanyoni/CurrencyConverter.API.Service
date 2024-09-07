using CurrencyConverter.Repository.Interface;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyConverter.API.Service.Test.Service
{
    [TestClass]
    public class OpenExchangeRatesServiceTests
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly IOpenExchangeRatesService _openExchangeRates;

        public OpenExchangeRatesServiceTests()
        {
            _factory = new WebApplicationFactory<Program>();
            _httpClient = _factory.CreateDefaultClient();

            _openExchangeRates = _factory.Services.CreateAsyncScope().ServiceProvider.GetRequiredService<IOpenExchangeRatesService>();
        }

        [TestMethod()]
        public void ConvertTest_Return_LatestRates()
        {
            var latestRates = _openExchangeRates.LatestRates().Result;
            Assert.IsNotNull(latestRates);
        }

        [TestMethod()]
        public void ConvertTest_Return_RatesHistory()
        {
            var latestRates = _openExchangeRates.HistoricRates(DateTime.Now).Result;
            Assert.IsNotNull(latestRates);
        }
    }
}
