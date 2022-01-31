using System.Data;
using System.Data.SqlClient;

namespace DALWithTests.DataAccessLayer
{
    public class OrderStatistics
    {
        public void GetHistory() => ExecuteProcedure("dbo.CustOrderHist");
        public void GetDetails() => ExecuteProcedure("dbo.CustOrdersDetail");

        public void ExecuteProcedure(string procedureName)
        {
            using (var conn = new SqlConnection(Database.ConnectionString))
            using (var command = new SqlCommand(procedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
        }

        
    }
}
