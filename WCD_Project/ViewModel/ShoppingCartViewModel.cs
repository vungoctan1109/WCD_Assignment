using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WCD_Project.Models;

namespace WCD_Project.ViewModel
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}