using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ProductWithQRCode.Core.CustomModel;
using Web_ProductWithQRCode.Core.Entity;

namespace WebQuanLyCoKhi.Core.Models
{
    public class GroupsUserModel
    {
        public dynamic getAll()
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.GroupsUser.Where(x => x.Status == true)
                                .Select(x => new
                                {
                                    MaTaiKhoan = x.ID,
                                    TenTaiKhoan = x.Name
                                }).ToList();
            }
        }
        public dynamic insert(GroupsUser entity)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Database.SqlQuery<ExecQueryResult>("exec TaiKhoan_insert @maTaiKhoan, @tenTaiKhoan, @tinhTrang",
                            new SqlParameter("@maTaiKhoan", entity.ID),
                            new SqlParameter("@tenTaiKhoan", entity.Name),
                            new SqlParameter("@tinhTrang", entity.Status)).Single();
            }
        }
        public dynamic update(GroupsUser entity)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Database.SqlQuery<ExecQueryResult>("exec TaiKhoan_update @maTaiKhoan, @tenTaiKhoan, @tinhTrang",
                            new SqlParameter("@maTaiKhoan", entity.ID),
                            new SqlParameter("@tenTaiKhoan", entity.Name),
                            new SqlParameter("@tinhTrang", entity.Status)).Single();
            }
        }
        public dynamic delete(string _maTaiKhoan)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Database.SqlQuery<ExecQueryResult>("exec TaiKhoan_delete @maTaiKhoan",
                            new SqlParameter("@maTaiKhoan", _maTaiKhoan)).Single();
            }
        }
    }
}
