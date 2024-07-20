using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.BlogPosts.Queries.GetList;

public class GetListBlogPostListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid MemberId { get; set; }
    public string MemberName { get; set; }
    public List<string> Comments { get; set; }
    public DateTime CreatedDate { get; set; }
}