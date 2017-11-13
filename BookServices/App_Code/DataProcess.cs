using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using bookstore.Models;
/// <summary>
/// Summary description for DataProcess
/// </summary>
public class DataProcess
{
    public DataProcess()
    {


    }
    public SqlConnection GetConnection()
    {
        SqlConnection conn = new SqlConnection("server=localhost; database=BookASMWAD;" + "uid=sa; pwd=123456");
        return conn;
    }


    //Order
    public List<Order> GetListOrder()
    {
        DataClassesDataContext ctx = new DataClassesDataContext();
        //string sql = "SELECT _id,[_total_bill],_content,[_status_paymen],[_status_delivery]," +
        //    "[_status_bill],[_date] FROM dbo.order_product " +
        //    "JOIN dbo.paymen ON paymen.[_payment_id] = order_product.[_payment_id]";

        var result = from o in ctx.order_products
                     join p in ctx.paymens on o._payment_id equals p._payment_id
                     select new { o._id, o._total_bill, p._content, o._status_paymen, o._status_delivery, o._status_bill, o._date };
        List<Order> list = new List<Order>();
        foreach (var item in result)
        {
            Order order = new Order()
            {
                id = item._id,
                total_bill = item._total_bill,
                content = item._content,
                status_payment = item._status_paymen,
                status_delivery = item._status_delivery,
                status_bill = item._status_bill,
                date = item._date,
            };
            list.Add(order);
        }
        return list;
    }

