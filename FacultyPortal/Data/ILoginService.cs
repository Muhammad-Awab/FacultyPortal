using ClassLibraryEnt;

namespace FacultyPortal.Data
{
    public interface ILoginService
    {
        Task<List<EntUser>> GetLogin(string Email, string Password);
        
    }
}
