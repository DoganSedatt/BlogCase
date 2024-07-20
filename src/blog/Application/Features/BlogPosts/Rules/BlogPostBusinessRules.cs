using Application.Features.BlogPosts.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.BlogPosts.Rules;

public class BlogPostBusinessRules : BaseBusinessRules
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ILocalizationService _localizationService;

    public BlogPostBusinessRules(IBlogPostRepository blogPostRepository, ILocalizationService localizationService)
    {
        _blogPostRepository = blogPostRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, BlogPostsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task BlogPostShouldExistWhenSelected(BlogPost? blogPost)
    {
        if (blogPost == null)
            await throwBusinessException(BlogPostsBusinessMessages.BlogPostNotExists);
    }

    public async Task BlogPostIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        BlogPost? blogPost = await _blogPostRepository.GetAsync(
            predicate: bp => bp.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await BlogPostShouldExistWhenSelected(blogPost);
    }
}