using System;
using DALWithTests.DataAccessLayer.Repositories;

namespace DALWithTests
{
    class Program
    {
        static void Main(string[] args)
        {
            OrdersRepository ordersRepository = new OrdersRepository();
            var getDataFromDB = ordersRepository.GetAll();
            foreach (var item in getDataFromDB)
                Console.WriteLine(item.ShipCity);
        }
    }
}
