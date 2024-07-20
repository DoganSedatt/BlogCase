using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Comments.Commands.Update;

public class UpdateCommentCommand : IRequest<UpdatedCommentResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required string Commenter { get; set; }
    public required string Content { get; set; }
    public required Guid BlogPostId { get; set; }
    public required Guid MemberId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetComments"];

    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, UpdatedCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        private readonly CommentBusinessRules _commentBusinessRules;

        public UpdateCommentCommandHandler(IMapper mapper, ICommentRepository commentRepository,
                                         CommentBusinessRules commentBusinessRules)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
            _commentBusinessRules = commentBusinessRules;
        }

        public async Task<UpdatedCommentResponse> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            Comment? comment = await _commentRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _commentBusinessRules.CommentShouldExistWhenSelected(comment);
            comment = _mapper.Map(request, comment);

            await _commentRepository.UpdateAsync(comment!);

            UpdatedCommentResponse response = _mapper.Map<UpdatedCommentResponse>(comment);
            return response;
        }
    }
}