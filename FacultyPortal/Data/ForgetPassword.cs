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

		public async Task PasswordForget(EntRegistration registerUser)
		{
			await _httpClient.PostAsJsonAsync("/Login/forgetpassword", registerUser);
		}
	}
}

