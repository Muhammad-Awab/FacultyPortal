using ClassLibraryEnt;

namespace FacultyPortal.Data
{
	public interface IForgetPassword
	{
		Task<EntRegistration> GetEmailExits(string Email);
	}
}
