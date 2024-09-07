using CurrencyConverter.Repository.Interface;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyConverter.API.Service.Test.Contoller
{
    [TestClass]
    public  class CurrencyControllerTests
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly IDomainManager _domainManager;

        public CurrencyControllerTests()
        {
            _factory = new WebApplicationFactory<Program>();
            _httpClient = _factory.CreateDefaultClient();

            _domainManager = _factory.Services.CreateAsyncScope().ServiceProvider.GetRequiredService<IDomainManager>();
        }

        [TestMethod()]
        public void ConvertTest_Return_ConvertTargetValue()
        {
            var currency = _domainManager.Convert("USD", "INR", 100).Result;
            Assert.IsNotNull(currency);
        }

        [TestMethod()]
        public void HistoryTest_Return_HistoryRates()
        {
            var currency = _domainManager.GetHistoryRates().Result;
            Assert.IsNotNull(currency);
        }
    }
}
