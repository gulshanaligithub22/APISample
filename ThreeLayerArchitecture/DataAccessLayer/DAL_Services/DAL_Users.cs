using DataAccessLayer.Entity;
using DataAccessLayer.ViewClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccessLayer
{
    public class DAL_Users
    {
        private readonly Entity.UserDbContext _DbContext;

        public DAL_Users( )
        {
            _DbContext = new Entity.UserDbContext();
        }
        #region Users
        public async Task<List<User>> GetUsers()
        {
            List<User> users = await _DbContext.Users.ToListAsync();
            return users;
        }
        public async Task<ActionResult<User>> GetUserByID(int UserID)
        {
            var user = await _DbContext.Users.Where(x => x.UserID == UserID).FirstOrDefaultAsync();
            return user;
        }

        public async Task CreateUser(User user)
        {
            user.CreatedDate = System.DateTime.Now;
            await _DbContext.AddAsync(user);
            await _DbContext.SaveChangesAsync();
        }
      

        public async Task<bool> UpdateUser(User user)
        {
            var users = await _DbContext.Users.Where(c => c.UserID == user.UserID).FirstOrDefaultAsync();
            if (users != null)
            {
                users.UserName = user.UserName;
                users.EmailID = user.EmailID;
                users.UserID = user.UserID;
                users.Password = user.Password;
                users.CreatedDate = System.DateTime.Now;
                _DbContext.Update(users);
                await _DbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> DeleteUser(int id)
        {
            var users = await _DbContext.Users.Where(c => c.UserID == id).FirstOrDefaultAsync();
            if(users != null)
            {
                _DbContext.Users.Remove(users);
                await _DbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<UserView_POCO>> GetAllbyProc()
        {
            string StoredProc = "exec GetAll ";
            List<UserView_POCO> result = await _DbContext.UserPOCOModels.FromSqlRaw(StoredProc).ToListAsync();
            return result;
        }

        public async Task<ActionResult<object>> GetAllDynamicColumn()
        {
            string conn =  _DbContext.Database.GetDbConnection().ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection sql = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.GetAll", sql))
                {
                    sql.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    dt.Load(dr);
                    sql.Close();
                    List<Dictionary<string, object>> rows =  new List<Dictionary<string, object>>();
                    Dictionary<string, object> row;
                    foreach (DataRow drs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, drs[col]);
                        }
                        rows.Add(row);
                    }
                    return rows;
                }
            }
        }
        #endregion
    }
}
