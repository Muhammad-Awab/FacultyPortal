using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryEnt;
using System.Reflection;

namespace ClassLibraryDAL
{
    public class DalCRUD
    {
		//the entity variables can be null , and there is no need to send the all details of table to delete data using
		//primary key, becase other attributes can be null like name description etc
		public static async Task<int> CRUD(string ProcedureName, SqlParameter[] sqlParameters)
		{
            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(ProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(sqlParameters);
                        int v=await cmd.ExecuteNonQueryAsync();
						await con.CloseAsync();
						if (v==1)
                        {	
							return 0;
                        }
                        else
                        {
                            return 1;
						}
                        
                    }
                }
            }
            catch (Exception ex)
            {
                return 1;
                Console.WriteLine($"Exception Occurred: {ex.Message}");
            }
        }
        public static async Task<ActionResult> ReadData(string ProcedureName)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(ProcedureName, conn))
                    {
                        await conn.OpenAsync();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        SqlDataReader rdr = await cmd.ExecuteReaderAsync();

                        await conn.CloseAsync();

                        if (dt.Rows.Count > 0)
                        {
                            string json = DalCustomLogics.DataTableToJSONWithJSONNet(dt);
                            return new ContentResult { Content = json, ContentType = "application/json" };

                            //this is made generic now
                            //first write code for getdatatable in CRUD, then this table is send to Dalcustomlogics to get
                            //jsonstring result,then json string will be sent to another function to make actionresult to make string into object 
                        }
                        else { return new ContentResult { Content = "", ContentType = "application/json" }; }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occurred: {ex.Message}");
                return new ContentResult { Content = "", ContentType = "application/json" };

            }

        }

        public static async Task<ActionResult> ReadData(string ProcedureName, SqlParameter[] sqlParameters)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(ProcedureName, conn))
                    {
                        await conn.OpenAsync();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(sqlParameters);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                        await conn.CloseAsync();

                        if (dt.Rows.Count > 0)
                        {
                            string json = DalCustomLogics.DataTableToJSONWithJSONNet(dt);
                            return new ContentResult { Content = json, ContentType = "application/json" };
                        }
                        else
                        {

                            dt = new DataTable();
                            string json = DalCustomLogics.DataTableToJSONWithJSONNet(dt);
                            return new ContentResult { Content = json, ContentType = "application/json" };
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occurred: {ex.Message}");
                return new ContentResult { Content = "", ContentType = "application/json" };

            }

        }

        public static EntRegistration GetLoginRecord(string Email, string Password)
        {
            EntRegistration ee = new EntRegistration();


            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GetLogin", con);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                ee.UserID = sdr["UserID"].ToString();
                ee.Email = sdr["Email"].ToString();
                ee.Password = sdr["Password"].ToString();
                ee.Location = sdr["Location"].ToString();
                ee.EmailVerified = sdr["EmailVerified"].ToString();
            }
            con.Close();

            return ee;
        }
        public static EntRegistration GetProfile(string UserId)
        {
            EntRegistration ee = new EntRegistration();


            SqlConnection con = DBHelper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GetProfileDataById", con);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                ee.Name = sdr["Name"].ToString();
                ee.Email = sdr["Email"].ToString();
                ee.Location = sdr["Location"].ToString();
                ee.PhoneNumber = sdr["PhoneNumber"].ToString();
                ee.Company = sdr["Company"].ToString();
                ee.Designation = sdr["Designation"].ToString();
            }
            con.Close();

            return ee;
        }

    }
}
    