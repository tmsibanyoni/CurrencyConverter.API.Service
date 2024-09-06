namespace CurrencyConverter.Repository.Interface
{
    public interface IRedisRepository
    {
        public string GetValue(string key);
        public void SetValue(string key, string value);
        public bool KeyExists(string key);
        public void DeleteKey(string key);
    }
}
