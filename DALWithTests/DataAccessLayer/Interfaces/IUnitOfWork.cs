using System;
using DALWithTests.DataAccessLayer.Entities;

namespace DALWithTests.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> Customers { get; }
        IRepository<Order> Orders { get; }
        IRepository<Product> Products { get; }
        IRepository<Supplier> Suppliers { get; }
        IRepository<Shipper> Shippers { get; }
        IRepository<Territory> Territories { get; }
        void Save();
    }
}
