using Microsoft.EntityFrameworkCore;

namespace DataAccessLayerModule.Models
{
    class OrdersContext : DbContext
    {
        public DbSet<Orders> Orders { get; set; }
    }
}
