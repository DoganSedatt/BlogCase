using NArchitecture.Core.Application.Responses;

namespace Application.Features.Comments.Queries.GetById;

public class GetByIdCommentResponse : IResponse
{
    public Guid Id { get; set; }
    public string Commenter { get; set; }
    public string Content { get; set; }
    public Guid BlogPostId { get; set; }
    public Guid MemberId { get; set; }
}