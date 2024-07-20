using NArchitecture.Core.Application.Responses;

namespace Application.Features.BlogPosts.Commands.Create;

public class CreatedBlogPostResponse : IResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid MemberId { get; set; }
}