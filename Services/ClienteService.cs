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
        private List<Models.Cliente> _clientes;
        public ClienteService(CustomHttpClientProvider customHttpClientProvider)
        {
            _customHttpClientProvider = customHttpClientProvider;
        }

        public List<FastClient> FastClients => _fastClientes;
        public List<Teste> Testes => _testes;
        public List<Cliente> Clientes => _clientes;

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
                _clientes = new List<Models.Cliente>();

                foreach (var item in _fastClientes)
                {
                    //obter o teste pelo campo Login igual ao username do fastclient
                    var teste = _testes.FirstOrDefault(x => x.Login == item.username);
                    _clientes.Add(new Cliente()
                    {
                        Fast_id = item.id,
                        Username = item.username,
                        Password = item.password,
                        DataExpericaoUnix = item.exp_date,
                        Notas = item.reseller_notes,
                        Celular = teste?.Celular,
                        Email = teste?.Email,
                        Teste = Convert.ToBoolean(item.is_trial)
                    });
                }
            }
        }
        public async Task<List<Cliente>> ClientesExpirandoHoje(string token)
        {
            if (_clientes == null)
            {
                await LoadData(token);
            }
            return _clientes.Where(x => x.DataExpiracao.Date == DateTime.Now.Date).ToList();
        }
    }
}
