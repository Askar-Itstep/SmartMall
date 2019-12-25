namespace SmartMall
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Debitors> Debitors { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<StatisticSeller> StatisticSeller { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<Customers>()
                .HasMany(e => e.Debitors)
                .WithOptional(e => e.Customers)
                .HasForeignKey(e => e.custom_id);

            modelBuilder.Entity<Customers>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Customers)
                .HasForeignKey(e => e.custom_id);

            modelBuilder.Entity<Debitors>()
                .Property(e => e.sum_begin_debit)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Debitors>()
                .Property(e => e.new_pay)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Debitors>()
                .Property(e => e.current_sum_debit)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Employees>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Employees)
                .HasForeignKey(e => e.seller_id);

            modelBuilder.Entity<Orders>()
                .Property(e => e.sum_pay)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .Property(e => e.sum_order)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .Property(e => e.sum_debit)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .HasMany(e => e.Debitors)
                .WithOptional(e => e.Orders)
                .HasForeignKey(e => e.order_id);

            modelBuilder.Entity<Products>()
                .Property(e => e.purch_price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Products>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Products)
                .HasForeignKey(e => e.prod_id);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Customers)
                .WithOptional(e => e.Roles)
                .HasForeignKey(e => e.role_id);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.Roles)
                .HasForeignKey(e => e.role_id);

            modelBuilder.Entity<StatisticSeller>()
                .Property(e => e.sum_cash)
                .HasPrecision(19, 4);
        }
    }
}
