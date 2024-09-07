using CurrencyConverter.Repository.Interface;
using CurrencyConverter.Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Repository.MySqlRepository
{
    public class MySqlRepository(ApplicationDbContext dbContext) : IMySqlRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<bool> Create(CurrencyModel currency)
        {
            try
            {
                if (currency != null)
                {
                    _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                    var getLastId = _dbContext.Currency.FirstOrDefault();

                    if (currency.Id == 0)
                    {
                        if (_dbContext.Currency.FirstOrDefault() == null)
                            currency.Id = 1;
                        else
                        {
                            var ID = ++(_dbContext.Currency.ToList().Last().Id);
                            currency.Id = ID;
                        }
                    }

                    _dbContext.Currency.Add(currency);
                    _dbContext.SaveChanges();
                    return await Task.FromResult(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception($"Error: {ex.Message}. StackTrace: {ex.StackTrace}");
            }
        }
        public async Task Delete(int id)
        {
            try
            {
                _dbContext.Currency.Remove(_dbContext.Currency.FirstOrDefault(x => x.Id == id));
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}. StackTrace: {ex.StackTrace}");
            }
        }
        public async Task<CurrencyModel> Get(int id)
        {
            try
            {
                return _dbContext.Currency.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}. StackTrace: {ex.StackTrace}");
            }
        }
        public async Task<List<CurrencyModel>> GetAll()
        {
            try
            {
                return _dbContext.Currency.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}. StackTrace: {ex.StackTrace}");
            }
        }
        public async Task Update(CurrencyModel currency)
        {
            try
            {
                if (currency == null)
                {
                   var getCurrency= _dbContext.Currency.FirstOrDefault(x => x.Id == currency.Id);
                    if (getCurrency != null)
                    {
                        getCurrency.Target = currency.Target;
                        getCurrency.Rate = currency.Rate;
                        getCurrency.Amount = currency.Amount;
                        getCurrency.Base = currency.Base;

                        _dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}. StackTrace: {ex.StackTrace}");
            }
        }
    }
}