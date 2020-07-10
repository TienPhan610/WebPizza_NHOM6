using Model.EnityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Func
{
    public class ProductCategoryFunc
    {
        FoodShopOnlineDBContext db = null;
        SqlConnection con = new SqlConnection(@"Data Source=desktop-3rfp42t\sqlexpress;" + "Initial Catalog=FoodShopOnline;" + "Integrated Security=True");
        public ProductCategoryFunc()
        {
            db = new FoodShopOnlineDBContext();
        }
        //hiển thị dữ liệu danh mục sản phẩm
        public List<ProductCategory> ListAllProCate()
        {
            var list = db.Database.SqlQuery<ProductCategory>("Sp_ProductCategory").ToList();
            return list;
            //return string.Format("select * from UF_ProductCategory_ListAll()").ToList();
        }
    }
}
