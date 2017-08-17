using System.Data.Entity.ModelConfiguration;
using DataAccess.Entities;

namespace DataAccess.Infrastructure.Configurations
{
    public class CustomerDemographicsConfiguration : EntityTypeConfiguration<CustomerDemographic>
    {
        public CustomerDemographicsConfiguration()
        {
            ToTable("dbo.CustomerDemographics");
            HasKey(x => x.CustomerTypeId);

            Property(x => x.CustomerTypeId).HasColumnName("CustomerTypeID").IsRequired().HasMaxLength(10);
            Property(x => x.CustomerDesc).HasColumnName("CustomerDesc").IsOptional().HasMaxLength(1073741823);
        }
    }
}