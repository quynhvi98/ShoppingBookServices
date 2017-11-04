using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class NewsModel : ConnectDatabase
    {
        public List<News> GetListNews(int type)
        {

            DataClassesDataContext ctx = new DataClassesDataContext();
            List<News> list = new List<News>();
            News news = null;
            IQueryable<book_new> listNew = null;
          
            
            if (type == 1)             
                listNew = (from bn in ctx.book_news orderby bn.id_new descending select bn).Take(4);
            if (type == 2)
                listNew = from bn in ctx.book_news select bn;
            if (type == 3)
                listNew = (from bn in ctx.book_news orderby bn.id_new descending select bn).Take(3);
            if (type == 4)
                listNew = (from bn in ctx.book_news orderby bn.id_new descending select bn).Take(8);

            foreach (var item in listNew)
            {
                news = new News()
                {
                    _id = item.id_new,
                    _name = item.title,
                    _img = item.img,
                    _date = DateTime.Parse(item.date_new.ToString()),
                    _content = item.content_new,
                    _author = item.author_new,
                    _short_title = item.short_title
                };
                list.Add(news);
            }
         
            return list;
        }

        public News GetNewsByID(string id_news)
        {
            News news = null;
            DataClassesDataContext ctx = new DataClassesDataContext();
            string sql = "SELECT id_new,title,img,date_new,content_new,author_new FROM dbo.book_news WHERE id_new = '" + id_news + "'";
            var result = (from n in ctx.book_news where n.id_new == Int32.Parse(id_news) select n).SingleOrDefault();
            if (result!=null)
            {
                news = new News()
                {
                    _id = result.id_new,
                    _name = result.title,
                    _img = result.img,
                    _date = DateTime.Parse(result.date_new.ToString()),
                    _content = result.content_new,
                    _author = result.author_new
                };

            }
            return news;
        }
    }
}
