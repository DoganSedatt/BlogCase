using Application.Features.BlogPosts.Constants;
using Application.Features.BlogPosts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.BlogPosts.Commands.Delete;

public class DeleteBlogPostCommand : IRequest<DeletedBlogPostResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBlogPosts"];

    public class DeleteBlogPostCommandHandler : IRequestHandler<DeleteBlogPostCommand, DeletedBlogPostResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly BlogPostBusinessRules _blogPostBusinessRules;

        public DeleteBlogPostCommandHandler(IMapper mapper, IBlogPostRepository blogPostRepository,
                                         BlogPostBusinessRules blogPostBusinessRules)
        {
            _mapper = mapper;
            _blogPostRepository = blogPostRepository;
            _blogPostBusinessRules = blogPostBusinessRules;
        }

        public async Task<DeletedBlogPostResponse> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
        {
            BlogPost? blogPost = await _blogPostRepository.GetAsync(predicate: bp => bp.Id == request.Id, cancellationToken: cancellationToken);
            await _blogPostBusinessRules.BlogPostShouldExistWhenSelected(blogPost);

            await _blogPostRepository.DeleteAsync(blogPost!);

            DeletedBlogPostResponse response = _mapper.Map<DeletedBlogPostResponse>(blogPost);
            return response;
        }
    }
}