using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using PasswordManager.Domain.DatabaseAgregate;
using PasswordManager.Domain.GroupAgregate;
using PasswordManager.Domain.RecordEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Data.EntityTypeConfigurations
{
    public class GroupAgregateConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable(DataConstants.GroupAndDatabaseCommonTableName);

            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id).ValueGeneratedOnAdd();

            builder.Ignore(g => g.DomainEvents);

            builder.Property(g => g.Name).IsRequired();
            builder.Property(g => g.ParentDatabase).IsRequired(false);

            builder.HasDiscriminator<string>(DataConstants.DiscriminatorName)
                .HasValue<Group>(nameof(Group))
                .HasValue<Database>(nameof(Database));

            builder.HasOne(g => g.ParentDatabase)
                .WithMany(DataConstants.GroupsCollectionName)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Record>(DataConstants.RecordsCollectionName)
                .WithOne()
                .HasForeignKey(r => r.RecordContainerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
