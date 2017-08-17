using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataAccess.Entities;

namespace DataAccess.Infrastructure.Configurations
{
    public class CategoriesConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoriesConfiguration()
        {
            ToTable("dbo.Categories");
            HasKey(x => x.CategoryId);

            Property(x => x.CategoryId).HasColumnName("CategoryID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CategoryName).HasColumnName("CategoryName").IsRequired().HasMaxLength(15);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(1073741823);
            Property(x => x.Picture).HasColumnName("Picture").IsOptional().HasMaxLength(2147483647);
        }
    }
}
