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
    public class OrderDetailFunc
    {
        FoodShopOnlineDBContext db = null;
        SqlConnection con = new SqlConnection(@"Data Source=desktop-3rfp42t\sqlexpress;" + "Initial Catalog=FoodShopOnline;" + "Integrated Security=True");
        public OrderDetailFunc()
        {
            db = new FoodShopOnlineDBContext();
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                //db.OrderDetails.Add(detail);
                //db.SaveChanges();
                SqlCommand cmd = new SqlCommand("SP_OrderDetail_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("OrderID", detail.OrderID);
                cmd.Parameters.AddWithValue("ProductID", detail.ProductID);
                cmd.Parameters.AddWithValue("Price", detail.Price);
                cmd.Parameters.AddWithValue("Quantity", detail.Quantity);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //hàm hiển thị chi tiết đơn hàng sử dụng Stored Procedure
        public List<OrderDetail> ListOrderDetail()
        {
            var list = db.Database.SqlQuery<OrderDetail>("SP_OrderDetail_ListAll").ToList();
            return list;
        }
    }
}
