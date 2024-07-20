using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members").HasKey(m => m.Id);

        builder.Property(m => m.Id).HasColumnName("Id").IsRequired();
        builder.Property(m => m.FirstName).HasColumnName("FirstName").IsRequired();
        builder.Property(m => m.LastName).HasColumnName("LastName").IsRequired();
        builder.Property(m => m.Email).HasColumnName("Email").IsRequired();
        builder.Property(m => m.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(m => m.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(m => m.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(m => m.DeletedDate).HasColumnName("DeletedDate");
        builder.HasIndex(indexExpression: m => m.UserId, name: "Member_UserID_UK").IsUnique();
        builder.HasOne(i => i.User);
        builder.HasQueryFilter(m => !m.DeletedDate.HasValue);
    }
}