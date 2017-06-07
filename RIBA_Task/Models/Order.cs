using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace RIBA_Task.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public int Quantity1 { get; set; }
        public int Quantity2 { get; set; }
        public int Quantity3 { get; set; }
        public int Quantity4 { get; set; }
        public decimal Cost { get; set; }
    }

    public class OrderDBContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
    }

}