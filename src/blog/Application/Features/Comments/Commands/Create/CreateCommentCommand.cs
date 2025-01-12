using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Comments.Commands.Create;

public class CreateCommentCommand : IRequest<CreatedCommentResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required string Commenter { get; set; }
    public required string Content { get; set; }
    public required Guid BlogPostId { get; set; }
    public required Guid MemberId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetComments"];

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CreatedCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        private readonly CommentBusinessRules _commentBusinessRules;

        public CreateCommentCommandHandler(IMapper mapper, ICommentRepository commentRepository,
                                         CommentBusinessRules commentBusinessRules)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
            _commentBusinessRules = commentBusinessRules;
        }

        public async Task<CreatedCommentResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            Comment comment = _mapper.Map<Comment>(request);

            await _commentRepository.AddAsync(comment);

            CreatedCommentResponse response = _mapper.Map<CreatedCommentResponse>(comment);
            return response;
        }
    }
}