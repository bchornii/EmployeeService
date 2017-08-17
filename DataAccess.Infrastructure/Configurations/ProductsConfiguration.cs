using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataAccess.Entities;

namespace DataAccess.Infrastructure.Configurations
{
    public class ProductsConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductsConfiguration()
        {
            ToTable("dbo.Products");
            HasKey(x => x.ProductId);

            Property(x => x.ProductId).HasColumnName("ProductID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasMaxLength(40);
            Property(x => x.SupplierId).HasColumnName("SupplierID").IsOptional();
            Property(x => x.CategoryId).HasColumnName("CategoryID").IsOptional();
            Property(x => x.QuantityPerUnit).HasColumnName("QuantityPerUnit").IsOptional().HasMaxLength(20);
            Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsOptional();
            Property(x => x.UnitsInStock).HasColumnName("UnitsInStock").IsOptional();
            Property(x => x.UnitsOnOrder).HasColumnName("UnitsOnOrder").IsOptional();
            Property(x => x.ReorderLevel).HasColumnName("ReorderLevel").IsOptional();
            Property(x => x.Discontinued).HasColumnName("Discontinued").IsRequired();

             
            HasOptional(a => a.Supplier).WithMany(b => b.Products).HasForeignKey(c => c.SupplierId);  
            HasOptional(a => a.Category).WithMany(b => b.Products).HasForeignKey(c => c.CategoryId);  
        }
    }
}