using HT6.Repositories;
using System;

namespace CallHT6
{
    class Program
    {
        static void Main(string[] args)
        {
            OrdersRepository ordersRepository = new OrdersRepository();
            var getDataFromBD = ordersRepository.GetAll();
            foreach (var item in getDataFromBD)
            {
                Console.WriteLine(item.ShipCity);
            }
        }
    }
}
