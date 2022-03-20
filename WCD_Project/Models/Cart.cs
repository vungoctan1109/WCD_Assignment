using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WCD_Project.Models
{
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }

        public string CartID { get; set; }

        public int ProductID { get; set; }
        public int Count { get; set; }
        public virtual Product Product { get; set; }
    }
}