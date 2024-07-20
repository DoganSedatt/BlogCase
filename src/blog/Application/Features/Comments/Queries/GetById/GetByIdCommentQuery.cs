using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Comments.Queries.GetById;

public class GetByIdCommentQuery : IRequest<GetByIdCommentResponse>
{
    public Guid Id { get; set; }

    public class GetByIdCommentQueryHandler : IRequestHandler<GetByIdCommentQuery, GetByIdCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        private readonly CommentBusinessRules _commentBusinessRules;

        public GetByIdCommentQueryHandler(IMapper mapper, ICommentRepository commentRepository, CommentBusinessRules commentBusinessRules)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
            _commentBusinessRules = commentBusinessRules;
        }

        public async Task<GetByIdCommentResponse> Handle(GetByIdCommentQuery request, CancellationToken cancellationToken)
        {
            Comment? comment = await _commentRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _commentBusinessRules.CommentShouldExistWhenSelected(comment);

            GetByIdCommentResponse response = _mapper.Map<GetByIdCommentResponse>(comment);
            return response;
        }
    }
}