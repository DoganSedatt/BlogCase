using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Comments.Queries.GetList;

public class GetListCommentListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Commenter { get; set; }
    public string Content { get; set; }
    public Guid BlogPostId { get; set; }
    public Guid MemberId { get; set; }
}