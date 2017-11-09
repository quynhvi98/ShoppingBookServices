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
        public void creatOrder(double _total_bill, int _customer, int _id_customer_address)
        {
            //String sql = "INSERT dbo.order_product( [_total_bill],_payment_id,_customer,_id_customer_address)VALUES  " +
            //    "( @_total_bill,1,@_customer,@_id_customer_address)";
            DataClassesDataContext ctx = new DataClassesDataContext();
            order_product op = new order_product();
            op._total_bill = _total_bill;
            op._payment_id = 1;
            op._status_paymen = "Chưa thanh toán";
            op._status_delivery = "Chưa vận chuyển";
            op._customer_id = _customer;
            op._id_customer_address = _id_customer_address;
            ctx.order_products.InsertOnSubmit(op);
            ctx.SubmitChanges();
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