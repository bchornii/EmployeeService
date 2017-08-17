using System.Data.Entity.ModelConfiguration;
using DataAccess.Entities;

namespace DataAccess.Infrastructure.Configurations
{
    public class CustomerCustomerDemoConfiguration : EntityTypeConfiguration<CustomerCustomerDemo>
    {
        public CustomerCustomerDemoConfiguration()
        {
            ToTable("dbo.CustomerCustomerDemo");
            HasKey(x => new { x.CustomerId, x.CustomerTypeId });

            Property(x => x.CustomerId).HasColumnName("CustomerID").IsRequired().HasMaxLength(5);
            Property(x => x.CustomerTypeId).HasColumnName("CustomerTypeID").IsRequired().HasMaxLength(10);

             
            HasRequired(a => a.Customers).WithMany(b => b.CustomerCustomerDemos).HasForeignKey(c => c.CustomerId);  
            HasRequired(a => a.CustomerDemographic).WithMany(b => b.CustomerCustomerDemos).HasForeignKey(c => c.CustomerTypeId);  
        }
    }
}