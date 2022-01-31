using System.Configuration;
using System.Data.SqlClient;

namespace HT6
{
    public static class Database
    {
        public static string ConnectionString =>
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = Northwind; Integrated Security=True";//ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static void OpenConnection()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
            }
        }
    }
}
