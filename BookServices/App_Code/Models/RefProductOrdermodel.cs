using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class RefProductOrdermodel :ConnectDatabase
    {
        public void creatRefProductOrder(RefProductOrder refProductOrder,int idOrder)
        {
            //String sql = "INSERT dbo.ref_product_order( [_quantity] ,[_id_order] , [_id_product] ,[_price] )VALUES " +
            //    " ( @_quantity , @_id_order , @_id_product , @_price )";

            DataClassesDataContext ctx = new DataClassesDataContext();
            ref_product_order rpo = new ref_product_order();
            rpo._quantity = refProductOrder._quantity;
            rpo._id_order = idOrder;
            rpo._id_product = refProductOrder._id_product;
            rpo._price = refProductOrder._price;
            ctx.ref_product_orders.InsertOnSubmit(rpo);
            ctx.SubmitChanges();
        }
    }
}