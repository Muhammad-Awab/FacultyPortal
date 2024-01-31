using ClassLibraryEnt;
using System.Text.Json;

namespace FacultyPortal.Data
{
	public class EmailExistChecker: IEmailExistChecker
	{
		private readonly HttpClient _httpClient;
		public EmailExistChecker(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<List<EntRegistration>> GetEmailExits(string Email)
		{
			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync($"/Login/getemailexits/{Email}");

				if (response.IsSuccessStatusCode)
				{
					// Ensure the response content type is JSON
					if (response.Content.Headers.ContentType.MediaType == "application/json")
					{
						return await response.Content.ReadFromJsonAsync<List<EntRegistration>>();
					}
					else
					{
						// Log or handle unexpected content type
						throw new JsonException("Unexpected content type");
					}
				}
				else
				{
					// Log or handle non-success status code
					throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
				}
			}
			catch (Exception ex)
			{
				// Log or handle any other exceptions
				throw new Exception("Error in GetEmailExits", ex);
			}
		}


	}
}
