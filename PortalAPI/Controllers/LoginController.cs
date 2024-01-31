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
		public async Task<IActionResult> SaveUserRegistration(EntRegistration er)
		{
			SqlParameter[] sp =
			{
		new SqlParameter("@UserID", er.UserID),
		new SqlParameter("@Name", er.Name),
		new SqlParameter("@Email", er.Email),
		new SqlParameter("@Password", er.Password),
		new SqlParameter("@EmailVerified", er.EmailVerified),
	};

			try
			{
				// Execute the stored procedure and check the ResultCode
				int resultCode = await DalCRUD.CRUD("SP_SaveRegistration", sp);

				if (resultCode == 0)
				{
					// Registration successful, return a success response
					return Ok(new { message = "Registration successful" });
				}
				else if (resultCode == 1)
				{
					// Email already exists, return a conflict response
					return Conflict(new { message = "Email already exists" });
				}
				else
				{
					// Handle other ResultCode values if needed
					return StatusCode(500, new { message = "Internal server error" });
				}
			}
			catch (Exception ex)
			{
				// Handle other exceptions if needed
				return StatusCode(500, new { message = "Internal server error" });
			}
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


		[HttpPost]
		[Route("saveuserprofile")]
		public async Task<IActionResult> SaveUserProfile(EntRegistration er)
		{
			SqlParameter[] sp =
			{
		new SqlParameter("@UserID", er.UserID),
		new SqlParameter("@Name", er.Name),
		new SqlParameter("@Location", er.Location),
		new SqlParameter("@Email", er.Email),
		new SqlParameter("@PhoneNumber", er.PhoneNumber),
		new SqlParameter("@Company", er.Company),
		new SqlParameter("@Designation", er.Designation),
	};

			try
			{
				// Execute the stored procedure and check the ResultCode
				int resultCode = await DalCRUD.CRUD("UpdateUser", sp);

				if (resultCode == 0)
				{
					// Registration successful, return a success response
					return Ok(new { message = "Update successful" });
				}
				else if (resultCode == 1)
				{
					// Email already exists, return a conflict response
					return Conflict(new { message = "Update Error" });
				}
				else
				{
					// Handle other ResultCode values if needed
					return StatusCode(500, new { message = "Internal server error" });
				}
			}
			catch (Exception ex)
			{
				// Handle other exceptions if needed
				return StatusCode(500, new { message = "Internal server error" });
			}
		}


		[HttpGet]
		[Route("getProfile/{UserId}")]
		public async Task<ActionResult<EntRegistration>> GetProfile(string UserId)
		{
			ContentResult result = new ContentResult();
			EntRegistration registration = new EntRegistration();

			registration = DalCRUD.GetProfile(UserId);


			if (registration != null)
			{

				return registration;


			}

			return NotFound();

		}


		[HttpGet]
		[Route("getemailexits/{Email}")]
		public async Task<ContentResult> GetEmailExits(string Email)
		{
			
			ContentResult result = new ContentResult();
			

			SqlParameter[] sqlParameter =
			{
                 new SqlParameter("@Email", Email)
			};
			result = (ContentResult)await DalCRUD.ReadData("SP_EmailExistChecker", sqlParameter);

			if(result!=null)
			{
				return result;
			}
			else
			{
				return new ContentResult();
			}
		}

	}
}
