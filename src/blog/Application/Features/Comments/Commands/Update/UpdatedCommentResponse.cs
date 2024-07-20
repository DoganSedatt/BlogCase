using NArchitecture.Core.Application.Responses;

namespace Application.Features.Comments.Commands.Update;

public class UpdatedCommentResponse : IResponse
{
    public Guid Id { get; set; }
    public string Commenter { get; set; }
    public string Content { get; set; }
    public Guid BlogPostId { get; set; }
    public Guid MemberId { get; set; }
}