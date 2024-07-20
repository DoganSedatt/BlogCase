using Application.Features.BlogPosts.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.BlogPosts.Queries.GetBlogByMemberId
{
    public class GetListBlogByMemberIdQuery : IRequest<GetListResponse<GetListBlogPostListItemDto>>
    {
        public PageRequest PageRequest { get; set; }
        public Guid MemberId { get; set; }
        public bool BypassCache { get; }
        public string? CacheKey => $"GetListBlogs({PageRequest.PageIndex},{PageRequest.PageSize})";
        public string? CacheGroupKey => "GetBlogs";
        public TimeSpan? SlidingExpiration { get; }

        public class GetListBlogByMemberIdQueryHandler : IRequestHandler<GetListBlogByMemberIdQuery, GetListResponse<GetListBlogPostListItemDto>>
        {
            private readonly IBlogPostRepository _blogpostRepository;
            private readonly IMapper _mapper;

            public GetListBlogByMemberIdQueryHandler(IBlogPostRepository blogpostRepository, IMapper mapper)
            {
                _blogpostRepository = blogpostRepository;
                _mapper = mapper;
            }
            public async Task<GetListResponse<GetListBlogPostListItemDto>> Handle(
                GetListBlogByMemberIdQuery request,
                CancellationToken cancellationToken)
            {
                IPaginate<BlogPost> blogposts = await _blogpostRepository.GetListAsync(
                    predicate: x => x.MemberId == request.MemberId,
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken,
                    include: p => p.Include(p => p.Comments)
                    .Include(b=>b.Member)
                );
                GetListResponse<GetListBlogPostListItemDto> response = _mapper.Map<GetListResponse<GetListBlogPostListItemDto>>(blogposts);
                return response;
            }
        }
    }
}
