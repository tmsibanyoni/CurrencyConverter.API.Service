using Newtonsoft.Json;

namespace CurrencyConverter.Repository.Model
{
    public class ExchangeRatesResponseModel
    {
        public Uri License { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("rates")]
        public Dictionary<string, double> Rates { get; set; }
    }

}
