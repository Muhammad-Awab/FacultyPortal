using ClassLibraryEnt;

namespace FacultyPortal.Data
{
	public interface IEmailExistChecker
	{
		Task<List<EntRegistration>> GetEmailExits(string Email);
	}
}
