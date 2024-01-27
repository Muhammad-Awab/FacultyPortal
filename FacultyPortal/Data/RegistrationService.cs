using ClassLibraryEnt;

namespace FacultyPortal.Data
{
    public class RegistrationService : IRegistrationService
    {
        private readonly HttpClient _httpClient;
        public RegistrationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task RegisterUser(EntRegistration registerUser)
        {
            await _httpClient.PostAsJsonAsync("/Login/saveuserregistration", registerUser);
        }

		public async Task UpdateRegisterUser(EntRegistration registerUser)
		{
			await _httpClient.PostAsJsonAsync("/Login/updateuserregistration", registerUser);
		}

	}
}
