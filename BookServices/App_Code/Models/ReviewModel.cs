using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class ReviewModel : ConnectDatabase
    {

        public List<Review> GetReviews(string id)
        {
           
          
            DataClassesDataContext ctx = new DataClassesDataContext();
            var result = (from re in ctx.reviews
                          join p in ctx.products on re._id_Product equals p._id
                          join cus in ctx.customers on re._id_customer equals cus._id
                          where p._id == id orderby re._date descending
                          select new { re._id,cus._name,re._date,re._comment,re._rating,re._title}).Take(5);



            List<Review> list = new List<Review>();
            foreach (var item in result)
            {
                Review review = new Review()
                {
                    _id = item._id,
                    _name_customer = item._name,
                    _date = DateTime.Parse(item._date.ToString()),
                    _comment = item._comment,
                    _rating = int.Parse(item._rating.ToString()) * 20,
                    _title = item._title
                };
                list.Add(review);
            }
               
            
            return list;
        }

       

        public void Comment_Book(string id, int rate, string comment,string name)
        {
            try
            {
                string sql = "INSERT dbo.review([_date],[_comment],[_rating],[_title],[_id_Product],[_id_customer]) VALUES(GETDATE(),@comment, @rate, @title,@id,@cus)";
                SqlCommand sc = new SqlCommand(sql, GetConnection());
                if (sc.Connection.State == ConnectionState.Closed)
                {
                    sc.Connection.Open();
                }
                string idcus = GetCus(name);
                sc.Parameters.AddWithValue("@comment", comment);
                sc.Parameters.AddWithValue("@id", id);
                sc.Parameters.AddWithValue("@rate", rate);
                sc.Parameters.AddWithValue("@title", comment);
                sc.Parameters.AddWithValue("@cus", idcus);
                SqlDataReader sdr = sc.ExecuteReader();
                sc.Connection.Close();
            }
            catch(Exception e)
            {
                String a = "fdf";
            }
        }
        public string GetCus(string name)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
           
            var cus =( from c in ctx.customers where c._user == name select c._id).SingleOrDefault();
    
            return cus.ToString();
        }
    }
}