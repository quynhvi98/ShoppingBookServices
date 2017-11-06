using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class AuthorModel : ConnectDatabase
    {
        public List<Author> GetAuthors()
        {
          
            DataClassesDataContext context = new DataClassesDataContext();
            var author = (from a in context.authors select a).Take(4);
            return Table_to_List(author.ToList<author>());
        }

        public List<Author> Table_to_List(List<author> dt)
        {
            List<Author> list = new List<Author>();
            foreach (author item in dt)
            {
                Author author = new Author();
                author.id_author = item._id.ToString();
                author.name_author = item._name_author.ToString();
                author.img_author = item._IMG.ToString();
                try
                {
                    author.description = item._description_author.ToString();

                }
                catch (Exception)
                {
                    author.description = null;
                }
                list.Add(author);
            }
            return list;
        }
    }
}