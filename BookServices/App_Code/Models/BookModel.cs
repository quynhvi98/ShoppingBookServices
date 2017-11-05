using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using bookstore.Models;

namespace bookstore.Models
{
    public class BookModel : ConnectDatabase
    {
        public List<Book> GetBooks(int query1, string query2)
        {
            List<Book> list = new List<Book>();
            DataClassesDataContext context = new DataClassesDataContext();
            var result = (from b in context.products
                          join pt in context.product_types on b._type equals pt._id
                          join cp in context.category_products on b._id equals cp._id_product
                          join pd in context.producers on b._id_producer equals pd._id
                          join au in context.authors on b._author_id equals au._id
                          join ca in context.categories on cp._id_category equals ca._id
                          where cp._id_category == query2
                          select new
                          {
                              b._id,
                              bookname = b._name,
                              b._IMG,
                              b._price,
                              b._pages,
                              b._weight,
                              b._content,
                              b._status,
                              b._year_of_creation,
                              producername = pd._name,
                              product_typename = pt._name,
                              authorname = au._name_author,
                              categoryname = ca._name
                          }).Take(query1);
            foreach (var item in result)
            {
                Book book = new Book()
                {
                    _id = item._id,
                    _name = item.bookname,
                    _img = item._IMG,
                    _price = item._price,
                    _pages = item._pages,
                    _weight = item._weight * 1000,
                    _content = item._content,
                    _status = item._status,
                    _year_of_creation = item._year_of_creation.ToString(),
                    _producer = item.producername,
                    _type = item.product_typename,
                    _author = item.authorname,
                    _category = item.categoryname
                };
                list.Add(book);
            }
            return list;
        }



        public double GetRating(string id_book)
        {
            double a = 0;

            try
            {
                DataClassesDataContext context = new DataClassesDataContext();
                var rat = (context.reviews.Where(r => (r._id_Product == id_book)).Average(r => r._rating));
                String save = rat.ToString();
                a = Double.Parse(save);
            }
            catch (Exception e)
            {
                a = 5;
            }

            return a;
        }

        public Book GetBookByID(string id)
        {
            Book book = null;

            DataClassesDataContext ctx = new DataClassesDataContext();
            var result = from p in ctx.products
                         join pt in ctx.product_types on p._type equals pt._id
                         join pd in ctx.producers on p._id_producer equals pd._id
                         join au in ctx.authors on p._author_id equals au._id
                         where p._id == id
                         select new { p._id, productname = p._name, p._IMG, p._price, p._pages, p._weight, p._content, p._status, p._year_of_creation, producername = pd._name, producttypename = pt._name, au._name_author };
            foreach (var item in result)
            {
                book = new Book()
                {
                    _id = item._id,
                    _name = item.productname,
                    _img = item._IMG,
                    _price = item._price,
                    _pages = item._pages,
                    _weight = item._weight,
                    _content = item._content,
                    _status = item._status,
                    _year_of_creation = item._year_of_creation.ToString(),
                    _producer = item.producername,
                    _type = item.producttypename,
                    _author = item._name_author,

                };
            }

            return book;
        }

        public List<Book> searchByName(string query)
        {
            string sql = "SELECT  product.[_id],dbo.product.[_name],[_IMG],AVG([_rating]) AS _rating FROM dbo.product LEFT JOIN dbo.review ON review.[_id_Product] = product.[_id] WHERE [_name] LIKE N'%" + query + "%' GROUP BY  dbo.product.[_id],dbo.product.[_name],dbo.product.[_IMG]";
            SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            
            return Table_to_List_Search(dt);
        }

        public List<Book> Table_to_List_Search(DataTable dt)
        {
            List<Book> list = new List<Book>();
            foreach (DataRow item in dt.Rows)
            {
                Book book = new Book()
                {
                    _id = item[0].ToString(),
                    _name = item[1].ToString(),
                    _img = item[2].ToString(),
                };
                list.Add(book);
            }
            return list;
        }

        public List<Book> GetBooksForSlider()
        {
            List<Book> list = new List<Book>();
            DataClassesDataContext ctx = new DataClassesDataContext();
            var result = (from b in ctx.products select new { b._id, b._name, b._IMG }).Take(12);
            foreach (var item in result)
            {
                Book book = new Book()
                {
                    _id = item._id,
                    _name = item._name,
                    _img = item._IMG,

                };
                list.Add(book);
            }
            return list;
        }
    }
}