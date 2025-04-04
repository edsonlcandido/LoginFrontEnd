using System.Net.Http;
using System.Net.Http.Json;
using LoginFrontEnd.Models;
using LoginFrontEnd.Providers;

namespace LoginFrontEnd.Services
{

    public class ClienteService
    {
        private readonly CustomHttpClientProvider _customHttpClientProvider;
        private List<FastClient> _fastClientes;
        private List<Teste> _testes;
        public ClienteService(CustomHttpClientProvider customHttpClientProvider)
        {
            _customHttpClientProvider = customHttpClientProvider;
        }

        public List<FastClient> FastClients => _fastClientes;
        public List<Teste> Testes => _testes;

        public async Task LoadData(string token)
        {
            if (_fastClientes == null || _testes == null)
            {
                _customHttpClientProvider.DefaultRequestHeaders.Clear();
                _customHttpClientProvider.DefaultRequestHeaders.Add("xc-token", token);

                var fastClientesResponse = await _customHttpClientProvider
                    .GetFromJsonAsync<List<FastClientResponse>>("https://bypass.ehtudo.app/https://eh-tudo-n8n.aiyfgd.easypanel.host/webhook/iptv-backend/v1/listar-clientes");
                _fastClientes = fastClientesResponse.FirstOrDefault().data;

                var testesResponse = await _customHttpClientProvider.GetFromJsonAsync<TestesResponse>("https://bypass.ehtudo.app/https://eh-tudo-nocodb.aiyfgd.easypanel.host/api/v2/tables/mkdo3202f4rv75a/records?limit=1000");
                _testes = testesResponse.list;
            }
        }
    }
}
