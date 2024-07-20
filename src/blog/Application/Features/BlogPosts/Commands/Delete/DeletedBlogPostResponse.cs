using NArchitecture.Core.Application.Responses;

namespace Application.Features.BlogPosts.Commands.Delete;

public class DeletedBlogPostResponse : IResponse
{
    public Guid Id { get; set; }
}