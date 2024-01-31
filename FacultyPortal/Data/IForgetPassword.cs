using ClassLibraryEnt;

namespace FacultyPortal.Data
{
	public interface IForgetPassword
	{
		Task PasswordForget(EntRegistration registerUser);
	}
}
