using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class OrderModel : ConnectDatabase
    {
        public void creatOrder(Decimal _total_bill, int _customer, int _id_customer_address)
        {
            String sql = "INSERT dbo.order_product( [_total_bill],_payment_id,_customer,_id_customer_address)VALUES  " +
                "( @_total_bill,1,@_customer,@_id_customer_address)";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.AddWithValue("@_total_bill", _total_bill);
            cmd.Parameters.AddWithValue("@_customer", _customer);
            cmd.Parameters.AddWithValue("@_id_customer_address", _id_customer_address);
            int reuslt = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public int GetIDOrderFromTotalBillIdCustomrAndCustomerAddress(double _total_bill, int _customer, int _id_customer_address)
        {
            try
            {
              

                DataClassesDataContext ctx = new DataClassesDataContext();
                var result = (from op in ctx.order_products
                              where op._total_bill == _total_bill
                              && op._customer_id == _customer
                              && op._id_customer_address == _id_customer_address
                              select op._id).SingleOrDefault();


                return result;

            }
            catch (Exception e)
            {
                return -1;
            }
            
        }
    }
}