namespace ASP_Project.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;

    public class ASPProjectDB : DbContext
    {
        public ASPProjectDB() : base("name=ASPProjectDB")
        {
            Database.SetInitializer(new ASPProjectSeedInitializer());
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Owner)
                .WithMany(u => u.OwnedProducts)
                .HasForeignKey(p => p.OwnerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasOptional(p => p.User)
                .WithMany(u => u.ProductsInCart)
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);

            //modelBuilder.Configurations.Add(new ASPProjectDBConfig());
        }
    }

    //public class ASPProjectDBConfig : EntityTypeConfiguration<ASPProjectDB>
    //{
    //    public ASPProjectDBConfig()
    //    {
    //        this.
    //    }
    //}
}