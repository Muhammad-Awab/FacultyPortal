using ClassLibraryDAL;
using ClassLibraryEnt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using ClassLibraryEnt;

namespace PortalAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]

	public class LoginController : Controller
    {
        [HttpGet]
        [Route("getlogin/{Email}/{Password}")]
        public async Task<ActionResult<EntRegistration>> GetLogin(string Email, string Password)
        {
            ContentResult result = new ContentResult();
            EntRegistration registration = new EntRegistration();

            registration = DalCRUD.GetLoginRecord(Email, Password);
            
            
            if (registration != null)
            {
                               
               return registration;
                

            }
            //else
            //{
                return NotFound();
            //}
        }



        [HttpPost]
        [Route("saveuserregistration")]
        public async Task SaveUserRegistration(EntRegistration er)
        {
            SqlParameter[] sp =
            {
                new SqlParameter("@UserID",er.UserID),
                new SqlParameter("@Name",er.Name),
                new SqlParameter("@Email",er.Email),
                 new SqlParameter("@Password",er.Password),
                new SqlParameter("@Location",er.Location),
                new SqlParameter("@EmailVerified",er.EmailVerified),

			};
            await DalCRUD.CRUD("SP_SaveRegistration", sp);
        }


		[HttpPost]
		[Route("updateuserregistration")]
		public async Task UpdateUserRegistration(EntRegistration er)
		{
			SqlParameter[] sp =
			{
				
				new SqlParameter("@Email",er.Email),
				new SqlParameter("@EmailVerified",er.EmailVerified)

			};
			await DalCRUD.CRUD("SP_UpdateRegistration", sp);
		}


	}
}
