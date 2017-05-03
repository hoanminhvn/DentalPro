using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ProductWithQRCode.Core.Entity;

namespace Web_ProductWithQRCode.Core.Models
{
    public class CategoriesModel
    {
        public dynamic GetAll(int type)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Categories.OrderByDescending(x => x.ID)
                                    .Where(x => x.Type == type)
                                    .Select(x => new
                                    {
                                        ID = x.ID,
                                        Name = x.Name,
                                        Meta = x.Meta,
                                        Parent = x.Parent,
                                        ParentName = db.Categories.Where(y => y.ID == x.Parent).FirstOrDefault().Name,
                                        Image = x.Image,
                                        Status = x.Status,
                                        Type = x.Type
                                    }).ToList();
            }
        }

        public Categories GetById(int id)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Categories.Where(x => x.Status == true && x.ID == id).SingleOrDefault();
            }
        }
        public bool Insert(Categories entity)
        {
            using (var db = new ProductWithQRCodeDbContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Categories.Add(entity);
                        db.SaveChanges();
                        dbContextTransaction.Commit(); 
                        return true;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return false;
                    }
                }
            }
        }
        public bool Update(Categories entity)
        {
            using (var db = new ProductWithQRCodeDbContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();
                        dbContextTransaction.Commit(); 
                        return true;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool UpdateMultiRecords(int[] idList)
        {
            using (var db = new ProductWithQRCodeDbContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var categories = db.Categories.Where(x => idList.Contains(x.ID)).ToList();
                        categories.ForEach(a => a.Status = false);
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return false;
                    }
                }
            }
        }
        public int Delete(int _cateId)
        {
            using (var db = new ProductWithQRCodeDbContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var category = db.Categories.Find(_cateId);
                        category.Status = false;
                        db.Categories.Attach(category);
                        db.Entry(category).State = EntityState.Modified;
                        db.SaveChanges();
                        dbContextTransaction.Commit(); 
                        return (int)category.Type;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return 0;
                    }
                }
            }
        }
    }
}
