namespace Web_ProductWithQRCode.Core.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProductWithQRCodeDbContext : DbContext
    {
        public ProductWithQRCodeDbContext()
            : base("name=ProductWithQRCodeDbContext")
        {
        }

        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<Functionals> Functionals { get; set; }
        public virtual DbSet<GroupsUser> GroupsUser { get; set; }
        public virtual DbSet<Modules> Modules { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Slideshows> Slideshows { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articles>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Articles>()
                .Property(e => e.Meta)
                .IsUnicode(false);

            modelBuilder.Entity<Categories>()
                .Property(e => e.Meta)
                .IsUnicode(false);

            modelBuilder.Entity<Categories>()
                .HasMany(e => e.Articles)
                .WithOptional(e => e.Categories)
                .HasForeignKey(e => e.CateID);

            modelBuilder.Entity<Categories>()
                .HasMany(e => e.Slideshows)
                .WithOptional(e => e.Categories)
                .HasForeignKey(e => e.CateID);

            modelBuilder.Entity<Contacts>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Contacts>()
                .Property(e => e.Mail)
                .IsUnicode(false);

            modelBuilder.Entity<Functionals>()
                .HasMany(e => e.Permissions)
                .WithRequired(e => e.Functionals)
                .HasForeignKey(e => e.FunctionalID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GroupsUser>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<GroupsUser>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.GroupsUser)
                .HasForeignKey(e => e.GroupID);

            modelBuilder.Entity<GroupsUser>()
                .HasMany(e => e.Permissions)
                .WithRequired(e => e.GroupsUser)
                .HasForeignKey(e => e.GroupID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Modules>()
                .HasMany(e => e.Functionals)
                .WithOptional(e => e.Modules)
                .HasForeignKey(e => e.ModuleID);

            modelBuilder.Entity<Products>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.Meta)
                .IsUnicode(false);

            modelBuilder.Entity<Slideshows>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Articles)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.CreateBy);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.CreateBy);
        }
    }
}
