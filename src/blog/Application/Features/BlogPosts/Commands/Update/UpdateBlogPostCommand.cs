using Application.Features.BlogPosts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.BlogPosts.Commands.Update;

public class UpdateBlogPostCommand : IRequest<UpdatedBlogPostResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBlogPosts"];

    public class UpdateBlogPostCommandHandler : IRequestHandler<UpdateBlogPostCommand, UpdatedBlogPostResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly BlogPostBusinessRules _blogPostBusinessRules;

        public UpdateBlogPostCommandHandler(IMapper mapper, IBlogPostRepository blogPostRepository,
                                         BlogPostBusinessRules blogPostBusinessRules)
        {
            _mapper = mapper;
            _blogPostRepository = blogPostRepository;
            _blogPostBusinessRules = blogPostBusinessRules;
        }

        public async Task<UpdatedBlogPostResponse> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
        {
            BlogPost? blogPost = await _blogPostRepository.GetAsync(predicate: bp => bp.Id == request.Id, cancellationToken: cancellationToken);
            await _blogPostBusinessRules.BlogPostShouldExistWhenSelected(blogPost);
            blogPost = _mapper.Map(request, blogPost);

            await _blogPostRepository.UpdateAsync(blogPost!);

            UpdatedBlogPostResponse response = _mapper.Map<UpdatedBlogPostResponse>(blogPost);
            return response;
        }
    }
}