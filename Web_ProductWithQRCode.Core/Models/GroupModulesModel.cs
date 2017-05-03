using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ProductWithQRCode.Core.CustomModel;
using Web_ProductWithQRCode.Core.Entity;

namespace Web_PRoductWithQRCode.Core.Models
{
    public class GroupModulesModel
    {
        public dynamic getAll()
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Modules.Where(x => x.Status == true)
                                      .Select(x => new
                                      {
                                          ID = x.ID,
                                          Name = x.Name
                                      }).ToList();
            }
        }
        public dynamic insert(Modules entity)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Database.SqlQuery<ExecQueryResult>("exec NhomChucNang_insert @tenChucNang, @tinhTrang",
                                        new SqlParameter("@tenChucNang", entity.Name),
                                        new SqlParameter("@tinhTrang", entity.Status)).Single();
            }
        }
        public dynamic update(Modules entity)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Database.SqlQuery<ExecQueryResult>("exec NhomChucNang_update @maChucNang, @tenChucNang",
                                        new SqlParameter("@maChucNang", entity.ID),
                                        new SqlParameter("@tenChucNang", entity.Name),
                                        new SqlParameter("@tinhTrang", entity.Status)).Single();
            }
        }
    }
}
