using ClassLibraryEnt;

namespace FacultyPortal.Data
{
    public class LoginService : ILoginService
    {

        private readonly HttpClient _httpClient;
        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<EntRegistration> GetLogin(string Email, string Password)
        {
            return await _httpClient.GetFromJsonAsync<EntRegistration>($"/Login/getlogin/{Email}/{Password}");
        }
    }


}
