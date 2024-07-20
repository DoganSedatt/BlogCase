using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BlogPosts.Queries.GetList;

public class GetListBlogPostQuery : IRequest<GetListResponse<GetListBlogPostListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListBlogPosts({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetBlogPosts";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListBlogPostQueryHandler : IRequestHandler<GetListBlogPostQuery, GetListResponse<GetListBlogPostListItemDto>>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IMapper _mapper;

        public GetListBlogPostQueryHandler(IBlogPostRepository blogPostRepository, IMapper mapper)
        {
            _blogPostRepository = blogPostRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListBlogPostListItemDto>> Handle(GetListBlogPostQuery request, CancellationToken cancellationToken)
        {
            IPaginate<BlogPost> blogPosts = await _blogPostRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                include: blog => blog.Include(b => b.Comments)
                .Include(b => b.Member),
                orderBy: blog => blog.OrderByDescending(b => b.CreatedDate)
                //Tarihi baz alarak yeniden eskiye göre doðru sýralama yapar.Varsayýlan da bu.
                //Aksi durum için blog.OrderBy(b=>b.CreatedDate)
                //Dinamik deðiþtirmeyi araþtýr
                ,
                //include kýsmýnda þunu diyoruz aslýnda.Bana blog yazýlarýnýn hepsini tek tek getir.her blog yazýsýna blog adýnda bir takma ad veriyorum. Bunlarýn bilgisini getirirken içine dahil et,include et diyorum. Neyi dahil et? b'nin içindekii yani veritabanýndan çektiði her blog yazýsý için onun içinde yer alan navigation prop ile tutulan Comments bilgisini çek diyorum. Yani bu da demek oluyorki her blog'un kaç yorumu varsa onu da sorguya dahil et. Navigation olmadan sorguya baþka tablodan veri çekemem 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListBlogPostListItemDto> response = _mapper.Map<GetListResponse<GetListBlogPostListItemDto>>(blogPosts);
            return response;
        }
    }
}