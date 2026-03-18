using System.Diagnostics;
using Crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Crud.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveData(Employe employe)
        {
            //SqlConnection connection = new SqlConnection();
            //connection.ConnectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Crudemp;Data Source=DESKTOP-NJ9AP5R\\SQLEXPRESS;Encrypt=False";
            Connection connection = new Connection();
            SqlCommand command = new SqlCommand();
            command.CommandText = $"insert into empinfo values(@Name,@Email,@Empcode,@Createdat,@Isactive)";
            command.Connection = connection.GetConnection();
            command.Parameters.AddWithValue("@Name", employe.Name);
            command.Parameters.AddWithValue("@Email", employe.Email);
            command.Parameters.AddWithValue("@Empcode", employe.Empcode);
            command.Parameters.AddWithValue("@Createdat", employe.Createdat);
            command.Parameters.AddWithValue("@Isactive", employe.Isactive);

            int value = command.ExecuteNonQuery();
            connection.GetConnection().Close();
            if (value > 0)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View("Index");
            }

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginData(Employe employe)
        {
            Connection connection = new Connection();
            SqlCommand command = new SqlCommand();
            command.CommandText = $"Select count(*) from empinfo where Email=@Email AND Empcode=@Empcode";
            command.Connection = connection.GetConnection();
            command.Parameters.AddWithValue("@Email", employe.Email);
            command.Parameters.AddWithValue("@Empcode", employe.Empcode);
            int count = (int)command.ExecuteScalar();

            if (count > 0)
            {
                return RedirectToAction("Edit", "Home", new { Gmail = employe.Email });


            }
            else
            {
                ViewBag.Message = "Invalid email or empcode";
                return View("Login");
            }

        }
        [HttpGet]
        public IActionResult Edit(string gmail)
        {
            Employe employe = new Employe();
            Connection connection = new Connection();
            SqlCommand command = new SqlCommand();
            command.CommandText = $"Select * from empinfo where Email=@Email";
            command.Connection = connection.GetConnection();
            command.Parameters.AddWithValue("@Email", gmail);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                employe.Name = reader["Name"].ToString();
                employe.Email = reader["Email"].ToString();
                employe.Empcode = reader["Empcode"].ToString();
                employe.Createdat = Convert.ToDateTime(reader["Createdat"]);
                employe.Isactive = Convert.ToBoolean(reader["Isactive"]);
                connection.GetConnection().Close();
            }

            return View(employe);
        }
        [HttpPut]
        public IActionResult Update(Employe employe)
        {
            Connection connection = new Connection();
            SqlCommand command = new SqlCommand();
            command.CommandText = $"Update empinfo SET Name=@Name,Email=@Email,Empcode=@Empcode,Createdat=@Createdat,Isactive=@Isactive where Email=@gmail";
            command.Connection = connection.GetConnection();
            command.Parameters.AddWithValue("@Name", employe.Name);
            command.Parameters.AddWithValue("@Email", employe.Email);
            command.Parameters.AddWithValue("@Empcode", employe.Empcode);
            command.Parameters.AddWithValue("@Createdat", employe.Createdat);
            command.Parameters.AddWithValue("@Isactive", employe.Isactive);
            command.Parameters.AddWithValue("@gmail", employe.Email);
            int value = command.ExecuteNonQuery();
            connection.GetConnection().Close();
            return View("Edit");
        }
        public IActionResult Delete(Employe employe)
        {
            Connection connection = new Connection();
            SqlCommand command = new SqlCommand();
            command.CommandText = $"Delete from empinfo where Email=@Email";
            command.Connection = connection.GetConnection();
            command.Parameters.AddWithValue("@Email", employe.Email);
            int value = command.ExecuteNonQuery();
            connection.GetConnection().Close();
            return RedirectToAction("Login","Home");

        }
    }

}   
