using Model.EnityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Func
{
    public class OrderFunc
    {
        FoodShopOnlineDBContext db = null;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-37MO0VM\SQLEXPRESS;" + "Initial Catalog=FoodShopOnline;" + "Integrated Security=True");
        public OrderFunc()
        {
            db = new FoodShopOnlineDBContext();
        }
        //hàm order sản phẩm vào database sử dụng Stored Procedure
        public void Insert(Order order)
        {
            //db.Orders.Add(order);
            //db.SaveChanges();
            //return order.ID;
            SqlCommand cmd = new SqlCommand("SP_Order_Insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("CustomerID", order.CustomerID);
            cmd.Parameters.AddWithValue("CustomerName", order.CustomerName);
            cmd.Parameters.AddWithValue("CustomerAddress", order.CustomerAddress);
            cmd.Parameters.AddWithValue("CustomerEmail", order.CustomerEmail);
            cmd.Parameters.AddWithValue("CustomerMobile", order.CustomerMobile);
            cmd.Parameters.AddWithValue("CreatedDate", order.CreatedDate);
            cmd.Parameters.AddWithValue("CreatedBy", order.CreatedBy);
            cmd.Parameters.AddWithValue("PaymentMethod", order.PaymentMethod);
            cmd.Parameters.AddWithValue("PaymentStatus", order.PaymentStatus);
            cmd.Parameters.AddWithValue("Status", order.Status);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //hàm hiển thị thông tin người mua sử dụng Stored Procedure
        public List<Order> ListOrder()
        {
            var list = db.Database.SqlQuery<Order>("SP_Order_ListAll").ToList();
            return list;
        }
    }
}
