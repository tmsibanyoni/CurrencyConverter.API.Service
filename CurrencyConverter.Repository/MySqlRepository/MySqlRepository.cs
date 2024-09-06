using CurrencyConverter.Repository.Interface;
using CurrencyConverter.Repository.Model;

namespace CurrencyConverter.Repository.MySqlRepository
{
    public class MySqlRepository(ApplicationDbContext dbContext) : IMySqlRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task Create(CurrencyModel currency)
        {
            try
            {
                if (currency != null)
                {
                    _dbContext.Currency.Add(currency);
                    _dbContext.SaveChanges();
                    return;
                }
            }
            catch (Exception ex)
            {
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