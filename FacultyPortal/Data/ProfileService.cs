using Azure;
using ClassLibraryEnt;
using System.Net;

namespace FacultyPortal.Data
{
    public class ProfileService : IProfile
    {

        private readonly HttpClient _httpClient;
        public ProfileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
      

        public async Task<bool> SaveUserProfile(EntRegistration UserProfile)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/Login/saveuserprofile", UserProfile);

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

        public async Task<EntRegistration> LoadUserProfile(EntRegistration UserProfile)
        {
            return await _httpClient.GetFromJsonAsync<EntRegistration>($"/Login/getProfile/{UserProfile.UserID}");
        }

    }
}
