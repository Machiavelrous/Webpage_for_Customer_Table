using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


using System.Data.SqlClient;
using WebApplication1.Pages.Clients;

namespace WebApplication1.Pages.Customers
{
    public class EditModel : PageModel
    {
        
        public CustomerInfo customerInfo = new CustomerInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public static int customer_ID;
        public static string Created_Date_String = "";


        public void OnGet()
        {
            
            string cust_ID = Request.Query["Cust_ID"];
            customer_ID = int.Parse(cust_ID);

            
            try
            {
                string connectionString = "*Insert Connection String Here!*";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Customers WHERE Cust_ID = @Cust_ID;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Cust_ID", cust_ID);


                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customerInfo.Cust_ID = reader.GetInt32(0);
                                customerInfo.First_Name = reader.GetString(1);
                                customerInfo.Last_Name = reader.GetString(2);
                                customerInfo.Email = reader.GetString(3);
                                customerInfo.Address = reader.GetString(4);

                                
                                customerInfo.Active = reader.GetBoolean(5);

                                
                                string x = reader.GetString(6);
                                var parsedDate = DateTime.Parse(x);
                                customerInfo.Created_Date = parsedDate.ToString("ddd M/dd/yy");


                                
                                Created_Date_String = parsedDate.ToString("dd/MM/yy");


                                
                                string y = reader.GetString(7);
                                var parsedDate2 = DateTime.Parse(y);
                                customerInfo.Last_Update = parsedDate2.ToString("ddd M/dd/yy");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost()
        {

            if (customer_ID == 0)
            {
                errorMessage = "customer_ID == 0!!!!!";
                return;
            }
            if (Created_Date_String.Length == 0)
            {
                errorMessage = "Created_Date_String.Length == 0!!!!!";
                return;
            }

            customerInfo.Cust_ID = customer_ID;
            customerInfo.First_Name = Request.Form["First_Name"];
            customerInfo.Last_Name = Request.Form["Last_Name"];
            customerInfo.Email = Request.Form["Email"];
            customerInfo.Address = Request.Form["Address"];
            customerInfo.Created_Date = Created_Date_String;

            try 
            {
                
                customerInfo.Active = bool.Parse(Request.Form["Active"]);
            }
            catch(Exception ex) 
            {
                errorMessage = "Please key in either \"True\" or \"False\" for the \"Active\" field.";
                return;
            }

            customerInfo.Last_Update = DateTime.Now.ToString("dd/MM/yy");


            if (customerInfo.First_Name.Length == 0 || customerInfo.Last_Name.Length == 0
                || customerInfo.Email.Length == 0 || customerInfo.Address.Length == 0
                || customerInfo.Cust_ID == 0)
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
                    string sql = "UPDATE Customers " +
                        "SET First_Name = @First_Name, " +
                        "Last_Name = @Last_Name, " +
                        "Email = @Email, " +
                        "Address = @Address, " +
                        "Active = @Active, " +
                        "Created_Date = @Created_Date, " +
                        "Last_Update = @Last_Update " +
                        "WHERE Cust_ID = @Cust_ID;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Cust_ID", customerInfo.Cust_ID);
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

            
            Response.Redirect("/Customers/Index");
        }
    }
}
