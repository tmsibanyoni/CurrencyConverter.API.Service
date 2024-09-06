using CurrencyConverter.Repository.Model;

namespace CurrencyConverter.Repository.Interface
{
    public interface IMySqlRepository
    {
        Task Create(CurrencyModel currency);
        Task<CurrencyModel> Get(int id);
        Task Update(CurrencyModel currency);
        Task Delete(int id);
        Task<List<CurrencyModel>> GetAll();
    }
}
