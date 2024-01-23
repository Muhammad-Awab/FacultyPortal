using ClassLibraryEnt;

namespace FacultyPortal.Data
{
    public interface ILoginService
    {
        Task<List<EntRegistration>> GetLogin(string Email, string Password);
        
    }
}
