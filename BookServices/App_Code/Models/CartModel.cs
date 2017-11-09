using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class CartModel : ConnectDatabase
    {
        public List<Cart> getCartByIdUser(String id)
        {
            
            DataClassesDataContext ctx = new DataClassesDataContext();
            var result = from cart in ctx.carts
                         join p in ctx.products on cart.id_product equals p._id
                         where cart.id_user == id
                         select new { cart._id,cart.id_user,cart.id_product,cart.quantity,p._IMG,p._price,p._price_pages,p._name,p._repository};

            List<Cart> list = new List<Cart>();
            foreach (var item in result)
            {
                Cart cart = new Cart();
                cart._id = item._id;

                cart.id_product = item.id_product;
                cart.quantity = item.quantity;
                cart.img = item._IMG;
                cart.price = item._price;
                cart.price_pages = double.Parse(item._price_pages.ToString());
                cart.name_product = item._name;
                cart.repository = item._repository;
                list.Add(cart);
            }
            return list;


        }

      
           
       
        public Decimal gettongtien(String id)
        {
            string sql = "SELECT SUM(dbo.product.[_price_pages]*quantity) AS 'dsd'  FROM cart JOIN dbo.product ON product.[_id] = cart.id_product WHERE id_user ='"+id+"'";
            SqlCommand md1 = new SqlCommand(sql, GetConnection());
            
            if (md1.Connection.State == ConnectionState.Closed)
            {
                md1.Connection.Open();
            }
            SqlDataReader rd1 = md1.ExecuteReader();
            if (rd1.HasRows)
            {
                while (rd1.Read())
                {
                    try
                    {
                     return rd1.GetDecimal(0);
                    }
                    catch (Exception e)
                    {

                    }
               
                }
                   
            }
            return 0;
        }

        public void updatecart(String quanti,String user,String product)
        {
            //String sql = "UPDATE dbo.cart SET quantity=@quanti WHERE id_user=@user AND id_product=@product";
            DataClassesDataContext ctx = new DataClassesDataContext();
            cart _cart = (from c in ctx.carts
                         where c.id_user == user && c.id_product == product
                         select c).SingleOrDefault();
            _cart.quantity = int.Parse(quanti);
            ctx.SubmitChanges();

        }
        public void creatAndUpdate(String user, String product)
        {
            //string sql = "SELECT * FROM dbo.cart WHERE id_user=@user AND id_product=@product";
         

            DataClassesDataContext ctx = new DataClassesDataContext();
            cart ca = (from c in ctx.carts
                      where c.id_user == user && c.id_product == product
                      select c).SingleOrDefault();

            if(ca != null)
            {
                if (ca.quantity < 30)
                {
                    ca.quantity += 1;
                }
                ctx.SubmitChanges();
            }
            else
            {
                cart cre_cart = new cart();
                cre_cart.id_user = user;
                cre_cart.id_product = product;
                cre_cart.quantity = 1;
                ctx.carts.InsertOnSubmit(cre_cart);
                ctx.SubmitChanges();
            }      
            //sql = "UPDATE dbo.cart SET quantity=quantity+ CASE WHEN quantity>=30 THEN 0 ELSE 1 END  WHERE id_user=@user AND id_product=@product";
          
        }
        public List<RefProductOrder> GetlistProductFromIdUser(String id)
        {
            //string sql = "SELECT id_product,quantity,[_price_pages] FROM  dbo.cart " +
            //    "JOIN dbo.product ON product.[_id] = cart.id_product  WHERE id_user='"+id+"'";
           
            DataClassesDataContext ctx = new DataClassesDataContext();
            var result = from c in ctx.carts
                         join p in ctx.products on c.id_product equals p._id
                         where c.id_user == id
                         select new { c.id_product,c.quantity,p._price_pages};
            List<RefProductOrder> list = new List<RefProductOrder>();
            foreach (var item in result)
            {
                RefProductOrder cart = new RefProductOrder();
                cart._id_product = item.id_product;
                cart._quantity = item.quantity;
                cart._price = double.Parse(item._price_pages.ToString());
                list.Add(cart);
            }
            return list;
        }
      
        public void Deletecart( String user)
        {
            //String sql = "DELETE FROM dbo.cart  WHERE id_user=@user";
            DataClassesDataContext ctx = new DataClassesDataContext();
            var c = from ca in ctx.carts
                      where ca.id_user == user
                      select ca;
            ctx.carts.DeleteAllOnSubmit(c);
            ctx.SubmitChanges();
        }
        public void DeletecartFromUserIdAndProductId(String user,String idProduct)
        {
            //String sql = "DELETE FROM dbo.cart WHERE id_user=@user AND id_product=@id_product";

            DataClassesDataContext ctx = new DataClassesDataContext();
            var result = from ca in ctx.carts
                         where ca.id_user == user && ca.id_product == idProduct
                         select ca;
            ctx.carts.DeleteAllOnSubmit(result);
            ctx.SubmitChanges();
        }

        public List<Cart> GetlistCartFromIdUser(String id)
        {
            //string sql = "SELECT * FROM cart WHERE id_user='"+id+ "'";
            DataClassesDataContext ctx = new DataClassesDataContext();
            var listcart = from ca in ctx.carts
                           where ca.id_user == id
                           select ca;

            List<Cart> list = new List<Cart>();
            foreach (var item in listcart)
            {
                Cart cart = new Cart();
                cart.id_user = item.id_user;
                cart.id_product = item.id_product;
                cart.quantity = item.quantity;
                list.Add(cart);
            }
            return list;
        }
       
        public void UpdateIdserFromIdCustomer(String user, String Customer)
        {
            List<Cart> IdUser = GetlistCartFromIdUser(user);
            List<Cart> IdCustomer = GetlistCartFromIdUser(Customer);


            for (int i = 0; i < IdUser.Count; i++)
            {
                int a = IdCustomer.IndexOf(IdUser[i]);
                if (a >= 0)
                {
                    updateCustomerHad(user, Customer, IdUser[i].id_product, IdUser[i].quantity);
                }
                
            }

            //String sql = "UPDATE  cart SET  id_user=@Customer WHERE  id_user=@user";
            DataClassesDataContext ctx = new DataClassesDataContext();
            var result = from ca in ctx.carts where ca.id_user == user
                     select ca;
            foreach (var item in result)
            {
                item.id_user = Customer;
            }

            ctx.SubmitChanges();

        }
        public void updateCustomerHad(String user, String Customer,String idProdcut,int quantity)
        {
            //String sql = "UPDATE  cart SET quantity=quantity+@quantity WHERE  id_user=@Customer and id_product=@idProdcut";
            DataClassesDataContext ctx = new DataClassesDataContext();
            cart c = (from ca in ctx.carts
                      where ca.id_user == Customer && ca.id_product == idProdcut
                      select ca).SingleOrDefault();
            c.quantity = c.quantity + quantity;
            ctx.SubmitChanges();
            //
            //String sql1 = "DELETE FROM cart WHERE  id_user=@user and id_product=@product";
            cart c1 = (from ca in ctx.carts
                       where ca.id_user == user && ca.id_product == idProdcut
                       select ca).SingleOrDefault();
            ctx.carts.DeleteOnSubmit(c1);
            ctx.SubmitChanges();
        }
    }
}