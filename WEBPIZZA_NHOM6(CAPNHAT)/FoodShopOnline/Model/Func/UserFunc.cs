using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Model.EnityFramework;
using PagedList;
using System.Data.SqlClient;
namespace Model.Func
{
    //data access object
    public class UserFunc
    {
        FoodShopOnlineDBContext db = null;
        SqlConnection con = new SqlConnection(@"Data Source=desktop-3rfp42t\sqlexpress;" + "Initial Catalog=FoodShopOnline;" + "Integrated Security=True");
        public UserFunc()
        {
            db = new FoodShopOnlineDBContext();
        }
        //CÁC THAO TÁC HÀM THÊM, XÓA, SỬA TRANG USER SỬ DỤNG STORED PROCEDUCE
        public int insert(string username, string password, string groupID, string name, string address, string email, 
            string phone,bool status )
        {
            SqlCommand cmd = new SqlCommand("Sp_Users_Insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("UserName", username);
            cmd.Parameters.AddWithValue("Password", password);
            cmd.Parameters.AddWithValue("GroupID", groupID);
            cmd.Parameters.AddWithValue("Name", name);
            cmd.Parameters.AddWithValue("Address", address);
            cmd.Parameters.AddWithValue("Email", email);
            cmd.Parameters.AddWithValue("Phone", phone);
            cmd.Parameters.AddWithValue("Status", status);
            con.Open();
            int k = cmd.ExecuteNonQuery();
            con.Close();
            return k;

        }
        //phương thức tìm kiếm lấy ra các bản ghi, sử dụng để hiện tất cả các danh sách người dùng
        //chức năng dành riêng cho admin
        public IEnumerable<User> ListAllPaging(string searchString, int page = 1, int pagesize = 10)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString) || x.GroupID.Contains(searchString));

            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pagesize);
        }
        //hàm Update bản ghi User trong database, sử dụng Store Procedure
        public bool UpdateUser(User entity)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Sp_Users_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("ID", entity.ID);
                cmd.Parameters.AddWithValue("UserName", entity.UserName);
                cmd.Parameters.AddWithValue("Password", entity.Password);
                cmd.Parameters.AddWithValue("GroupID", entity.GroupID);
                cmd.Parameters.AddWithValue("Name", entity.Name);
                cmd.Parameters.AddWithValue("Address", entity.Address);
                cmd.Parameters.AddWithValue("Email", entity.Email);
                cmd.Parameters.AddWithValue("Phone", entity.Phone);
                cmd.Parameters.AddWithValue("Status", entity.Status);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }
        }
        //Hàm xóa thông tin User của id được truyền vào, sử dụng Store Procedure
        public bool DeleteUser(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Sp_User_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("ID", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch(Exception ex)
            {
                con.Close();
                return false;
            }
        }
        //hàm chi tiết bản ghi user được update, truyền vào một ID user, sử dụng Stored Procedure
        public User ViewDetailUser(int id)
        {
            //return db.Users.Find(id);

            var idParameter = new SqlParameter
            {
                ParameterName = "ID",
                Value = id
            };
            return db.Database.SqlQuery<User>("EXEC SP_DetailUserByID @ID", idParameter).SingleOrDefault();
        }
        //hàm trả về bản ghi user để thực hiện tính năng đăng nhập, Xét UserName được nhập vào thỏa mãn điều kiện nào của tính năng đăng nhập, sử dụng Stored Procedure
        public User GetUserByUserName(string userName)
        {
            //return db.Users.SingleOrDefault(x => x.UserName == userName);
            var nameParameter = new SqlParameter
            {
                ParameterName = "UserName",
                Value = userName
            };
            return db.Database.SqlQuery<User>("EXEC SP_DetailUserByUserName @UserName", nameParameter).SingleOrDefault();
        }
        //
        public int Login(string userName, string passWord, bool IsLoginAdmin = false)
        {
            //var result = db.Users.SingleOrDefault(x => x.UserName == userName); // biểu thức lamda expression, x là đại diện cho mỗi phần tử của user lặp qua
            var nameParameter = new SqlParameter
            {
                ParameterName = "UserName",
                Value = userName
            };
            var result = db.Database.SqlQuery<User>("EXEC SP_DetailUserByUserName @UserName", nameParameter).SingleOrDefault();
            if (result == null)
            {
                return 0; //tài khoản không tồn tại
            }
            else
            {
                if(IsLoginAdmin == true)
                {
                    if((result.GroupID == CommonConstant.ADMIN_GROUP) || (result.GroupID == CommonConstant.MOD_GROUP))
                    {
                        if (result.Status == false)
                        {
                            return -1; //tài khoản đang bị khóa
                        }
                        else
                        {
                            if (result.Password == passWord)
                            {
                                return 1; //đăng nhập thành công
                            }
                            else
                            {
                                return -2; //nhập sai password
                            }
                        }
                    }
                    else
                    {
                        return -3;  //tài khoản không có quyền đăng nhập
                    }
                }
                else
                {
                    if (result.Status == false)
                    {
                        return -1; //tài khoản đang bị khóa
                    }
                    else if((result.GroupID == CommonConstant.ADMIN_GROUP))
                    {
                        if (result.Password == passWord)
                        {
                            return 1; //đăng nhập thành công
                        }
                        else
                        {
                            return -2; //nhập sai password
                        }
                    }
                    else
                    {
                        if (result.Password == passWord)
                        {
                            return 1; //đăng nhập thành công
                        }
                        else
                        {
                            return -2; //nhập sai password
                        }
                    }
                }
            }
        }
        public List<string> GetListCredential(string userName)
        {
            var user = db.Users.Single(x => x.UserName == userName);
            var data = (from a in db.Credentials
                        join b in db.UserGroups on a.UserGroupID equals b.ID
                        join c in db.Roles on a.RoleID equals c.ID
                        where b.ID == user.GroupID
                        select new
                        {
                            RoleID = a.RoleID,
                            UserGroupID = a.UserGroupID
                        }).AsEnumerable().Select(x => new Credential()
                        {
                            RoleID = x.RoleID,
                            UserGroupID = x.UserGroupID
                        });
            return data.Select(x => x.RoleID).ToList();
        }
        ///////////////////////////////////////////////////////
        public bool ChangeStatus(int id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
            //var idParameter = new SqlParameter
            //{
            //    ParameterName = "ID",
            //    Value = id
            //};
            //var user = db.Database.SqlQuery<User>("EXEC SP_DetailUserByID @ID", idParameter).SingleOrDefault();
            //user.Status = !user.Status;
            //return user.Status;
        }

        //////////////////////////////////////////
        public List<Menu> ListByGroupID(int groupId)
        {
            return db.Menus.Where(x => x.GroupID == groupId && x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
        ///////////////////////////////////////////
        //Hàm check UserName đã tồn tại hay chưa khi đăng kí tài khoản mới, sử dụng FUNCTION 
        public int CheckUserName(string userName)
        {
            //return db.Users.Count(x => x.UserName == userName) > 0;
            SqlCommand cmd = new SqlCommand("SELECT dbo.UF_CheckUserName(@UserName)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@UserName", userName));
            con.Open();
            var result = cmd.ExecuteScalar();
            con.Close();
            return (int)result;
        }
        //Hàm check Email đã tồn tại hay chưa khi đăng kí tài khoản mới, sử dụng FUNCTION
        public int CheckEmail(string email)
        {
            //return db.Users.Count(x => x.Email == email) > 0;
            SqlCommand cmd = new SqlCommand("SELECT dbo.UF_CheckEmail(@Email)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@Email", email));
            con.Open();
            var result = cmd.ExecuteScalar();
            con.Close();
            return (int)result;
        }
        //hiển thị dữ liệu nhóm người dùng sử dụng Store Procedure
        public List<UserGroup> ListUserGroup()
        {
            var list = db.Database.SqlQuery<UserGroup>("SP_UserGroup").ToList();
            return list;
        }
    }
}
