using Application.Features.BlogPosts.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.BlogPosts;

public class BlogPostManager : IBlogPostService
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly BlogPostBusinessRules _blogPostBusinessRules;

    public BlogPostManager(IBlogPostRepository blogPostRepository, BlogPostBusinessRules blogPostBusinessRules)
    {
        _blogPostRepository = blogPostRepository;
        _blogPostBusinessRules = blogPostBusinessRules;
    }

    public async Task<BlogPost?> GetAsync(
        Expression<Func<BlogPost, bool>> predicate,
        Func<IQueryable<BlogPost>, IIncludableQueryable<BlogPost, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        BlogPost? blogPost = await _blogPostRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return blogPost;
    }

    public async Task<IPaginate<BlogPost>?> GetListAsync(
        Expression<Func<BlogPost, bool>>? predicate = null,
        Func<IQueryable<BlogPost>, IOrderedQueryable<BlogPost>>? orderBy = null,
        Func<IQueryable<BlogPost>, IIncludableQueryable<BlogPost, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<BlogPost> blogPostList = await _blogPostRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return blogPostList;
    }

    public async Task<BlogPost> AddAsync(BlogPost blogPost)
    {
        BlogPost addedBlogPost = await _blogPostRepository.AddAsync(blogPost);

        return addedBlogPost;
    }

    public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
    {
        BlogPost updatedBlogPost = await _blogPostRepository.UpdateAsync(blogPost);

        return updatedBlogPost;
    }

    public async Task<BlogPost> DeleteAsync(BlogPost blogPost, bool permanent = false)
    {
        BlogPost deletedBlogPost = await _blogPostRepository.DeleteAsync(blogPost);

        return deletedBlogPost;
    }
}
