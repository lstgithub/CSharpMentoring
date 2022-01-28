using System.ComponentModel.DataAnnotations;

namespace DataAccessLayerModule.Models
{
    class Orders
    {
        [Required]
        public int OrderID { get; set; }

        [Required, StringLength(10)]
        public string CustomerID { get; set; }

        [Required, StringLength(100)]
        public string EmployeeID { get; set; }

        [Required, StringLength(100)]
        public string ShipCity { get; set; }
    }
}
