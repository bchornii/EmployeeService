using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataAccess.Entities;

namespace DataAccess.Infrastructure.Configurations
{
    public class ShippersConfiguration : EntityTypeConfiguration<Shipper>
    {
        public ShippersConfiguration()
        {
            ToTable("dbo.Shippers");
            HasKey(x => x.ShipperId);

            Property(x => x.ShipperId).HasColumnName("ShipperID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasMaxLength(40);
            Property(x => x.Phone).HasColumnName("Phone").IsOptional().HasMaxLength(24);
        }
    }
}