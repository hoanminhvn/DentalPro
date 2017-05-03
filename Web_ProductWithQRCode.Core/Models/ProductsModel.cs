using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ProductWithQRCode.Core.CustomModel;
using Web_ProductWithQRCode.Core.Entity;

namespace Web_ProductWithQRCode.Core.Models
{
    public class ProductsModel
    {
        public dynamic GetAll()
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Products.OrderByDescending(x => x.ID)
                                    .Select(x => new
                                    {
                                        ID = x.ID,
                                        Name = x.Name,
                                        Intro = x.Intro,
                                        Content = x.Content,
                                        Image = x.Image,
                                        Price = x.Price,
                                        Discount = x.Discount,
                                        ListImage = x.ListImage,
                                        CreateBy = x.Users.Name,
                                        CreateDate = x.CreateDateTime,
                                        Meta = x.Meta,
                                        CateID = x.CateID,
                                        Status = x.Status,
                                        QRCode = x.QRCode
                                    }).ToList();
            }
        }

        public dynamic GetById(int id)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                return db.Products.Where(x => x.ID == id)
                                    .Select(x => new
                                    {
                                        ID = x.ID,
                                        Name = x.Name,
                                        Intro = x.Intro,
                                        Content = x.Content,
                                        Image = x.Image,
                                        Price = x.Price,
                                        Discount = x.Discount,
                                        ListImage = x.ListImage,
                                        CreateBy = x.CreateBy,
                                        CreateDate = x.CreateDateTime,
                                        Meta = x.Meta,
                                        CateID = x.CateID,
                                        Lang = x.Lang,
                                        Status = x.Status,
                                        QRCode = x.QRCode,
                                        Tag = x.Tag
                                    }).Single();
            }
        }
        public List<Products> GetByCateMeta(string meta)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                int cateId = db.Categories.Where(x => x.Meta.Contains(meta)).Single().ID;
                var product = db.Products.Where(x => x.Status == true && x.CateID.Contains(":" + cateId + ":")).ToList();
                return product;
            }
        }
        public List<Products> GetByCateId(int cateId)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                var product = db.Products.Where(x => x.Status == true && x.CateID.Contains(":" + cateId + ":")).ToList();
                return product;
            }
        }
        public Products GetByMeta(string meta)
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                var product = db.Products.Where(x => x.Status == true && x.Meta.Contains(meta)).SingleOrDefault();
                return product;
            }
        }
        public List<Products> GetRecentProduct()
        {
            using (ProductWithQRCodeDbContext db = new ProductWithQRCodeDbContext())
            {
                var listProduct = db.Products.Where(x => x.Status == true).Take(12);
                if (listProduct != null)
                    return listProduct.ToList();
                return null;
            }
        }
        public bool insert(Products entity)
        {
            using (var db = new ProductWithQRCodeDbContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Products _pro = db.Products.OrderByDescending(u => u.ID).FirstOrDefault();
                        if (_pro != null)
                        {
                            entity.ID = _pro.ID + 1;
                        }
                        else entity.ID = 1;
                        db.Products.Add(entity);
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
        public bool update(Products entity)
        {
            using (var db = new ProductWithQRCodeDbContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try{
                        db.Products.Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        return true;
                    }
                    catch(Exception){
                        dbContextTransaction.Rollback();
                        return false;
                    }
                }
            }
        }
        public bool delete(int _proId)
        {
            using (var db = new ProductWithQRCodeDbContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var product = db.Products.SingleOrDefault(x => x.ID == _proId);
                        product.Status = false;
                        db.Products.Attach(product);
                        db.Entry(product).State = EntityState.Modified;
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
    }
}
