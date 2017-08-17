using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataAccess.Entities;

namespace DataAccess.Infrastructure.Configurations
{
    public class OrdersConfiguration : EntityTypeConfiguration<Order>
    {
        public OrdersConfiguration()
        {
            ToTable("dbo.Orders");
            HasKey(x => x.OrderId);

            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CustomerId).HasColumnName("CustomerID").IsOptional().HasMaxLength(5);
            Property(x => x.EmployeeId).HasColumnName("EmployeeID").IsOptional();
            Property(x => x.OrderDate).HasColumnName("OrderDate").IsOptional();
            Property(x => x.RequiredDate).HasColumnName("RequiredDate").IsOptional();
            Property(x => x.ShippedDate).HasColumnName("ShippedDate").IsOptional();
            Property(x => x.ShipVia).HasColumnName("ShipVia").IsOptional();
            Property(x => x.Freight).HasColumnName("Freight").IsOptional();
            Property(x => x.ShipName).HasColumnName("ShipName").IsOptional().HasMaxLength(40);
            Property(x => x.ShipAddress).HasColumnName("ShipAddress").IsOptional().HasMaxLength(60);
            Property(x => x.ShipCity).HasColumnName("ShipCity").IsOptional().HasMaxLength(15);
            Property(x => x.ShipRegion).HasColumnName("ShipRegion").IsOptional().HasMaxLength(15);
            Property(x => x.ShipPostalCode).HasColumnName("ShipPostalCode").IsOptional().HasMaxLength(10);
            Property(x => x.ShipCountry).HasColumnName("ShipCountry").IsOptional().HasMaxLength(15);

             
            HasOptional(a => a.Customer).WithMany(b => b.Orders).HasForeignKey(c => c.CustomerId);  
            HasOptional(a => a.Employee).WithMany(b => b.Orders).HasForeignKey(c => c.EmployeeId);  
            HasOptional(a => a.Shipper).WithMany(b => b.Orders).HasForeignKey(c => c.ShipVia);  
        }
    }
}