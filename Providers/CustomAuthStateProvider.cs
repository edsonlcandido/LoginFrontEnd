using System.Security.Claims;
using LoginFrontEnd.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;


namespace LoginFrontEnd.Providers
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private const string AuthenticationType = "CustomAuth";
        private const string StorageKey = "UserInfo";
        private readonly LocalStorageService _localStorageService;
        private readonly NavigationManager _navigationManager;
        public User CurrentUser { get; set; } = new();

        public CustomAuthStateProvider(LocalStorageService localStorageService, NavigationManager navigationManager)
        {
            _localStorageService = localStorageService;
            _navigationManager = navigationManager;
            //AuthenticationStateChanged += CustomAuthStateProvider_AuthenticationStateChanged;
        }

        private async void CustomAuthStateProvider_AuthenticationStateChanged(Task<AuthenticationState> task)
        {
            var authState = await task;
            if (authState is not null)
            {
                var idStr = authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (
                !string.IsNullOrWhiteSpace(idStr) // idStr is not null or empty
                && int.TryParse(idStr, out int id) // idStr is a valid integer
                && id > 0 // id is a gereater than zero
                )
                {
                    CurrentUser = new User
                    {
                        Id = id,
                        Name = authState.User.FindFirst(ClaimTypes.Name)!.Value,
                        Token = authState.User.FindFirst("Token")!.Value
                    };
                    return;
                }
            }
            CurrentUser = new();
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            Console.WriteLine("GetAuthenticationStateAsync chamado");
            //verify if UserInfo exists in local storage
            var user = await _localStorageService.Get<User>(StorageKey);
            if (user != null) {
                Claim[] claims = new Claim[] {new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("Token", user.Token) };

                var identity = new ClaimsIdentity(claims, AuthenticationType);
                var claimsPrincial = new ClaimsPrincipal(identity);
                var authState = new AuthenticationState(claimsPrincial);
                return authState;
            }
            else
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }            
        }
        public async Task LoginAsync(string username, string password)
        {
            Console.WriteLine("LoginAsync chamado");
            var user = new User
            {
                Id = 1,
                Name = "Abhay Prince",
                Token = "some-random-token-value"
            };
            await _localStorageService.Save(StorageKey, user);

            Claim[] claims = new Claim[] {new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim("Token", user.Token) };

            var identity = new ClaimsIdentity(claims, AuthenticationType);
            var claimsPrincial = new ClaimsPrincipal(identity);
            var authState = new AuthenticationState(claimsPrincial);
            NotifyAuthenticationStateChanged();
            // Truque de navegação: navegar para uma rota temporária e depois para /app
            //_navigationManager.NavigateTo("/temp", forceLoad: false, replace: true); // Navega para uma rota temporária
            //_navigationManager.NavigateTo("/", forceLoad: true); // Navega de volta para /app
        }
        public void NotifyAuthenticationStateChanged()
        {
            Console.WriteLine("NotifyAuthenticationStateChanged chamado");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());            
        }
        public async Task LogoutAsync()
        {
            Console.WriteLine("LogoutAsync chamado");
            await _localStorageService.Remove(StorageKey);
            NotifyAuthenticationStateChanged();
            // Truque de navegação: navegar para uma rota temporária e depois para /app
            //_navigationManager.NavigateTo("/temp", forceLoad: false, replace: true); // Navega para uma rota temporária
            //_navigationManager.NavigateTo("/", forceLoad: true); // Navega de volta para /app
        }

    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}
