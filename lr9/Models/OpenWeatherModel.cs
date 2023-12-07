namespace lr9.Models
{
    public class OpenWeatherModel
    {
        private const string API = "3f5ea523012bc42d70510cb7b85b3cac";
        private readonly HttpClient _client;
        public OpenWeatherModel()
        {
            _client = new HttpClient();
        }
        public async Task<string> GetWeather(string city)
        {
            try
            {
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={API}&units=metric";
                string response = await _client.GetStringAsync(url);
                return response;
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return "City not found";
                }
                else return ex.Message;
            }
            
        }
    }
}
