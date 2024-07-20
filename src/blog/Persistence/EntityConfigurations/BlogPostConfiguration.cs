using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.ToTable("BlogPosts").HasKey(bp => bp.Id);

        builder.Property(bp => bp.Id).HasColumnName("Id").IsRequired();
        builder.Property(bp => bp.Title).HasColumnName("Title").IsRequired();
        builder.Property(bp => bp.Content).HasColumnName("Content").IsRequired();
        builder.Property(bp => bp.MemberId).HasColumnName("MemberId").IsRequired();
        builder.Property(bp => bp.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(bp => bp.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(bp => bp.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(bp => !bp.DeletedDate.HasValue);
    }
}