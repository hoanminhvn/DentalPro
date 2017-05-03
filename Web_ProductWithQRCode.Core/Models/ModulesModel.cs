using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ProductWithQRCode.Core.CustomModel;
using Web_ProductWithQRCode.Core.Entity;

namespace Web_ProductWithQRCode.Core.Models
{
    public class ModulesModel
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
        public dynamic getByGroup(int _module)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Modules.Where(x => x.ID == _module && x.Status == true)
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
                return db.Database.SqlQuery<ExecQueryResult>("exec ChucNang_insert @tenChucnang, @maNhomChucNang, @tinhTrang",
                                    new SqlParameter("@tenChucNang", entity.Name),
                                    new SqlParameter("@maNhomChucNang", entity.ID),
                                    new SqlParameter("@tinhTrang", entity.Status)).Single();
            }
        }
        //public dynamic update(Modules entity)
        //{
        //    using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
        //    {
        //        return db.Database.SqlQuery<ExecQueryResult>("exec ChucNang_update @maChucNang, @tenChucnang, @maNhomChucNang, @tinhTrang",
        //                            new SqlParameter("@maChucNang", entity.MaChucNang),
        //                            new SqlParameter("@tenChucNang", entity.TenChucNang),
        //                            new SqlParameter("@maNhomChucNang", entity.MaNhomChucNang),
        //                            new SqlParameter("@tinhTrang", entity.TinhTrang)).Single();
        //    }
        //}
        //public dynamic delete(int _maChucNang)
        //{
        //    using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
        //    {
        //        return db.Database.SqlQuery<ExecQueryResult>("exec ChucNang_delete @maChucNang",
        //                            new SqlParameter("@maChucNang", _maChucNang)).Single();
        //    }
        //}
    }
}
