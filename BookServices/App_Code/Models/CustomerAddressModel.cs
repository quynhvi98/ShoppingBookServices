using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace bookstore.Models
{
    public class CustomerAddressModel : ConnectDatabase
    {
        public void creatCustomerAddress(String _name, String _adddress_full, String _phone, String _city, String _district, int _id_customer)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            customer_address cus_add = new customer_address();
            cus_add._name = _name;
            cus_add._adddress_full = _adddress_full;
            cus_add._phone = _phone;
            cus_add._city = _city;
            cus_add._district = _district;
            cus_add._id_customer = _id_customer;
            ctx.customer_addresses.InsertOnSubmit(cus_add);
            ctx.SubmitChanges();

           
        }
        public int GetIDCustomerAddressrUniqueByIdCustomer(int id)
        {
            try
            {
               

                DataClassesDataContext ctx = new DataClassesDataContext();
                var result = (from cus_add in ctx.customer_addresses
                              where cus_add._id_customer == id
                              select cus_add._id).SingleOrDefault();

                
                    return result;
                
            }
            catch (Exception e)
            {
                return -1;
            }
            
        }
        public void creatCustomerAddressHaveEmail(String email, String name, String _adddress_full, String _phone, String _city, String _district, int _id_customer)
        {
            //String sql = "INSERT dbo.customer_address ( _email,_name,[_adddress_full] , [_phone] ,[_city] ,[_district] , [_id_customer]) VALUES  (@_email,@_name, @_adddress_full, @_phone,@_city,@_district ,@_id_customer )";        
            DataClassesDataContext ctx = new DataClassesDataContext();
            customer_address cus_add = new customer_address();
            cus_add._email = email;
            cus_add._name = name;
            cus_add._adddress_full = _adddress_full;
            cus_add._phone = _phone;
            cus_add._city = _city;
            cus_add._district = _district;
            cus_add._id_customer = _id_customer;
            ctx.customer_addresses.InsertOnSubmit(cus_add);
            ctx.SubmitChanges();
        }
        public int GetIDCustomerAddressrTop1UniqueByIdCustomer(int id)
        {
            try
            {
               
                DataClassesDataContext ctx = new DataClassesDataContext();
                var result = (from cus_add in ctx.customer_addresses
                              where cus_add._id_customer == id
                              orderby cus_add._id descending
                              select cus_add._id).Take(1).SingleOrDefault();
              
                return result;      
            }
            catch (Exception e)
            {
                return -1;
            }

        }
        public List<CustomerAddress> GetListAddressCustomerByCustomerId(int id)
        {
            List<CustomerAddress> list = new List<CustomerAddress>();
           
            DataClassesDataContext ctx = new DataClassesDataContext();
            var result = (from cus_add in ctx.customer_addresses
                          where cus_add._id_customer == id
                          select cus_add).Take(5);
            foreach (var item in result)
            {
                CustomerAddress customerAddress = new CustomerAddress();

                customerAddress.id = item._id;
                try
                {
                    customerAddress._adddress_full = item._adddress_full;
                }
                catch (Exception)
                {

                    customerAddress._adddress_full = null;
                }
                try
                {
                    customerAddress._email = item._email;
                }
                catch (Exception)
                {

                    customerAddress._email = null;
                }

                try
                {
                    customerAddress._phone = item._phone;
                }
                catch (Exception)
                {

                    customerAddress._phone = null;
                }

                try
                {
                    customerAddress._company = item._company;

                }
                catch (Exception)
                {

                    customerAddress._company = null;
                }
                try
                {
                    customerAddress._zipe_code = item._zipe_code;
                }
                catch (Exception)
                {

                    customerAddress._zipe_code = null;
                }
                try
                {
                    customerAddress._nation = item._nation;
                }
                catch (Exception)
                {

                    customerAddress._nation = null;
                }
                try
                {
                    customerAddress._city = item._city;
                }
                catch (Exception)
                {

                    customerAddress._city = null;
                }
                try
                {
                    customerAddress._district = item._district;
                }
                catch (Exception)
                {

                    customerAddress._district = null;
                }
                customerAddress._id_customer = int.Parse(item._id_customer.ToString());

                try
                {
                    customerAddress._name = item._name;
                }
                catch (Exception)
                {

                    customerAddress._name = null;
                }
                list.Add(customerAddress);

            }

            return list;
        }
        public bool MailToCustomer(string email)
        {
            try
            {
                StringBuilder Body = new StringBuilder();
                Body.Append("<p>Chúc mừng bạn đã đặt hàng thành công, nhân viên của chúng tôi sẽ sớm gọi điện lại cho bạn để xác nhận.</p>");     
                MailMessage mail = new MailMessage();
                mail.To.Add(email);//đổi mail của mình để test
                mail.From = new MailAddress("projectsem2aptech@gmail.com");
                mail.Subject = "Edubook.com.vn";
                mail.Body = Body.ToString();
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential("projectsem2aptech@gmail.com", "Chicanem1");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}