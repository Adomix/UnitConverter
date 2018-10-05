using System.Net.Http;
using System.Threading.Tasks;
using System.Configuration;

namespace Converter
{
    public class WARequest
    {
        private readonly HttpClient _http;

        public WARequest()
        {
            _http = new HttpClient();
        }

        public async Task<string> WaAsync(decimal valueA, string unitA, string unitB)
        {
            using (var req = await _http.GetAsync($"http://api.wolframalpha.com/v1/result?appid={ConfigurationManager.AppSettings["wolfram"]}&i={valueA.ToString()}{unitA} to {unitB}"))
            {
                var content = await req.Content.ReadAsStringAsync();

                if (content.Contains("understand"))
                {
                    content = "Request could not be completed!";
                }

                return content;
            }
        }

    }
}
