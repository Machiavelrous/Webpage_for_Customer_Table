using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;

using System.Globalization;
using static System.Net.WebRequestMethods;


namespace WebApplication1.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<CustomerInfo> CustomerList = new List<CustomerInfo>();

        public string errorMessage = "";

        public void OnGet()
        {
            try
            { 
                string connectionString = "*Insert Connection String Here!*";

                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    string sql = "SELECT * FROM Customers";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    { 

                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            while (reader.Read()) 
                            {
                                CustomerInfo customerInfo = new CustomerInfo();
                                customerInfo.Cust_ID = reader.GetInt32(0);
                                customerInfo.First_Name = reader.GetString(1);
                                customerInfo.Last_Name = reader.GetString(2);
                                customerInfo.Email = reader.GetString(3);
                                customerInfo.Address = reader.GetString(4);
                                customerInfo.Active = reader.GetBoolean(5);

                                string x = reader.GetString(6);
                                var parsedDate = DateTime.Parse(x);
                                customerInfo.Created_Date = parsedDate.ToString("ddd M/dd/yy");

                                string y = reader.GetString(7);
                                var parsedDate2 = DateTime.Parse(y);
                                customerInfo.Last_Update = parsedDate2.ToString("ddd M/dd/yy");
                                
                                CustomerList.Add(customerInfo);
                            }
                        }
                    }
                }
            }

            catch (Exception ex) 
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }

    public class CustomerInfo
    {
        public int Cust_ID;

        public string First_Name;

        public string Last_Name;

        public string Email;

        public string Address;

        public bool Active;

        public string Created_Date;

        public string Last_Update;
    }
}
