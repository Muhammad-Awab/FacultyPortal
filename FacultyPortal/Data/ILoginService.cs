using ClassLibraryEnt;

namespace FacultyPortal.Data
{
    public interface ILoginService
    {
        Task<EntRegistration> GetLogin(string Email, string Password);
        
    }
}
