using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductModel
/// </summary>
public class ProductModel : DataProcess
{
    public ProductModel()
    {
    }
    public Boolean AddProduct(Product product)
    {
        //String sql = " INSERT dbo.product ( " +
        //    "[_id] ,[_name] , [_IMG] ,dbo.product.[_price_pages],[_price] ,[_pages],[_repository] ,[_weight] ,[_content] ,[_status] ,[_date] , [_year_of_creation] ,[_id_producer] ,[_type] ,[_author_id]) VALUES " +
        //    " (@id, @name, @IMG, @_price_pages, @price ,  @pages ,@repository, @weight ,@content , @status , GETDATE() , @yearOfCreation ,@_id_producer , @type,@author)";
        DataClassesDataContext ctx = new DataClassesDataContext();
        try
        {
            product p = new global::product()
            {
                _id = product.id,
                _name = product.name,
                _IMG = product.IMG,
                _price_pages = product.price_pages,
                _price = double.Parse(product.price.ToString()),
                _pages = product.pages,
                _repository = product.repository,
                _weight = product.weight,
                _content = product.content,
                _status = product.status,
                _year_of_creation = int.Parse(product.year_of_creation),
                _id_producer = product.idProducer,
                _type = product.type,
                _author_id = product.author
            };
            ctx.products.InsertOnSubmit(p);
            ctx.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }


    }
    public List<Product> getListProduct()
    {
        //string sql = "SELECT product._id,product._name,_price,_status,producer._name as" +
        //    " 'NhaXuatBan',product_type._name as productType,_name_author,_repository" +
        //    " FROM BookASMWAD.dbo.product JOIN BookASMWAD.dbo.producer on product._id_producer = producer._id JOIN" +
        //    " BookASMWAD.dbo.product_type on product._type = product_type._id JOIN" +
        //    " BookASMWAD.dbo.author on product._author_id = author._id";
        DataClassesDataContext ctx = new DataClassesDataContext();
        var result = from p in ctx.products
                     join pd in ctx.producers on p._id_producer equals pd._id
                     join pt in ctx.product_types on p._type equals pt._id
                     join au in ctx.authors on p._author_id equals au._id
                     select new { p._id, productname = p._name, p._status, p._price, p.producer, producername = pd._name, producttypename = pt._name, au._name_author, p._repository };
        List<Product> listProduct = new List<Product>();
        foreach (var item in result)
        {
            Product product = new Product()
            {
                id = item._id,
                name = item.productname,
                price = Decimal.Parse(item._price.ToString()),
                status = item._status,
                producer = item.producername,
                TypeName = item.producttypename,
                AuthorName = item._name_author,
                repository = item._repository,
            };

            listProduct.Add(product);
        }

        return listProduct;
    }
    public Product getListProductToEdit(String id)
    {
        //string sql = "SELECT product._id,product._name,dbo.product.[_IMG],_price,_pages,_weight,_content," +
        //    "_status,_year_of_creation, producer._name as 'NhaXuatBan',product_type._name as productType,_name_author," +
        //    "_repository FROM BookASMWAD.dbo.product JOIN BookASMWAD.dbo.producer on" +
        //    " product._id_producer = producer._id JOIN BookASMWAD.dbo.product_type on product._type = product_type._id " +
        //    "JOIN BookASMWAD.dbo.author on product._author_id = author._id WHERE product._id='" + id + "'";
        DataClassesDataContext ctx = new DataClassesDataContext();
        var result = (from p in ctx.products
                      join pc in ctx.producers on p._id_producer equals pc._id
                      join pt in ctx.product_types on p._type equals pt._id
                      join au in ctx.authors on p._author_id equals au._id
                      where p._id == id
                      select new { p._id, productname = p._name, p._IMG, p._price, p._pages, p._weight, p._content, p._status, p._year_of_creation, producername = pc._name, producttypename = pt._name, au._name_author, p._repository }).SingleOrDefault();
        Product product = new Product()
        {
            id = result._id,
            name = result.producername,
            IMG = result._IMG,
            price = Decimal.Parse(result._price.ToString()),
            pages = result._pages,
            weight = result._weight,
            content = result._content,
            status = result._status,
            year_of_creation = result._year_of_creation.ToString(),
            producer = result.producername,
            TypeName = result.producttypename,
            AuthorName = result._name_author,
            repository = result._repository


        };
        return product;
    }
    public bool updateProduct(Product product, String IDCu)
    {
        try
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            var result = (from p in ctx.products
                          where p._id == IDCu
                          select p).SingleOrDefault();
            result._name = product.name;
            result._IMG = product.IMG;
            result._price = double.Parse(product.price.ToString());
            result._pages = product.pages;
            result._repository = product.repository;
            result._weight = product.weight;
            result._content = product.content;
            result._status = product.status;
            result._id_producer = product.idProducer;
            result._type = product.type;
            result._author_id = product.author;
            ctx.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
        //String sql = "UPDATE BookASMWAD.dbo.product SET " +
        //    "_id=@id ," +
        //    "_name=@name," +
        //     "_IMG=@IMG," +
        //    "_price=@price," +
        //    "_pages=@page," +
        //    "_repository=@repository," +
        //    "_weight=@weight," +
        //    "_content=@content," +
        //    "_status=@status," +
        //    "_id_producer=@procucer" +
        //    ",_type=@type" +
        //    ",_author_id=@author " +
        //    "WHERE _id=@idCu";
       

    }
    public DataTable getListTongDoanhThuBanTheoSanPham()
    {
        string sql = "SELECT SUM(dbo.ref_product_order.[_quantity] * dbo.ref_product_order.[_price]) AS 'tongtien',SUM(dbo.ref_product_order.[_quantity]) AS 'soluongban',dbo.product.[_name],[_id_product] FROM dbo.ref_product_order JOIN dbo.product ON product.[_id] = ref_product_order.[_id_product] GROUP BY [_id_product],dbo.product.[_name]";
        SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection());
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    public DataTable getListTongDoanhThuBanTheoType()
    {
        string sql = "SELECT SUM(dbo.ref_product_order.[_price] * [_quantity]) AS 'tongdoanhthu',dbo.product_type.[_name] FROM dbo.ref_product_order JOIN dbo.product ON product.[_id] = ref_product_order.[_id_product] JOIN dbo.product_type ON product_type.[_id] = product.[_type] GROUP BY dbo.product.[_type],dbo.product_type.[_name]";
        SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection());
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    public DataTable getListTongDoanhThuBanTheoThangNayVoiType()
    {
        string sql = "SELECT SUM(dbo.ref_product_order.[_price] * [_quantity]) AS 'tongdoanhthu',dbo.product_type.[_name], DATEPART(MM,GETDATE()) AS 'thang' FROM dbo.ref_product_order JOIN dbo.product ON product.[_id] = ref_product_order.[_id_product] JOIN dbo.product_type ON product_type.[_id] = product.[_type] JOIN dbo.order_product ON order_product.[_id] = ref_product_order.[_id_order] WHERE  DATEPART(MM,order_product.[_date]) = DATEPART(MM,getdate()) GROUP BY dbo.product.[_type],dbo.product_type.[_name]";
        SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection());
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    public DataTable getSoLuongCoTrongKhoHang()
    {
        string sql = "SELECT N'Tổng Số Lượng ' AS 'name' ,SUM(_repository) AS'tongsoluong'   FROM dbo.product ";
        SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection());
        DataTable dt = new DataTable();
        da.Fill(dt);
        ProductTypeModel productTypeModel = new ProductTypeModel();
        List<ProductType> list = productTypeModel.GetlistIDProductType();
        for (int i = 0; i < list.Count; i++)
        {
            sql = "SELECT N'" + list[i].name + "' AS 'name' ,SUM(_repository) AS'tongsoluong'   FROM dbo.product WHERE [_type]=" + list[i].id + " GROUP BY [_type]";
            da = new SqlDataAdapter(sql, GetConnection());
            da.Fill(dt);
        }
        return dt;
    }
    public List<Product> SearchProduct(string query, int type)
    {
        DataClassesDataContext ctx = new DataClassesDataContext();
        //string sql = "";
        List<Product> list = new List<Product>();
        if (type == 1)
        {
            //sql = "SELECT product._id,product._name,_price,_status,producer._name as 'NhaXuatBan',product_type._name " +
            //    "as productType,_name_author,_repository" +
            //" FROM BookASMWAD.dbo.product JOIN BookASMWAD.dbo.producer on product._id_producer = producer._id " +
            //"JOIN BookASMWAD.dbo.product_type on product._type = product_type._id " +
            //"JOIN BookASMWAD.dbo.author on product._author_id = author._id WHERE dbo.product.[_id]='" + query + "'";
            var result = from p in ctx.products
                         join pc in ctx.producers on p._id_producer equals pc._id
                         join pt in ctx.product_types on p._type equals pt._id
                         join au in ctx.authors on p._author_id equals au._id
                         where p._id == query
                         select new { p._id, productname = p._name, p._price, p._status, producername = pc._name, producttypename = pt._name, au._name_author, p._repository };
            foreach (var item in result)
            {
                Product product = new Product()
                {
                    id = item._id,
                    name = item.productname,
                    price = Decimal.Parse(item._price.ToString()),
                    status = item._status,
                    producer = item.producername,
                    TypeName = item.producttypename,
                    AuthorName = item._name_author,
                    repository = item._repository,
                };
                list.Add(product);
            }
        }
        if (type == 2)
        {
            //sql = "SELECT product._id,product._name,_price,_status,producer._name as 'NhaXuatBan',product_type._name " +
            //    "as productType,_name_author,_repository" +
            //" FROM BookASMWAD.dbo.product JOIN BookASMWAD.dbo.producer on product._id_producer = producer._id " +
            //"JOIN BookASMWAD.dbo.product_type on product._type = product_type._id " +
            //"JOIN BookASMWAD.dbo.author on product._author_id = author._id WHERE dbo.product.[_name] LIKE N'%" + query + "%'";
            var result = from p in ctx.products
                         join pc in ctx.producers on p._id_producer equals pc._id
                         join pt in ctx.product_types on p._type equals pt._id
                         join au in ctx.authors on p._author_id equals au._id
                         where p._name.Contains(@query)
                         select new { p._id, productname = p._name, p._price, p._status, producername = pc._name, producttypename = pt._name, au._name_author, p._repository };
            foreach (var item in result)
            {
                Product product = new Product()
                {
                    id = item._id,
                    name = item.productname,
                    price = Decimal.Parse(item._price.ToString()),
                    status = item._status,
                    producer = item.producername,
                    TypeName = item.producttypename,
                    AuthorName = item._name_author,
                    repository = item._repository,
                };
                list.Add(product);
            }
        }
        if (type == 3)
        {
            //sql = "SELECT product._id,product._name,_price,_status,producer._name as 'NhaXuatBan',product_type._name as productType,_name_author,_repository" +
            //" FROM BookASMWAD.dbo.product JOIN BookASMWAD.dbo.producer on product._id_producer = producer._id JOIN BookASMWAD.dbo.product_type on product._type = product_type._id JOIN BookASMWAD.dbo.author on product._author_id = author._id WHERE dbo.product.[_price] = '" + query + "'";
            var result = from p in ctx.products
                         join pc in ctx.producers on p._id_producer equals pc._id
                         join pt in ctx.product_types on p._type equals pt._id
                         join au in ctx.authors on p._author_id equals au._id
                         where p._price == double.Parse(query)
                         select new { p._id, productname = p._name, p._price, p._status, producername = pc._name, producttypename = pt._name, au._name_author, p._repository };
            foreach (var item in result)
            {
                Product product = new Product()
                {
                    id = item._id,
                    name = item.productname,
                    price = Decimal.Parse(item._price.ToString()),
                    status = item._status,
                    producer = item.producername,
                    TypeName = item.producttypename,
                    AuthorName = item._name_author,
                    repository = item._repository,
                };
                list.Add(product);
            }
        }

        return list;
    }
}