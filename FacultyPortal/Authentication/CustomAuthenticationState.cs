
using ClassLibraryEnt;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace FacultyPortal.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage? _sessionStorage;
        private ClaimsPrincipal? _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {

                var userSessionStorageResult = await _sessionStorage.GetAsync<EntRegistration>("UserSession");
                var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));


                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    { new Claim(ClaimTypes.Name, userSession.UserID.ToString()),
                      new Claim (ClaimTypes.Role ,userSession.Role),

                    }, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));

            }

            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }
        //public  async Task<AuthenticationState> GetVenueAuthenticationStateAsync()
        //{
        //	try
        //	{

        //		var userSessionStorageResult = await _sessionStorage.GetAsync<UserSession>("VenueSession");
        //		var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
        //		if (userSession == null)
        //			return await Task.FromResult(new AuthenticationState(_anonymous));


        //		var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        //			{ new Claim(ClaimTypes.Name, userSession.VenueSessionId.ToString()),


        //			}, "CustomAuth"));
        //		return await Task.FromResult(new AuthenticationState(claimsPrincipal));

        //	}

        //	catch
        //	{
        //		return await Task.FromResult(new AuthenticationState(_anonymous));
        //	}
        //}


        public async Task UpdateAuthenticationState(EntRegistration userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                await _sessionStorage.SetAsync("UserSession", userSession);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    {  new Claim(ClaimTypes.Name, userSession.UserID.ToString()),
                      new Claim (ClaimTypes.Role ,userSession.Role),

                    }));

            }
            else
            {
                await _sessionStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        //public async Task UpdateAuthenticationStateVenue(UserSession userSession)
        //{
        //	ClaimsPrincipal claimsPrincipal;

        //	if (userSession != null)
        //	{
        //		await _sessionStorage.SetAsync("VenueSession", userSession);
        //		claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        //			{  new Claim(ClaimTypes.Name, userSession.VenueSessionId.ToString()),

        //			}));

        //	}
        //	else
        //	{
        //		await _sessionStorage.DeleteAsync("VenueSession");
        //		claimsPrincipal = _anonymous;
        //	}
        //	NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        //}


    }
}