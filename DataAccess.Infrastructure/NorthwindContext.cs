using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DataAccess.Entities;
using DataAccess.Infrastructure.Configurations;

namespace DataAccess.Infrastructure
{
    public interface INorthwindContext : IDisposable
    {
        IDbSet<Category> Categories { get; set; }  
        IDbSet<Contact> Contacts { get; set; }  
        IDbSet<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }  
        IDbSet<CustomerDemographic> CustomerDemographics { get; set; }  
        IDbSet<Customer> Customers { get; set; }  
        IDbSet<Employee> Employees { get; set; }  
        IDbSet<EmployeeTerritory> EmployeeTerritories { get; set; }  
        IDbSet<Order> Orders { get; set; }  
        IDbSet<Product> Products { get; set; }  
        IDbSet<Region> Region { get; set; }  
        IDbSet<Shipper> Shippers { get; set; }  
        IDbSet<Supplier> Suppliers { get; set; }  
        IDbSet<Territory> Territories { get; set; }
        IDbSet<OrderDetail> OrderDetails { get; set; }

        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbSet Set(Type entityType);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }

    public class NorthwindContext : DbContext, INorthwindContext
    {
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Contact> Contacts { get; set; }
        public IDbSet<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
        public IDbSet<CustomerDemographic> CustomerDemographics { get; set; }
        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<Employee> Employees { get; set; }
        public IDbSet<EmployeeTerritory> EmployeeTerritories { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<Region> Region { get; set; }
        public IDbSet<Shipper> Shippers { get; set; }
        public IDbSet<Supplier> Suppliers { get; set; }
        public IDbSet<Territory> Territories { get; set; }
        public IDbSet<OrderDetail> OrderDetails { get; set; }

        static NorthwindContext()
        {
            Database.SetInitializer<NorthwindContext>(null);
        }

        public NorthwindContext()
            : base("Name=Northwind")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CategoriesConfiguration());
            modelBuilder.Configurations.Add(new ContactsConfiguration());
            modelBuilder.Configurations.Add(new CustomerCustomerDemoConfiguration());
            modelBuilder.Configurations.Add(new CustomerDemographicsConfiguration());
            modelBuilder.Configurations.Add(new CustomersConfiguration());
            modelBuilder.Configurations.Add(new EmployeesConfiguration());
            modelBuilder.Configurations.Add(new EmployeeTerritoriesConfiguration());
            modelBuilder.Configurations.Add(new OrdersConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailsConfiguration());
            modelBuilder.Configurations.Add(new ProductsConfiguration());
            modelBuilder.Configurations.Add(new RegionConfiguration());
            modelBuilder.Configurations.Add(new ShippersConfiguration());
            modelBuilder.Configurations.Add(new SuppliersConfiguration());
            modelBuilder.Configurations.Add(new TerritoriesConfiguration());
        }
    }
}
