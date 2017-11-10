using bookstore.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

    ////BookStoreService////////////////////////////////////////////////////


    //BookModel/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    bookstore.Models.BookModel bm = new bookstore.Models.BookModel();

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
    bookstore.Models.AuthorModel am = new bookstore.Models.AuthorModel();

    [WebMethod]
    public List<bookstore.Models.Author> GetAuthors()
    {
        return am.GetAuthors();
    }

    //CartModel////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    bookstore.Models.CartModel cm = new bookstore.Models.CartModel();

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

    bookstore.Models.CategoryModel catemo = new bookstore.Models.CategoryModel();
    
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

    bookstore.Models.CustomerAddressModel cam = new bookstore.Models.CustomerAddressModel();

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
    bookstore.Models.CustomerModel cusm = new bookstore.Models.CustomerModel();

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
    bookstore.Models.NewsModel nm = new bookstore.Models.NewsModel();

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
    bookstore.Models.OrderModel om = new bookstore.Models.OrderModel();

    [WebMethod]
    public void creatOrder(double _total_bill, int _customer, int _id_customer_address)
    {
        om.creatOrder(_total_bill, _customer, _id_customer_address);
    }
    [WebMethod]
    public int GetIDOrderFromTotalBillIdCustomrAndCustomerAddress(double _total_bill, int _customer, int _id_customer_address)
    {
        return om.GetIDOrderFromTotalBillIdCustomrAndCustomerAddress(_total_bill, _customer, _id_customer_address);
    }

    //RefProductOrderModel////////////////////////////////////////////////////////////////////////////////////////////////
    bookstore.Models.RefProductOrdermodel rpo = new bookstore.Models.RefProductOrdermodel();

    [WebMethod]
    public void creatRefProductOrder(RefProductOrder refProductOrder, int idOrder)
    {
        rpo.creatRefProductOrder(refProductOrder, idOrder);
    }


    //ReviewModel//////////////////////////////////////////////////////////////////////////////////////////////////////////
    bookstore.Models.ReviewModel rm = new bookstore.Models.ReviewModel();

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
    public int GetCus(string name)
    {
        return rm.GetCus(name);
    }

    //////////BookManagerService////////////////////////////

    AuthorModel au_manager = new AuthorModel();

    [WebMethod]
    public List<String> GetlistAuthor()
    {
        return au_manager.GetlistAuthor();
    }

    [WebMethod]
    public int GetIdProductTypeByName(String Name)
    {
        return au_manager.GetIdProductTypeByName(Name);
    }

    [WebMethod]
    public bool AddAuthors(Author author)
    {
        return au_manager.AddAuthors(author);
    }

    [WebMethod]
    public bool HasIdAuthor(Author author)
    {
        return au_manager.HasIdAuthor(author);
    }

    [WebMethod]
    public bool UpdateAuthor(Author author)
    {
        return au_manager.UpdateAuthor(author);
    }

    [WebMethod]
    public DataTable SearchAuthor(string query, int type)
    {
        return au_manager.SearchAuthor(query, type);
    }

    //CategoryModel/////////////////////////
    CategoryModel cate_manager = new CategoryModel();

    [WebMethod]
    public String GetIdProductTypeByName_CateModel(String Name)
    {
        return cate_manager.GetIdProductTypeByName(Name);
    }

    //CategoryProductModel///////////////////////////
    CategoryProductModel catepm_manager = new CategoryProductModel();
    
    [WebMethod]
    public Boolean AddCategoryProduct(CategoryProduct categoryProduct)
    {
        return catepm_manager.AddCategoryProduct(categoryProduct);
    }

    [WebMethod]
    public void AddListCategoryProduct(List<String> list, String productId)
    {
        catepm_manager.AddListCategoryProduct(list, productId);
    }

    [WebMethod]
    public List<String> GetListCategoryProductID(String productId)
    {
        return catepm_manager.GetListCategoryProductID(productId);
    }

    [WebMethod]
    public void delCategoryProductID(String productId)
    {
        catepm_manager.delCategoryProductID(productId);
    }

    //CustomerModel////////////////////////
    CustomerModel cm_manager = new CustomerModel();

    [WebMethod]
    public bool UpdateCustomer(Customer customer)
    {
        return cm_manager.UpdateCustomer(customer);
    }

    [WebMethod]
    public DataTable SearchCustomer(string query, int type)
    {
        return cm_manager.SearchCustomer(query, type);
    }

    //ProducerModel//////////////////////////////
    ProducerModel pm_manager = new ProducerModel();

    [WebMethod]
    public List<String> GetlistProducer()
    {
        return pm_manager.GetlistProducer();
    }

    [WebMethod]
    public int GetIdProducerByName(String Name)
    {
        return pm_manager.GetIdProducerByName(Name);
    }

    [WebMethod]
    public bool AddProducer(Producer producer)
    {
        return pm_manager.AddProducer(producer);
    }

    [WebMethod]
    public bool HasIdProducer(Producer producer)
    {
        return pm_manager.HasIdProducer(producer);
    }

    [WebMethod]
    public bool UpdateProducer(Producer producer)
    {
        return pm_manager.UpdateProducer(producer);
    }

    [WebMethod]
    public DataTable SearchProducer(string query, int type)
    {
        return pm_manager.SearchProducer(query, type);
    }

    //DataProcess/////////////////////////////
    DataProcess dp = new DataProcess();

    [WebMethod]
    public DataTable GetListOrder()
    {
        return dp.GetListOrder();
    }

    [WebMethod]
    public bool DeleteOrder(string id)
    {
        return dp.DeleteOrder(id);
    }

    [WebMethod]
    public bool UpdateOrderProduct(string id, string payment_type, string status_payment, string status_delivery)
    {
        return dp.UpdateOrderProduct(id, payment_type,status_payment,status_delivery);
    }

    [WebMethod]
    public DataTable OrderDetailsByID(string id)
    {
        return dp.OrderDetailsByID(id);
    }

    [WebMethod]
    public List<string> GetInfoCustomer_Order(string id)
    {
        return dp.GetInfoCustomer_Order(id);
    }

    [WebMethod]
    public DataTable GetSortData(string sort_type)
    {
        return dp.GetSortData(sort_type);
    }

    [WebMethod]
    public DataTable SearchOrder(string query, int type)
    {
        return dp.SearchOrder(query,type);
    }

    [WebMethod]
    public DataTable GetCustomerInformation()
    {
        return dp.GetCustomerInformation();
    }

    [WebMethod]
    public DataTable GetProducerInformation()
    {
        return dp.GetProducerInformation();
    }

    [WebMethod]
    public DataTable GetAuthorInformation()
    {
        return dp.GetAuthorInformation();
    }

    [WebMethod]
    public bool LoginWithAccAndPass_DataProcess(String userAccount, String password)
    {
        return dp.LoginWithAccAndPass(userAccount, password);
    }

    [WebMethod]
    public List<string> DataTableToList(DataTable dt)
    {
        return dp.DataTableToList(dt);
    }
    //ProductModel//////////////////
    ProductModel product_manager = new ProductModel();

    [WebMethod]
    public Boolean AddProduct(Product product)
    {
        return product_manager.AddProduct(product);
    }

    [WebMethod]
    public DataTable getListProduct()
    {
        return product_manager.getListProduct();
    }

    [WebMethod]
    public DataTable getListProductToEdit(String id)
    {
        return product_manager.getListProductToEdit(id);
    }

    [WebMethod]
    public bool updateProduct(Product product, String IDCu)
    {
        return product_manager.updateProduct(product, IDCu);
    }

    [WebMethod]
    public DataTable getListTongDoanhThuBanTheoSanPham()
    {
        return product_manager.getListTongDoanhThuBanTheoSanPham();
    }

    [WebMethod]
    public DataTable getListTongDoanhThuBanTheoType()
    {
        return product_manager.getListTongDoanhThuBanTheoType();
    }

    [WebMethod]
    public DataTable getListTongDoanhThuBanTheoThangNayVoiType()
    {
        return product_manager.getListTongDoanhThuBanTheoThangNayVoiType();
    }

    [WebMethod]
    public DataTable getSoLuongCoTrongKhoHang()
    {
        return product_manager.getSoLuongCoTrongKhoHang();
    }

    [WebMethod]
    public DataTable SearchProduct(string query, int type)
    {
        return product_manager.SearchProduct(query,type);
    }

    //ProductType/////////////////
    ProductTypeModel pt_manager = new ProductTypeModel();

    [WebMethod]
    public List<String> GetlistProductType()
    {
        return pt_manager.GetlistProductType();
    }

    [WebMethod]
    public int GetIdProductTypeByName_ProductType(String Name)
    {
        return pt_manager.GetIdProductTypeByName(Name);
    }

    [WebMethod]
    public List<ProductType> GetlistIDProductType()
    {
        return pt_manager.GetlistIDProductType();
    }
    
    

}