using System.Data.SqlClient;

namespace DALWithTests.DataAccessLayer
{
    public static class Database
    {
        public static string ConnectionString =>
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = Northwind; Integrated Security=True";

        public static void OpenConnection()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
            }
        }
    }
}
