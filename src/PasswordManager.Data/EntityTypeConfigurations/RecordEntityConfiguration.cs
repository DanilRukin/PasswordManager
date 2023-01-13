using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PasswordManager.Domain.GroupAgregate;
using PasswordManager.Domain.RecordEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Data.EntityTypeConfigurations
{
    public class RecordEntityConfiguration : IEntityTypeConfiguration<Record>
    {
        public void Configure(EntityTypeBuilder<Record> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();

            builder.Ignore(r => r.DomainEvents);

            builder.Property(r => r.ResourceUrl).IsRequired();
            builder.Property(r => r.ResourceName).IsRequired();
            builder.Property(r => r.Description).IsRequired();
            builder.Property(r => r.ResourcePasswordHash).IsRequired();
            builder.Property(r => r.RecordContainerId).IsRequired();
            builder.Property(r => r.CreationDate).IsRequired();
            builder.Property(r => r.LastAccessDate).IsRequired();
            builder.Property(r => r.LastModifiedDate).IsRequired();

            builder.HasOne<Group>()
                .WithMany(DataConstants.RecordsCollectionName)
                .HasForeignKey(r => r.RecordContainerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
