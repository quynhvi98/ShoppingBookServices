using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class Author
    {
        public int id_author { get; set; }
        public string name_author { get; set; }
        public string img_author { get; set; }
        public string description { get; set; }

        //public string id { get; set; }
        //public String name { get; set; }
        //public String description { get; set; }
        //public String img { get; set; }
    }
}