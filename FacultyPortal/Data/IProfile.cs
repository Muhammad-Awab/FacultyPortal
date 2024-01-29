using ClassLibraryEnt;

namespace FacultyPortal.Data
{
    public interface IProfile
    {
        Task<bool> SaveUserProfile(EntRegistration UserProfile);
        Task<EntRegistration> LoadUserProfile(EntRegistration UserProfile);

    }
}
