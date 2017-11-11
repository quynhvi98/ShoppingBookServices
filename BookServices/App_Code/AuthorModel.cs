using bookstore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AuthorModel
/// </summary>
public class AuthorModel : DataProcess
{
    public AuthorModel()
    {
    }

    public List<String> GetlistAuthor()
    {
        List<String> list = new List<string>();
        String sql = "SELECT * FROM author";
        SqlCommand cmd = new SqlCommand(sql, GetConnection());
        cmd.Connection.Open();
        SqlDataReader rd = cmd.ExecuteReader();
        while (rd.Read())
        {
            list.Add(rd.GetString(1));
        }
        cmd.Connection.Close();
        return list;
    }
    public int GetIdProductTypeByName(String Name)
    {
        List<String> list = new List<string>();
        String sql = "SELECT * FROM author where _name_author=@name";
        SqlCommand cmd = new SqlCommand(sql, GetConnection());
        cmd.Connection.Open();
        cmd.Parameters.AddWithValue("name", Name);
        SqlDataReader rd = cmd.ExecuteReader();
        int index = -1;
        while (rd.Read())
        {
            index = rd.GetInt32(0);
        }
        cmd.Connection.Close();
        return index;
    }
    public bool AddAuthors(Author author)
    {
        string sql = " INSERT INTO dbo.author( _name_author, _description_author,_IMG) VALUES (@name, @description, @img ) ";

        SqlCommand cmd = new SqlCommand(sql, GetConnection());
        if (cmd.Connection.State == ConnectionState.Closed)
            cmd.Connection.Open();
        cmd.Parameters.AddWithValue("name", author.name_author);
        cmd.Parameters.AddWithValue("description", author.description);
        cmd.Parameters.AddWithValue("img",author.img_author);
        int result = cmd.ExecuteNonQuery();
        cmd.Connection.Close();
        return result > 0;
    }
    public bool HasIdAuthor(Author author)
    {
        String sql = "SELECT * FROM author WHERE _id='" + author.id_author + "'";
        SqlCommand cmd = new SqlCommand(sql, GetConnection());
        if (cmd.Connection.State == ConnectionState.Closed)
        {
            cmd.Connection.Open();
        }
        //cmd.Parameters.AddWithValue("id", author.id);
        SqlDataReader rd = cmd.ExecuteReader();
        bool reuslt = rd.HasRows;
        cmd.Connection.Close();
        return reuslt;

    }

    public bool UpdateAuthor(Author author)
    {
        string sql = "UPDATE dbo.author SET [_name_author]= @name, [_description_author]= @description where _id=@id";
        SqlCommand cmd = new SqlCommand(sql, GetConnection());
        if (cmd.Connection.State == ConnectionState.Closed)
            cmd.Connection.Open();
        cmd.Parameters.AddWithValue("id", author.id_author);
        cmd.Parameters.AddWithValue("name", author.name_author);
        cmd.Parameters.AddWithValue("description", author.description);
        int result = cmd.ExecuteNonQuery();
        cmd.Connection.Close();
        return result > 0;
    }
    public List<Author> SearchAuthor(string query, int type)
    {
        List<Author> list = new List<Author>();
        DataClassesDataContext ctx = new DataClassesDataContext();
        if (type == 1)
        {
            //sql = "SELECT _id, _name_author, _description_author from author  WHERE _id = '" + query + "'";
            var result = from author in ctx.authors where author._id == int.Parse(query) select new { author._id, author._name_author, author._description_author};
            foreach (var item in result)
            {
                Author au = new Author()
                {
                    id_author = item._id,
                    name_author = item._name_author,
                    description = item._description_author,
                };
                list.Add(au);
            }
            
        }
        if (type == 2)
        {
            //sql = "SELECT _id, _name_author, _description_author from author  WHERE _name_author LIKE N'%" + query + "%'";
            var result = from author in ctx.authors where author._name_author.Contains(@query) select author;
            foreach (var item in result)
            {
                Author au = new Author()
                {
                    id_author = item._id,
                    name_author = item._name_author,
                    description = item._description_author,
                };
                list.Add(au);
            }
        }
        return list;
    }
}