using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDAL
{
    public class DBHelper
    {

        public static SqlConnection GetConnection()
        {

             SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=faculyPortal;Integrated Security=True; TrustServerCertificate=True");

             return con;




        }
    }
}