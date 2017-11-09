using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class CategoryModel : ConnectDatabase
    {
        public List<Book> GetBookByCategory(string name_type)
        {
            
            DataClassesDataContext ctx = new DataClassesDataContext();
            var result = from p in ctx.products
                         join pt in ctx.product_types on p._type equals pt._id
                         where pt._name == name_type
                         select new { p._id,p._name,p._IMG,p._price,p._price_pages};
            List<Book> list = new List<Book>();
            foreach (var item in result)
            {
                Book book = new Book()
                {
                    _id = item._id,
                    _name = item._name,
                    _img = item._IMG,
                    _price = Convert.ToDouble(item._price),
                    _price_pages = Convert.ToDouble(item._price_pages),

                };
                list.Add(book);
            }
            return list;
          
        }

     
    }
}