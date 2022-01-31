using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using HT6.Entities;

namespace HT6.Repositories
{
    public class OrdersRepository : IRepository<Order>
    {
        public IEnumerable<Order> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT [OrderID]," +
                                      "[CustomerID]," +
                                      "[EmployeeID]," +
                                      "[OrderDate]," +
                                      "[RequiredDate]," +
                                      "[ShippedDate]," +
                                      "[ShipVia]," +
                                      "[Freight]," +
                                      "[ShipName]," +
                                      "[ShipAddress]," +
                                      "[ShipCity]," +
                                      "[ShipRegion]," +
                                      "[ShipPostalCode]," +
                                      "[ShipCountry] FROM[Northwind].[dbo].[Orders]";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                List<Order> orders = new List<Order>();
                using (reader)
                {
                    
                    while (reader.Read()) 
                    {
                        DateTime ? nullDateTime = null;
                        Order order = new Order(reader.GetInt32(0), reader.GetString(1),
                            reader.GetInt32(2),
                            reader.IsDBNull(3) ? nullDateTime : reader.GetDateTime(3), 
                        reader.GetDateTime(4),
                        reader.IsDBNull(5) ? nullDateTime : reader.GetDateTime(5), 
                            reader.GetInt32(6),
                        decimal.Parse(reader.GetSqlMoney(7).ToString()), reader.GetString(8),
                        reader.GetString(9), reader.GetString(10),
                        reader.IsDBNull(11) ? null : reader.GetString(11),
                        reader.IsDBNull(12) ? null : reader.GetString(12), 
                            reader.GetString(13));
                        orders.Add(order);
                    }
                }
                reader.Close();
                return orders;
            }
        }

        public Order Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT [OrderID]," +
                                      "[CustomerID]," +
                                      "[EmployeeID]," +
                                      "[OrderDate]," +
                                      "[RequiredDate]," +
                                      "[ShippedDate]," +
                                      "[ShipVia]," +
                                      "[Freight]," +
                                      "[ShipName]," +
                                      "[ShipAddress]," +
                                      "[ShipCity]," +
                                      "[ShipRegion]," +
                                      "[ShipPostalCode]," +
                                      "[ShipCountry] FROM[Northwind].[dbo].[Orders]" +
                                      $"WHERE OrderID = {id}";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                Order order = null;
                using (reader)
                {

                    while (reader.Read())
                    {
                        DateTime? nullDateTime = null;
                        order = new Order(reader.GetInt32(0), reader.GetString(1),
                            reader.GetInt32(2),
                            reader.IsDBNull(3) ? nullDateTime : reader.GetDateTime(3),
                        reader.GetDateTime(4),
                        reader.IsDBNull(5) ? nullDateTime : reader.GetDateTime(5),
                            reader.GetInt32(6),
                        decimal.Parse(reader.GetSqlMoney(7).ToString()), reader.GetString(8),
                        reader.GetString(9), reader.GetString(10),
                        reader.IsDBNull(11) ? null : reader.GetString(11),
                        reader.IsDBNull(12) ? null : reader.GetString(12),
                            reader.GetString(13));
                    }
                    reader.Close();
                    return order;
                }
            }
        }

        public OrderInformation GetOrderInformation(int id)
        {
            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT o.[OrderID]," +
                                      "[CustomerID]," +
                                      "[EmployeeID]," +
                                      "[OrderDate]," +
                                      "[RequiredDate]," +
                                      "[ShippedDate]," +
                                      "[ShipVia]," +
                                      "[Freight]," +
                                      "[ShipName]," +
                                      "[ShipAddress]," +
                                      "[ShipCity]," +
                                      "[ShipRegion]," +
                                      "[ShipPostalCode]," +
                                      "[ShipCountry]," +
                                      "[UnitPrice]," +
                                      "[Quantity]," +
                                      "[Discount]" +
                                      "FROM [Northwind].[dbo].[Orders] o " +
                                      "JOIN [Northwind].[dbo].[Order Details] d " +
                                      "ON o.OrderID = d.OrderID " +
                                      $"WHERE o.OrderID = {id}";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                Order order = null;
                OrderDetails orderDetails = null;
                using (reader)
                {
                    while (reader.Read())
                    {
                        DateTime? nullDateTime = null;
                        order = new Order(reader.GetInt32(0), reader.GetString(1),
                            reader.GetInt32(2),
                            reader.IsDBNull(3) ? nullDateTime : reader.GetDateTime(3),
                        reader.GetDateTime(4),
                        reader.IsDBNull(5) ? nullDateTime : reader.GetDateTime(5),
                            reader.GetInt32(6),
                        decimal.Parse(reader.GetSqlMoney(7).ToString()), reader.GetString(8),
                        reader.GetString(9), reader.GetString(10),
                        reader.IsDBNull(11) ? null : reader.GetString(11),
                        reader.IsDBNull(12) ? null : reader.GetString(12),
                            reader.GetString(13));
                        orderDetails = new OrderDetails();
                        orderDetails.OrderID = reader.GetInt32(0);
                        orderDetails.UnitPrice = reader.GetSqlMoney(14);
                        orderDetails.Quantity = reader.GetInt16(15);
                        orderDetails.Discount = reader.GetFloat(16);
                    }
                }

                SqlCommand productsIDsCommand = new SqlCommand();
                productsIDsCommand.CommandText = $"SELECT [ProductID] FROM [Northwind].[dbo].[Order Details] WHERE OrderID = {id}";
                productsIDsCommand.Connection = connection;
                SqlDataReader productsIDsReader = productsIDsCommand.ExecuteReader();
                List<Product> products = new List<Product>();
                using (productsIDsReader)
                {
                    while (productsIDsReader.Read())
                    {
                        Product product = new Product();
                        product.ProductID = productsIDsReader.GetInt32(0);
                        products.Add(product);
                    }
                }

                foreach (var prod in products)
                {
                    SqlCommand productsCommand = new SqlCommand();
                    productsCommand.CommandText = $"SELECT ProductName FROM [Northwind].[dbo].Products WHERE ProductID = {prod.ProductID}";
                    productsCommand.Connection = connection;
                    SqlDataReader productsReader = productsCommand.ExecuteReader();
                    using (productsReader)
                    {
                        while (productsReader.Read())
                        {
                            prod.ProductName = productsReader.GetString(0);
                        }
                    }
                }
                return new OrderInformation(order, orderDetails, products);
            }
        }
            

        public IEnumerable<Order> Find(Func<Order, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Order Create(Order item)
        {
            string expression = "INSERT INTO[dbo].[Orders]" +
                                "([CustomerID]" +
                                ",[EmployeeID]" +
                                ",[RequiredDate]" +
                                ",[ShipVia]" +
                                ",[Freight]" +
                                ",[ShipName]" +
                                ",[ShipAddress]" +
                                ",[ShipCity]" +
                                ",[ShipRegion]" +
                                ",[ShipPostalCode]" +
                                ",[ShipCountry])" +
                                $"VALUES ('{item.CustomerID}', {item.EmployeeID}, '{item.RequiredDate}', {item.ShipVia}, CAST({item.Freight} AS money)," +
                                $" '{item.ShipName}', '{item.ShipAddress}', '{item.ShipCity}','{item.ShipRegion}', '{item.ShipPostalCode}', '{item.ShipCountry}')";
            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = expression;
                command.Connection = connection;
                command.ExecuteNonQuery();
            }

            expression = $"select OrderID FROM Orders WHERE RequiredDate = '{item.RequiredDate}'";
            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = expression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        item.OrderID = reader.GetInt32(0);
                    }
                }

                return item;
            }
        }

        public void Update(Order item)
        {
            if (item.Status == Statuses.New)
            {
                string expression = "UPDATE [dbo].[Orders] SET " +
                                    $"[CustomerID] = '{item.CustomerID}'" +
                                    $",[EmployeeID] = {item.EmployeeID}" +
                                    $",[RequiredDate] = '{item.RequiredDate}'" +
                                    $",[ShipVia] = {item.ShipVia}" +
                                    $",[Freight] = {item.Freight}" +
                                    $",[ShipName] = '{item.ShipName}'" +
                                    $",[ShipAddress] = '{item.ShipAddress}'" +
                                    $",[ShipCity] = '{item.ShipCity}'" +
                                    $",[ShipRegion] = '{item.ShipRegion}'" +
                                    $",[ShipPostalCode] = '{item.ShipPostalCode}'" +
                                    $",[ShipCountry] = '{item.ShipCountry}' " +
                                    $"WHERE OrderID = {item.OrderID}";
                using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = expression;
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            Order order = Get(id);
            if (order.Status == Statuses.New || order.Status == Statuses.InProgress)
            {
                string expression = $"DELETE FROM Orders WHERE OrderID='{id}'";
                using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = expression;
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void SetInProgress(Order order)
        {
            string expression = "UPDATE [dbo].[Orders] SET " +
                                $"[OrderDate] = '{DateTime.Now}' " +
                                $"WHERE OrderID = {order.OrderID}";
            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = expression;
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
            order.ChangeStatus(Statuses.InProgress);
        }

        public void SetComplete(Order order)
        {
            string expression = "UPDATE [dbo].[Orders] SET " +
                                $"[ShippedDate] = {DateTime.Now}" +
                                $"WHERE OrderID = {order.OrderID}";
            using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = expression;
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
            order.ChangeStatus(Statuses.Completed);
        }
    }
}
