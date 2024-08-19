using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerID { get; set; }    
        public DateTime OrderDate { get; set; } 
        public string ShippingAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public User User { get; set; }
    }
}
