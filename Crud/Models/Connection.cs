using Microsoft.Data.SqlClient;

namespace Crud.Models
{
    public class Connection
    {
        public SqlConnection GetConnection() 
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Crudemp;Data Source=DESKTOP-NJ9AP5R\\SQLEXPRESS;Encrypt=False";
            connection.Open();
            return connection;
        }
      

    }
}
