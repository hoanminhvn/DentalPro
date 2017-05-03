using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ProductWithQRCode.Core.Entity;
using Web_ProductWithQRCode.Core.CustomModel;
using Web_ProductWithQRCode.Core.Helper;
using System.Diagnostics;

namespace Web_ProductWithQRCode.Core.Models
{
    public class UsersModel
    {
        public dynamic Login(string _username, string _password)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Database.SqlQuery<ExecQueryResult>("exec Pro_Login @username, @password",
                                new SqlParameter("@username", _username),
                                new SqlParameter("@password", _password)).Single();
            }
        }
        public dynamic getAll()
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Users.OrderByDescending(x => x.ID)
                                .Where(x => x.Status == true)
                                .Select(x => new
                                {
                                    ID = x.ID,
                                    name = x.Name,
                                    gender = x.Gender,
                                    birthday = x.Birthday,
                                    phone = x.Phone,
                                    groupId = x.GroupID,
                                    username = x.Username,
                                    password = x.Password,
                                    group = x.GroupsUser.Name,
                                    status = x.Status
                                }).ToList();
            }
        }
        public dynamic getByUsernamePassword(string _username, string _password)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Database.SqlQuery<AccountSession>("exec Users_getByUsernamePassword @username, @password",
                                                    new SqlParameter("@username", _username),
                                                    new SqlParameter("@password", _password)).Single();
            }
        }

        public dynamic getUserType()
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.GroupsUser.OrderByDescending(x => x.ID)
                        .Where(x => x.Status == true)
                        .Select(x => new
                        {
                            ID = x.ID,
                            name = x.Name
                        }).ToList();
            }
        }

        public dynamic resetPass(string _username, string _birthday)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Database.SqlQuery<ExecQueryResult>("exec Users_Resetpass @username, @birthday",
                                    new SqlParameter("@username", _username),
                                    new SqlParameter("@birthday", DateTime.Parse(_birthday))).Single();
            }
        }
        public dynamic changePass(string _username, string _oldpass, string _newpass)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Database.SqlQuery<ExecQueryResult>("exec Users_ChangePassword @username, @password, @newpass",
                                new SqlParameter("@username", _username),
                                new SqlParameter("@password", _oldpass),
                                new SqlParameter("@newpass", _newpass)).Single();
            }
        }
        public dynamic insert(Users entity)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Database.SqlQuery<ExecQueryResult>("exec Users_insert @fullname, @gender, @birthday, @phone, @username, @password, @type, @status",
                                new SqlParameter("@fullname", entity.Name),
                                new SqlParameter("@gender", entity.Gender),
                                new SqlParameter("@birthday", entity.Birthday),
                                new SqlParameter("@phone", entity.Phone),
                                new SqlParameter("@username", entity.Username),
                                new SqlParameter("@password", entity.Password),
                                new SqlParameter("@type", entity.GroupID),
                                new SqlParameter("@status", entity.Status)).Single();
            }
        }
        public dynamic update(Users entity)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Database.SqlQuery<ExecQueryResult>("exec Users_update @id, @fullname, @gender, @birthday, @phone, @type, @status",
                                new SqlParameter("@id", entity.ID),
                                new SqlParameter("@fullname", entity.Name),
                                new SqlParameter("@gender", entity.Gender),
                                new SqlParameter("@birthday", entity.Birthday),
                                new SqlParameter("@phone", entity.Phone),
                                new SqlParameter("@type", entity.GroupID),
                                new SqlParameter("@status", entity.Status)).Single();
            }
        }
        public dynamic delete(string _maNhanVien)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Database.SqlQuery<ExecQueryResult>("exec Users_delete @id",
                                new SqlParameter("@id", _maNhanVien)).Single();
            }
        }
    }
}
