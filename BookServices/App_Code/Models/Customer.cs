using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class Customer
    {
        public int id { get; set; }
        public String email { get; set; }
        public String user { get; set; }
        public String password { get; set; }
        public String name { get; set; }
        public String status { get; set; }
        public double total_bill { get; set; }
        public string address_full { get; set; }
       

        //public string id { get; set; }
        //public String email { get; set; }
        //public String user { get; set; }
        //public String password { get; set; }
        //public String name { get; set; }
        //public String status { get; set; }
    }
}