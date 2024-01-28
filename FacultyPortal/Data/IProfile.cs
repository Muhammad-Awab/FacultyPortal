using ClassLibraryEnt;

namespace FacultyPortal.Data
{
    public interface IProfile
    {
        Task<bool> SaveUserProfile(EntProfile UserProfile);

    }
}
