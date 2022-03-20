using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCD_Project.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public decimal Total { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}