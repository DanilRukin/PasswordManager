using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PasswordManager.Domain.DatabaseAgregate;
using PasswordManager.Domain.GroupAgregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Data.EntityTypeConfigurations
{
    public class DatabaseAgregateConfiguration : IEntityTypeConfiguration<Database>
    {
        public void Configure(EntityTypeBuilder<Database> builder)
        {
            builder.ToTable(DataConstants.GroupAndDatabaseCommonTableName);

            builder.HasKey(x => x.Id);
            builder.Property(d => d.Id).ValueGeneratedOnAdd();

            builder.Ignore(d => d.DomainEvents);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(d => d.ParentDatabase).IsRequired(false);

            builder.HasMany<Group>(DataConstants.GroupsCollectionName)
                .WithOne(g => g.ParentDatabase)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
