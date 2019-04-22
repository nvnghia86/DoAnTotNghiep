namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using DataAccess.Models;
    

    public class DataContext : DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DataAccess.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public DataContext()
            : base("name=DataContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<ServiceWarrantyCard> ServiceWarrantyCards { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleGroup> ArticleGroups { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderRepair> OrderRepairs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        //public DbSet<PromotionalProduct> PromotionalProducts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
        public DbSet<GuaranteeProduct> GuaranteeProducts { get; set; }
        public DbSet<ProductWarrantyCard> ProductWarrantyCards { get; set; }
        //public DbSet<PromotionalProduct> PromotionalProducts { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}