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
    public class ProductFunc
    {
        FoodShopOnlineDBContext db = null;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-37MO0VM\SQLEXPRESS;" + "Initial Catalog=FoodShopOnline;" + "Integrated Security=True");
        public ProductFunc()
        {
            db = new FoodShopOnlineDBContext();
        }
        //CÁC HÀM THỰC HIỆN THAO TÁC THÊM, XÓA, SỬA CỦA TRANG SẢN PHẨM
        //hiển thị dữ liệu sản phẩm
        public List<Product> ListAllPro()
        {
            var list = db.Database.SqlQuery<Product>("Sp_Product").ToList();
            return list;
        }
        //hàm thêm mới sản phẩm sử dụng Stored Procedure
        public int Create(string name, string alias, int categoryID, string image, decimal price, decimal? promotion,
            string description, DateTime? createdDate, string CreatedBy, bool status)
        {
            SqlCommand cmd = new SqlCommand("SP_Product_Insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Name", name);
            cmd.Parameters.AddWithValue("Alias", alias);
            cmd.Parameters.AddWithValue("CategoryID", categoryID);
            cmd.Parameters.AddWithValue("Image", image);
            cmd.Parameters.AddWithValue("Price", price);
            cmd.Parameters.AddWithValue("Promotion", promotion);
            cmd.Parameters.AddWithValue("Description", description);
            cmd.Parameters.AddWithValue("CreatedDate", createdDate);
            cmd.Parameters.AddWithValue("CreateBy", CreatedBy);
            cmd.Parameters.AddWithValue("Status", status);
            con.Open();
            int k = cmd.ExecuteNonQuery();
            con.Close();
            return k;
        }
        //hảm update sản phẩm sử dụng Stored Procedure
        public bool Update(Product entity)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Sp_Product_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("ID", entity.ID);
                cmd.Parameters.AddWithValue("Name", entity.Name);
                cmd.Parameters.AddWithValue("Alias", entity.Alias);
                cmd.Parameters.AddWithValue("CategoryID", entity.CategoryID);
                cmd.Parameters.AddWithValue("Image", entity.Image);
                cmd.Parameters.AddWithValue("Price", entity.Price);
                cmd.Parameters.AddWithValue("Promotion", entity.Promotion);
                cmd.Parameters.AddWithValue("Description", entity.Description);
                cmd.Parameters.AddWithValue("CreatedDate", entity.CreatedDate);
                cmd.Parameters.AddWithValue("CreateBy", entity.CreatedBy);
                cmd.Parameters.AddWithValue("Status", entity.Status);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
 
        }
        //hàm lấy chi tiết bản ghi thông tin sản phẩm
        public Product ViewDetailProduct(int id)
        {
            //return db.Products.Find(id);
            var idParameter = new SqlParameter
            {
                ParameterName = "ID",
                Value = id
            };
            return db.Database.SqlQuery<Product>("EXEC SP_ViewDetailProduct @ID", idParameter).SingleOrDefault();
        }
        //hàm xóa thông tin một sản phẩm sử dụng Stored Procedure
        public int DeleteProduct(int id)
        {
            SqlCommand cmd = new SqlCommand("Sp_Product_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ID", id);
            con.Open();
            int k = cmd.ExecuteNonQuery();
            con.Close();
            return k;
        }
    }
}
