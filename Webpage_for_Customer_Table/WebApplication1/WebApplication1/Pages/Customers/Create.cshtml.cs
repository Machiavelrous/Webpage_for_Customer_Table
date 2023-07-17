using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Reflection.PortableExecutable;
using WebApplication1.Pages.Clients;


using System.Data.SqlClient;
using System.Net;
using System;

namespace WebApplication1.Pages.Customers
{
    public class CreateModel : PageModel
    {
       
        public CustomerInfo customerInfo = new CustomerInfo();

        public string errorMessage = "";

        public string successMessage = "";

        public void OnGet()
        {
        }

        
        public void OnPost() 
        {
            
            customerInfo.First_Name = Request.Form["First_Name"];
            customerInfo.Last_Name = Request.Form["Last_Name"];
            customerInfo.Email = Request.Form["Email"];
            customerInfo.Address = Request.Form["Address"];
            customerInfo.Active = true;
            customerInfo.Created_Date = DateTime.Now.ToString("dd/MM/yy");
            customerInfo.Last_Update = DateTime.Now.ToString("dd/MM/yy");


            
            if(customerInfo.First_Name.Length == 0 || customerInfo.Last_Name.Length == 0 
                || customerInfo.Email.Length == 0 || customerInfo.Address.Length == 0)
            {
                errorMessage = "All the fields are required!";
                return;
            }


            
            try
            {

                
                string connectionString = "*Insert Connection String Here!*";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    
                    string sql = "INSERT INTO Customers (First_Name, Last_Name, Email, Address, Active, Created_Date, Last_Update)"
                        + "VALUES (@First_Name, @Last_Name, @Email, @Address, @Active, @Created_Date, @Last_Update);";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        
                        command.Parameters.AddWithValue("@First_Name", customerInfo.First_Name);
                        command.Parameters.AddWithValue("@Last_Name", customerInfo.Last_Name);
                        command.Parameters.AddWithValue("@Email", customerInfo.Email);
                        command.Parameters.AddWithValue("@Address", customerInfo.Address);
                        command.Parameters.AddWithValue("@Active", customerInfo.Active);
                        command.Parameters.AddWithValue("@Created_Date", customerInfo.Created_Date);
                        command.Parameters.AddWithValue("@Last_Update", customerInfo.Last_Update);

                        
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                
                errorMessage = ex.Message;
                return;
            }


            
            customerInfo.First_Name = "";
            customerInfo.Last_Name = "";
            customerInfo.Email = "";
            customerInfo.Address = "";
            successMessage = "New Client added correctly!";


            
            Response.Redirect("/Customers/Index");
        }
    }
}
