using System.Data.Entity.ModelConfiguration;
using DataAccess.Entities;

namespace DataAccess.Infrastructure.Configurations
{
    public class TerritoriesConfiguration : EntityTypeConfiguration<Territory>
    {
        public TerritoriesConfiguration()
        {
            ToTable("dbo.Territories");
            HasKey(x => x.TerritoryId);

            Property(x => x.TerritoryId).HasColumnName("TerritoryID").IsRequired().HasMaxLength(20);
            Property(x => x.TerritoryDescription).HasColumnName("TerritoryDescription").IsRequired().HasMaxLength(50);
            Property(x => x.RegionId).HasColumnName("RegionID").IsRequired();

             
            HasRequired(a => a.Region).WithMany(b => b.Territories).HasForeignKey(c => c.RegionId);  
        }
    }
}