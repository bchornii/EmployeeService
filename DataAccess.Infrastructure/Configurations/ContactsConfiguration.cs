using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataAccess.Entities;

namespace DataAccess.Infrastructure.Configurations
{
    public class ContactsConfiguration : EntityTypeConfiguration<Contact>
    {
        public ContactsConfiguration()
        {
            ToTable("dbo.Contacts");
            HasKey(x => x.ContactId);

            Property(x => x.ContactId).HasColumnName("ContactID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ContactType).HasColumnName("ContactType").IsOptional().HasMaxLength(50);
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasMaxLength(40);
            Property(x => x.ContactName).HasColumnName("ContactName").IsOptional().HasMaxLength(30);
            Property(x => x.ContactTitle).HasColumnName("ContactTitle").IsOptional().HasMaxLength(30);
            Property(x => x.Address).HasColumnName("Address").IsOptional().HasMaxLength(60);
            Property(x => x.City).HasColumnName("City").IsOptional().HasMaxLength(15);
            Property(x => x.Region).HasColumnName("Region").IsOptional().HasMaxLength(15);
            Property(x => x.PostalCode).HasColumnName("PostalCode").IsOptional().HasMaxLength(10);
            Property(x => x.Country).HasColumnName("Country").IsOptional().HasMaxLength(15);
            Property(x => x.Phone).HasColumnName("Phone").IsOptional().HasMaxLength(24);
            Property(x => x.Extension).HasColumnName("Extension").IsOptional().HasMaxLength(4);
            Property(x => x.Fax).HasColumnName("Fax").IsOptional().HasMaxLength(24);
            Property(x => x.HomePage).HasColumnName("HomePage").IsOptional().HasMaxLength(1073741823);
            Property(x => x.PhotoPath).HasColumnName("PhotoPath").IsOptional().HasMaxLength(255);
            Property(x => x.Photo).HasColumnName("Photo").IsOptional().HasMaxLength(2147483647);
        }
    }
}