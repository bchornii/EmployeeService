using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataAccess.Entities;

namespace DataAccess.Infrastructure.Configurations
{
    public class SuppliersConfiguration : EntityTypeConfiguration<Supplier>
    {
        public SuppliersConfiguration()
        {
            ToTable("dbo.Suppliers");
            HasKey(x => x.SupplierId);

            Property(x => x.SupplierId).HasColumnName("SupplierID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasMaxLength(40);
            Property(x => x.ContactName).HasColumnName("ContactName").IsOptional().HasMaxLength(30);
            Property(x => x.ContactTitle).HasColumnName("ContactTitle").IsOptional().HasMaxLength(30);
            Property(x => x.Address).HasColumnName("Address").IsOptional().HasMaxLength(60);
            Property(x => x.City).HasColumnName("City").IsOptional().HasMaxLength(15);
            Property(x => x.Region).HasColumnName("Region").IsOptional().HasMaxLength(15);
            Property(x => x.PostalCode).HasColumnName("PostalCode").IsOptional().HasMaxLength(10);
            Property(x => x.Country).HasColumnName("Country").IsOptional().HasMaxLength(15);
            Property(x => x.Phone).HasColumnName("Phone").IsOptional().HasMaxLength(24);
            Property(x => x.Fax).HasColumnName("Fax").IsOptional().HasMaxLength(24);
            Property(x => x.HomePage).HasColumnName("HomePage").IsOptional().HasMaxLength(1073741823);
        }
    }
}