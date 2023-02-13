using CapAppMauiClient.Models;
using System.Text;
using System.Text.Json;
using Debug = System.Diagnostics.Debug;

namespace CapAppMauiClient.DataServices
{
    internal class RestDataService : IRestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestDataService()
        {
            _httpClient = new HttpClient();
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5137" : "https://localhost:7137";
            _url = $"{_baseAddress}/api";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }


        public async Task AddCapAppAsync(CapApp capApp)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No Internet Access...");
                return;
            }

            try
            {
                string jsonCapApp = JsonSerializer.Serialize<CapApp>(capApp);
                StringContent content = new StringContent(jsonCapApp, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/capapp", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully created CapApp!");
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }

            return;
        }

        public async Task DeleteCapAppAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No Internet Access...");
                return;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/capapp/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully deleted CapApp");
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }

            return;
        }

        public async Task<List<CapApp>> GetAllCapAppsAsync()
        {
            List<CapApp> capapps = new List<CapApp>();
            
            if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No Internet Access...");
                return capapps;
            }
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/capapp");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    
                    capapps = JsonSerializer.Deserialize<List<CapApp>>(content, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }

            return capapps;
        }

        public async Task UpdateCapAppAsync(CapApp capApp)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No Internet Access...");
                return;
            }

            try
            {
                string jsonCapApp = JsonSerializer.Serialize<CapApp>(capApp);
                StringContent content = new StringContent(jsonCapApp, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/capapp/{capApp.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully updated CapApp!");
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }

            return;
        }
    }
}
