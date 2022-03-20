using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WCD_Project.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Thumbnail { get; set; }
        public Category Category { get; set; }
    }
}