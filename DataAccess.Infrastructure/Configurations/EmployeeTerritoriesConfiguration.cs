using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataAccess.Entities;

namespace DataAccess.Infrastructure.Configurations
{
    public class EmployeeTerritoriesConfiguration : EntityTypeConfiguration<EmployeeTerritory>
    {
        public EmployeeTerritoriesConfiguration()
        {
            ToTable("dbo.EmployeeTerritories");
            HasKey(x => new { x.EmployeeId, x.TerritoryId });

            Property(x => x.EmployeeId).HasColumnName("EmployeeID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TerritoryId).HasColumnName("TerritoryID").IsRequired().HasMaxLength(20);

             
            HasRequired(a => a.Employee).WithMany(b => b.EmployeeTerritories).HasForeignKey(c => c.EmployeeId);  
            HasRequired(a => a.Territories).WithMany(b => b.EmployeeTerritories).HasForeignKey(c => c.TerritoryId);  
        }
    }
}