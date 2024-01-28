using ClassLibraryEnt;

namespace FacultyPortal.Data
{
    public interface IRegistrationService
    {
		Task<bool> RegisterUser(EntRegistration registerUser);

		Task UpdateRegisterUser(EntRegistration registerUser);
	}
}
