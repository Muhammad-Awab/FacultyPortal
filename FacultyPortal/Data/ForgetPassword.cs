using ClassLibraryEnt;

namespace FacultyPortal.Data
{
	public class ForgetPassword: IForgetPassword
	{
		private readonly HttpClient _httpClient;
		public ForgetPassword(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<EntRegistration> GetEmailExits(string Email)
		{
			return await _httpClient.GetFromJsonAsync<EntRegistration>($"/ForgetPassword/getforgetpassword/{Email}");
		}
	}
}
