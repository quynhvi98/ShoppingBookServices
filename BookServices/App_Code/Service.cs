using bookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class Service : System.Web.Services.WebService
{
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    //BookModel/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    BookModel bm = new BookModel();

    [WebMethod]
    public List<Book> GetBooksForSlider()
    {
        return bm.GetBooksForSlider();
    }
    [WebMethod]
    public List<Book> GetBooks(int query1, string query2)
    {
        return bm.GetBooks(query1,query2);
    }
    [WebMethod]
    public double GetRating(string id_book)
    {
        return bm.GetRating(id_book);
    }
    [WebMethod]
    public Book GetBookByID(string id)
    {
        return bm.GetBookByID(id);
    }
    [WebMethod]
    public List<Book> searchByName(string query)
    {
        return bm.searchByName(query);
    }

    //AuthorModel///////////////////////////////////////////////////////////////////////////////////////////////////////////////
    AuthorModel am = new AuthorModel();

    [WebMethod]
    public List<Author> GetAuthors()
    {
        return am.GetAuthors();
    }

    //CartModel////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    CartModel cm = new CartModel();

    [WebMethod]
    public List<Cart> getCartByIdUser(string id)
    {
        return cm.getCartByIdUser(id);
    }
    [WebMethod]
    public Decimal gettongtien(String id)
    {
        return cm.gettongtien(id);
    }
    [WebMethod]
    public void updatecart(String quanti, String user, String product)
    {
        cm.updatecart(quanti, user, product);
    }
    [WebMethod]
    public void creatAndUpdate(String user, String product)
    {
        cm.creatAndUpdate(user, product);
    }
    [WebMethod]
    public List<RefProductOrder> GetlistProductFromIdUser(String id)
    {
        return cm.GetlistProductFromIdUser(id);
    }
    [WebMethod]
    public void Deletecart(String user)
    {
        cm.Deletecart(user);
    }
    [WebMethod]
    public void DeletecartFromUserIdAndProductId(String user, String idProduct)
    {
        cm.DeletecartFromUserIdAndProductId(user, idProduct);
    }
    [WebMethod]
    public List<Cart> GetlistCartFromIdUser(String id)
    {
        return cm.GetlistCartFromIdUser(id);
    }
    [WebMethod]
    public void UpdateIdserFromIdCustomer(String user, String Customer)
    {
        cm.UpdateIdserFromIdCustomer(user, Customer);
    }
    [WebMethod]
    public void updateCustomerHad(String user, String Customer, String idProdcut, int quantity)
    {
        cm.updateCustomerHad(user, Customer, idProdcut, quantity);
    }

    //CategoryModel////////////////////////////////////////////////////////////////////////////////////////////////////////////

    CategoryModel catemo = new CategoryModel();
    
    [WebMethod]
    public List<Book> GetBookByCategory(string name_type)
    {
        return catemo.GetBookByCategory(name_type);
    }

    //ContactModel////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ContactModel conm = new ContactModel();

    [WebMethod]
    public bool SendMail_Contact(string hoten, string email, string sdt, string tieude, string noidung)
    {
        return conm.SendMail_Contact(hoten, email, sdt, tieude, noidung);
    }

    //CustomerAddressModel//////////////////////////////////////////////////////////////////////////////////////////////////////

    CustomerAddressModel cam = new CustomerAddressModel();

    [WebMethod]
    public void creatCustomerAddress(String _name, String _adddress_full, String _phone, String _city, String _district, int _id_customer)
    {
        cam.creatCustomerAddress(_name, _adddress_full, _phone, _city, _district, _id_customer);
    }
    [WebMethod]
    public int GetIDCustomerAddressrUniqueByIdCustomer(int id)
    {
        return cam.GetIDCustomerAddressrUniqueByIdCustomer(id);
    }
    [WebMethod]
    public void creatCustomerAddressHaveEmail(String email, String name, String _adddress_full, String _phone, String _city, String _district, int _id_customer)
    {
        cam.creatCustomerAddressHaveEmail(email, name, _adddress_full, _phone, _city, _district, _id_customer);
    }
    [WebMethod]
    public int GetIDCustomerAddressrTop1UniqueByIdCustomer(int id)
    {
        return cam.GetIDCustomerAddressrTop1UniqueByIdCustomer(id);
    }
    [WebMethod]
    public List<CustomerAddress> GetListAddressCustomerByCustomerId(int id)
    {
        return cam.GetListAddressCustomerByCustomerId(id);
    }
    [WebMethod]
    public bool MailToCustomer(string email)
    {
        return cam.MailToCustomer(email);
    }

    
    //CustomerModel///////////////////////////////////////////////////////////////////////////////////////////////////////////////
    CustomerModel cusm = new CustomerModel();

    [WebMethod]
    public bool LoginWithAccAndPass(String userAccount, String password)
    {
        return cusm.LoginWithAccAndPass(userAccount, password);
    }
    [WebMethod]
    public void creatCustomer(String email, String user, String name)
    {
        cusm.creatCustomer(email, user, name);
    }
    [WebMethod]
    public int GetIDCustomerByEmail(String email)
    {
        return cusm.GetIDCustomerByEmail(email);
    }
    [WebMethod]
    public String GetuserByEmail(String email)
    {
        return cusm.GetuserByEmail(email);
    }
    [WebMethod]
    public int GetIDCustomerByUser(String User)
    {
        return cusm.GetIDCustomerByUser(User);
    }
    [WebMethod]
    public void Signin(String email, String user, String password, String name)
    {
        cusm.Signin(email, user, password, name);
    }


    //NewModel///////////////////////////////////////////////////////////////////////////////////////////////////////////
    NewsModel nm = new NewsModel();

    [WebMethod]
    public List<News> GetListNews(int type)
    {
        return nm.GetListNews(type);
    }
    [WebMethod]
    public News GetNewsByID(string id_news)
    {
        return nm.GetNewsByID(id_news);
    }
    
    //OrderModel/////////////////////////////////////////////////////////////////////////////////////////////////////////
    OrderModel om = new OrderModel();

    [WebMethod]
    public void creatOrder(Decimal _total_bill, int _customer, int _id_customer_address)
    {
        om.creatOrder(_total_bill, _customer, _id_customer_address);
    }
    [WebMethod]
    public int GetIDOrderFromTotalBillIdCustomrAndCustomerAddress(double _total_bill, int _customer, int _id_customer_address)
    {
        return om.GetIDOrderFromTotalBillIdCustomrAndCustomerAddress(_total_bill, _customer, _id_customer_address);
    }

    //RefProductOrderModel////////////////////////////////////////////////////////////////////////////////////////////////
    RefProductOrdermodel rpo = new RefProductOrdermodel();

    [WebMethod]
    public void creatRefProductOrder(RefProductOrder refProductOrder, int idOrder)
    {
        rpo.creatRefProductOrder(refProductOrder, idOrder);
    }


    //ReviewModel//////////////////////////////////////////////////////////////////////////////////////////////////////////
    ReviewModel rm = new ReviewModel();

    [WebMethod]
    public List<Review> GetReviews(string id)
    {
        return rm.GetReviews(id);
    }

    [WebMethod]
    public void Comment_Book(string id, int rate, string comment, string name)
    {
        rm.Comment_Book(id, rate, comment, name);
    }
    [WebMethod]
    public string GetCus(string name)
    {
        return rm.GetCus(name);
    }
}