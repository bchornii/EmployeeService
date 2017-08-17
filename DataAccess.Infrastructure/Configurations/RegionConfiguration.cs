using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataAccess.Entities;

namespace DataAccess.Infrastructure.Configurations
{
    public class RegionConfiguration : EntityTypeConfiguration<Region>
    {
        public RegionConfiguration()
        {
            ToTable("dbo.Region");
            HasKey(x => x.RegionId);

            Property(x => x.RegionId).HasColumnName("RegionID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.RegionDescription).HasColumnName("RegionDescription").IsRequired().HasMaxLength(50);
        }
    }
}