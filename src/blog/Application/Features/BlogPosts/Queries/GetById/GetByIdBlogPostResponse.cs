using NArchitecture.Core.Application.Responses;

namespace Application.Features.BlogPosts.Queries.GetById;

public class GetByIdBlogPostResponse : IResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid MemberId { get; set; }
    public string MemberName { get; set; }
    public List<string> Comments { get; set; }
}