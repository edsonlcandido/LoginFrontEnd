using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace LoginFrontEnd.Providers
{
    public class CustomHttpClientProvider : HttpClient 
    {
        public CustomHttpClientProvider()
        {
        }
        public async Task<LoginResponse> LoginAsync(string user, string password)
        {
            var loginRequest = new LoginRequest
            {
                User = user,
                Senha = password
            };

            var response = await this.PostAsJsonAsync("https://bypass.ehtudo.app/https://eh-tudo-n8n.aiyfgd.easypanel.host/webhook/iptv-backend/v1/login", loginRequest);
            LoginResponse loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return loginResponse;
        }

        public class LoginRequest
        {
            public string User { get; set; }
            public string Senha { get; set; }
        }
        public class LoginResponse {
            public int Id {  get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            //o campo do json para o token é xc-token
            //como eu especifico qual o campo ele deve ler
            [JsonPropertyName("xc-token")]
            public string Token { get; set; }
        }

    }
}
