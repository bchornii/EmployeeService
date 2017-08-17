using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataAccess.Entities;

namespace DataAccess.Infrastructure.Configurations
{
    public class OrderDetailsConfiguration : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailsConfiguration()
        {
            ToTable("dbo.OrderDetails");
            HasKey(x => new { x.OrderId, x.ProductId });

            Property(x => x.OrderId).HasColumnName("OrderID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ProductId).HasColumnName("ProductID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsRequired();
            Property(x => x.Quantity).HasColumnName("Quantity").IsRequired();
            Property(x => x.Discount).HasColumnName("Discount").IsRequired();
           
            HasRequired(a => a.Order).WithMany(b => b.OrderDetails).HasForeignKey(c => c.OrderId); 
            HasRequired(a => a.Product).WithMany(b => b.OrderDetails).HasForeignKey(c => c.ProductId); 
        }
    }
}