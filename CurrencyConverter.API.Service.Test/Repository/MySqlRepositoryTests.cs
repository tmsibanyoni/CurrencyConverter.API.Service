using CurrencyConverter.Repository.Interface;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
namespace CurrencyConverter.API.Service.Test.Repository
{
    [TestClass]
    public class MySqlRepositoryTests
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly IMySqlRepository _mySqlRepository;

        public MySqlRepositoryTests()
        {
            _factory = new WebApplicationFactory<Program>();

            _mySqlRepository = _factory.Services.CreateAsyncScope().ServiceProvider.GetRequiredService<IMySqlRepository>();
        }

        [TestMethod()]
        public void Create_Success_Insert_MySqlDb()
        { 
            var insertRecord = _mySqlRepository.Create(new CurrencyConverter.Repository.Model.CurrencyModel()
            {
                Amount = 3,
                Base = "USD",
                Target = "CNY"
            }).Result;

            Assert.IsTrue(insertRecord);
        }

        [TestMethod()]
        public void Create_fail_Insert_MySqlDb()
        {
            var insertRecord =  _mySqlRepository.Create(new CurrencyConverter.Repository.Model.CurrencyModel()
            {   
                Id = 1,
                Amount = 3,
                Base = "USD",
                Target = "TTT"
            }).Result;

            Assert.IsFalse(insertRecord);
        }

        [TestMethod()]
        public void GetHistory_Success_Insert_MySqlDb()
        {
            var insertRecord = _mySqlRepository.GetAll().Result;

            Assert.IsNotNull(insertRecord);
        }
    }
}
