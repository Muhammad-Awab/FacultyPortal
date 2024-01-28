using ClassLibraryEnt;
using System.Net;

namespace FacultyPortal.Data
{
    public class RegistrationService : IRegistrationService
    {
        private readonly HttpClient _httpClient;
        public RegistrationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

		public async Task<bool> RegisterUser(EntRegistration registerUser)
		{
			HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/Login/saveuserregistration", registerUser);

			if (response.IsSuccessStatusCode)
			{
				// Registration successful
				return true;
			}
			else if (response.StatusCode == HttpStatusCode.Conflict)
			{
				// Email already exists
				return false;
			}
			else
			{
				// Handle other cases if needed
				return false;
			}
		}


		public async Task UpdateRegisterUser(EntRegistration registerUser)
		{
			await _httpClient.PostAsJsonAsync("/Login/updateuserregistration", registerUser);
		}

	}
}
