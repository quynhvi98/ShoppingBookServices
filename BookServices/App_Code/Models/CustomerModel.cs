using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class CustomerModel : ConnectDatabase
    {
        public bool LoginWithAccAndPass(String userAccount, String password)
        {
            
            DataClassesDataContext ctx = new DataClassesDataContext();
            var result = (from c in ctx.customers
                         where c._user == userAccount && c._password == password
                         select c).SingleOrDefault();

            if (result == null)
            {
              
              
                var result1 = (from c in ctx.customers
                              where c._email == userAccount && c._password == password
                              select c).SingleOrDefault();
              
                if (result1 == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return true;
            }
          

          
        }
        public void creatCustomer(String email,String user, String name)
        {
            String sql = "INSERT dbo.customer( [_email],_user ,[_name],_password  )VALUES  ( @email,@user,@name,@password )";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@password","123456");
            int reuslt = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public int GetIDCustomerByEmail(String email)
        {
            try
            {
                
                DataClassesDataContext ctx = new DataClassesDataContext();
                var result = (from c in ctx.customers where c._email == email select c).SingleOrDefault();
                if (result != null)
                {
                    return result._id;
                }
            }
            catch (Exception e)
            {
                return -1;
            }
            return -1;
        }
        public String GetuserByEmail(String email)
        {
            try
            {
                DataClassesDataContext ctx = new DataClassesDataContext();
                var result = (from c in ctx.customers where c._email == email select c).SingleOrDefault();
                if (result != null)
                {
                    return result._user;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }
        public int GetIDCustomerByUser(String User)
        {
            try
            {
                DataClassesDataContext ctx = new DataClassesDataContext();
                var result = (from c in ctx.customers where c._user == User select c).SingleOrDefault();
                if (result != null)
                {
                    return result._id;
                }
            }
            catch (Exception e)
            {
                return -1;
            }
            return -1;
        }
        public void Signin(String email, String user, String password, String name)
        {
            String sql = "INSERT dbo.customer( [_email],_user ,[_name],_password  )VALUES  ( @email,@user,@name,@password )";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@password", password);
            int reuslt = cmd.ExecuteNonQuery();
        }

    }
}