using ClassLibraryEnt;

namespace FacultyPortal.Data
{
    public interface IRegistrationService
    {
        Task RegisterUser(EntRegistration registerUser);

		Task UpdateRegisterUser(EntRegistration registerUser);
	}
}
