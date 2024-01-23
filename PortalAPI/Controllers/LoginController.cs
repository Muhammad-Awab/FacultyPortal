using ClassLibraryDAL;
using ClassLibraryEnt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace PortalAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]

	public class LoginController : Controller
    {
        [HttpGet]
        [Route("getlogin/{Email}/{Password}")]
        public async Task<ActionResult> GetLogin(string Email, string Password)
        {
            ContentResult result = new ContentResult();
            SqlParameter[] sp =
            {
                new SqlParameter("@Email",Email),
                new SqlParameter("@Password",Password),
        };
            result = (ContentResult)await DalCRUD.ReadData("SP_GetLogin", sp);
            if (result != null)
            {
                return result;
            }
            else
            {
                return new ContentResult();
            }
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
            };
            await DalCRUD.CRUD("SP_SaveRegistration", sp);
        }



    }
}