    public bool DeleteOrder(string id)
    {
        //string sql = "UPDATE dbo.order_product " +
        //   "SET _status_bill = N'Đã hủy' WHERE [_id] = @id";
        try
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            var result = (from op in ctx.order_products
                          where op._id == int.Parse(id)
                          select op).SingleOrDefault();
            result._status_bill = "Đã hủy";
            ctx.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }




    }

    public bool UpdateOrderProduct(string id, string payment_type, string status_payment, string status_delivery)
    {
        //string sql = "UPDATE dbo.order_product SET _payment_id=@pi, _status_paymen=@sp,_status_delivery=@sd WHERE _id=@id";
        DataClassesDataContext ctx = new DataClassesDataContext();
        order_product order = (from op in ctx.order_products
                               where op._id == int.Parse(id)
                               select op).SingleOrDefault();

        order._payment_id = int.Parse(payment_type);
        order._status_paymen = status_payment;
        order._status_delivery = status_delivery;
        ctx.SubmitChanges();
        return true;
    }

    public List<RefProductOrder> OrderDetailsByID(string id)
    {
        string sql = "SELECT product.[_id],product.[_name],[_quantity],dbo.ref_product_order.[_price],([_quantity]*dbo.ref_product_order.[_price]) AS total FROM dbo.product JOIN dbo.ref_product_order ON ref_product_order.[_id_product] = product.[_id] WHERE [_id_order]='" + id + "'";
        SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection());
        DataTable dt = new DataTable();
        da.Fill(dt);
        List<RefProductOrder> list = new List<RefProductOrder>();
        foreach (DataRow item in dt.Rows)
        {
            RefProductOrder order = new RefProductOrder()
            {
                _id_order = int.Parse(item[0].ToString()),
                _name_product = item[1].ToString(),
                _quantity = int.Parse(item[2].ToString()),
                _price = double.Parse(item[3].ToString()),
                _total = double.Parse(item[4].ToString())
            };
            list.Add(order);
        }
        return list;
    }

    public List<string> GetInfoCustomer_Order(string id)
    {
        string sql = "SELECT customer.[_name],[_adddress_full],[_date],[_content],[_total_bill] FROM dbo.customer JOIN dbo.customer_address ON customer_address.[_id_customer] = customer.[_id] JOIN dbo.order_product ON order_product.[_customer_id] = customer.[_id] JOIN dbo.paymen ON paymen.[_payment_id] = order_product.[_payment_id] WHERE order_product.[_id]='" + id + "'";
        SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection());
        DataTable dt = new DataTable();
        da.Fill(dt);
        List<string> customer = new List<string>();
        foreach (DataRow item in dt.Rows)
        {
            for (int i = 0; i < 5; i++)
                customer.Add(item[i].ToString());
        }
        return customer;
    }

    public List<Order> GetSortData(string sort_type)
    {
        DataClassesDataContext ctx = new DataClassesDataContext();
        //var result = from o in ctx.order_products orderby sort_type ascending
        //             join p in ctx.paymens on o._payment_id equals p._payment_id 
        //             select new { o._id, o._total_bill, p._content, o._status_paymen, o._status_delivery, o._status_bill, o._date };
        string sql = "SELECT _id,[_total_bill],_content,[_status_paymen],[_status_delivery],[_status_bill],[_date] FROM dbo.order_product JOIN dbo.paymen ON paymen.[_payment_id] = order_product.[_payment_id] ORDER BY " + sort_type + " ASC";
        SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection());
        DataTable dt = new DataTable();
        da.Fill(dt);
        List<Order> list = new List<Order>();
        foreach (DataRow item in dt.Rows)
        {
            Order order = new Order()
            {
                id = int.Parse(item[0].ToString()),
                total_bill = double.Parse(item[1].ToString()),
                content = item[2].ToString(),
                status_payment = item[3].ToString(),
                status_delivery = item[4].ToString(),
                status_bill = item[5].ToString(),
                date = DateTime.Parse(item[6].ToString())
            };
            list.Add(order);
        }
        return list;
    }

    public List<Order> SearchOrder(string query, int type)
    {
        string sql = "";
        if (type == 1)
        {
            sql = "SELECT _id,[_total_bill],_content,[_status_paymen],[_status_delivery],[_status_bill],[_date] FROM dbo.order_product JOIN dbo.paymen ON paymen.[_payment_id] = order_product.[_payment_id] WHERE _id = '" + query + "'";
        }
        if (type == 2)
        {
            sql = "SELECT dbo.order_product._id,[_total_bill],_content,[_status_paymen],[_status_delivery],[_status_bill],[_date] FROM dbo.order_product JOIN dbo.paymen ON paymen.[_payment_id] = order_product.[_payment_id] JOIN dbo.customer ON customer.[_id] = order_product.[_customer_id] WHERE [_name] LIKE N'%" + query + "%'";
        }
        List<Order> list = new List<Order>();
        SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection());
        DataTable dt = new DataTable();
        da.Fill(dt);
        foreach (DataRow item in dt.Rows)
        {
            Order order = new Order()
            {
                id = int.Parse(item[0].ToString()),
                total_bill = double.Parse(item[1].ToString()),
                content = item[2].ToString(),
                status_payment = item[3].ToString(),
                status_delivery = item[4].ToString(),
                status_bill = item[5].ToString(),
                date = DateTime.Parse(item[6].ToString()),

            };
            list.Add(order);
        }
        return list;
    }

    public List<string> DataTableToList(DataTable dt)
    {
        List<string> list = new List<string>();
        foreach (DataRow item in dt.Rows)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
                list.Add(item[i].ToString());
        }
        return list;
    }
    //customer
    public List<Customer> GetCustomerInformation()
    {
        List<Customer> list = new List<Customer>();
        string sql = "SELECT dbo.customer._id, dbo.customer.[_email], dbo.customer.[_user], dbo.customer.[_name]," +
            " SUM(dbo.order_product.[_total_bill]) AS _total_bill, dbo.customer_address.[_adddress_full], dbo.customer._status " +
            "FROM dbo.customer JOIN dbo.customer_address ON dbo.customer.[_id]= dbo.customer_address.[_id_customer] " +
            "LEFT JOIN dbo.order_product ON order_product.[_customer_id] = customer.[_id] GROUP BY dbo.customer.[_id], " +
            "dbo.customer.[_email],[_user], customer.[_name], dbo.customer_address.[_adddress_full], dbo.customer._status";
        SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection());
        DataTable dt = new DataTable();
        da.Fill(dt);
        foreach (DataRow item in dt.Rows)
        {
            Customer customer = new Customer();
            customer.id = int.Parse(item[0].ToString());
            customer.email = item[1].ToString();
            customer.user = item[2].ToString();
            customer.name = item[3].ToString();
            try
            {
                customer.total_bill = double.Parse(item[4].ToString());
            }
            catch (Exception)
            {
                customer.total_bill = 0;

            }

            customer.address_full = item[5].ToString();
            customer.status = item[6].ToString();
            list.Add(customer);
        }
        return list;
    }
    //producer
    public List<Producer> GetProducerInformation()
    {
        List<Producer> list = new List<Producer>();
        DataClassesDataContext ctx = new DataClassesDataContext();
        var result = from producer in ctx.producers select producer;
        foreach (var item in result)
        {
            Producer producer = new Producer()
            {
                id = item._id,
                name = item._name,
                description = item._description

            };
            list.Add(producer);
        }
        return list;
    }
    //author
    public List<Author> GetAuthorInformation()
    {
        List<Author> list = new List<Author>();
        DataClassesDataContext ctx = new DataClassesDataContext();
        var result = from au in ctx.authors select au;
        foreach (var item in result)
        {
            Author author = new Author()
            {
                id_author = item._id,
                name_author = item._name_author,
                img_author = item._IMG,
                description = item._description_author,
            };
            list.Add(author);
        }
        return list;
    }


    //lethanh tạo get id dang nhap
    public bool LoginWithAccAndPass(String userAccount, String password)
    {
        DataClassesDataContext ctx = new DataClassesDataContext();
        var result = (from admin in ctx.administrators
                      where admin._user == userAccount && admin._password == password
                      select admin).SingleOrDefault();
        //String sql = "SELECT* FROM administrator WHERE _user = @userAccount AND _password = @password";
        //if (rd.HasRows != true)
        if (result==null)
        {
            //String sql1 = "SELECT * FROM administrator WHERE  _email=@userAccount AND _password=@password";
            var result1 = (from admin in ctx.administrators
                           where admin._email == userAccount && admin._password == password
                           select admin).SingleOrDefault();
           
            if (result1 != null)
            {
                return true;
            }
            else
            {
                return false;
            }           
        }    
        return true;
    }

}