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
        public async Task<List<EntUser>> GetLogin(string Email, string Password)
        {
            return await _httpClient.GetFromJsonAsync<List<EntUser>>("api/user/getlogin/{Email}/{Password}");
        }
    }


}
