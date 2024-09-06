using CurrencyConverter.Repository.Interface;
using CurrencyConverter.Repository.Model;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CurrencyConverter.Repository.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CurrencyConverter.Repository.Service
{
    public class OpenExchangeRatesService(ILogger<OpenExchangeRatesService> logger,
                                          HttpClient client,
                                          IOptions<OpenExchangeRatesOptions> openExchangeRatesOptions,
                                          JsonSerializerOptions defaultSerializerOptions = null) : IOpenExchangeRatesService
    {

        private readonly JsonSerializerOptions _defaultSerializerOptions = defaultSerializerOptions ?? new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        private readonly HttpClient _client = new HttpClient();
        private readonly ILogger<OpenExchangeRatesService> _logger = logger;
        private readonly IOptions<OpenExchangeRatesOptions> _openExchangeRatesOptions = openExchangeRatesOptions;
        private readonly string app_id = $"app_id={openExchangeRatesOptions.Value.App_Id}";

        public async Task<double> ConvertCurrency(string @base, string target, double amount)
        {
            //The below Endpoint is not available for free users
            var requestUrl = $"{openExchangeRatesOptions.Value.BaseUrl}convert/{amount}/{@base}/{target}.json?{app_id}";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            try
            {
                var response = await _client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("{Service} returned {StatusCode} with {Content}.", $"{requestUrl}", response.StatusCode, await response.Content.ReadAsStringAsync());
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ExchangeRatesResponseModel> LatestRates()
        {
            var requestUrl = $"{openExchangeRatesOptions.Value.BaseUrl}latest.json?{app_id}";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            try
            {
                var response = await _client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("{Service} returned {StatusCode} with {Content}.", $"{requestUrl}", response.StatusCode, await response.Content.ReadAsStringAsync());
                }

                return await JsonSerializer.DeserializeAsync<ExchangeRatesResponseModel>(
                       await response.Content.ReadAsStreamAsync(),
                       _defaultSerializerOptions); ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ExchangeRatesResponseModel> HistoricRates(DateTime date)
        {
            var requestUrl = $"{openExchangeRatesOptions.Value.BaseUrl}historical/{date}.json?{app_id}";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            try
            {
                var response = await _client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("{Service} returned {StatusCode} with {Content}.", $"{requestUrl}", response.StatusCode, await response.Content.ReadAsStringAsync());
                }

                return await JsonSerializer.DeserializeAsync<ExchangeRatesResponseModel>(
                       await response.Content.ReadAsStreamAsync(),
                       _defaultSerializerOptions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
