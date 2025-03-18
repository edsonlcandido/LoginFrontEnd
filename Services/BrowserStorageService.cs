using System.Text.Json;
using Microsoft.JSInterop;

namespace LoginFrontEnd.Services
{
    public class LocalStorageService
    {
        private const string StorageType = "localStorage";
        private readonly IJSRuntime _jSRuntime;

        public LocalStorageService(IJSRuntime jSRuntime) {
            _jSRuntime = jSRuntime;
        }

        public async Task Save<TData>(string key, TData value)
        {
            var serializedData = Serialize(value);
            await _jSRuntime.InvokeVoidAsync($"{StorageType}.setItem", key, serializedData);
        }

        public async Task<TData?> Get<TData>(string key)
        {
            var serializedData = await _jSRuntime.InvokeAsync<string>($"{StorageType}.getItem", key);
            return Deserialize<TData>(serializedData);
        }

        public async Task Remove(string key)
        {
            await _jSRuntime.InvokeVoidAsync($"{StorageType}.removeItem", key);
        }

        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions();

        private static string Serialize<TData>(TData data) =>
            JsonSerializer.Serialize(data, _jsonSerializerOptions);

        private static TData? Deserialize<TData>(string? jsonData)
        {
            if (!string.IsNullOrWhiteSpace(jsonData))
            {
                return JsonSerializer.Deserialize<TData>(jsonData, _jsonSerializerOptions);
            }
            return default;
        }
    }
}
