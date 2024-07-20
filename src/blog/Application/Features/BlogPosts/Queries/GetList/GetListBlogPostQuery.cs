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
                //Tarihi baz alarak yeniden eskiye g�re do�ru s�ralama yapar.Varsay�lan da bu.
                //Aksi durum i�in blog.OrderBy(b=>b.CreatedDate)
                //Dinamik de�i�tirmeyi ara�t�r
                ,
                //include k�sm�nda �unu diyoruz asl�nda.Bana blog yaz�lar�n�n hepsini tek tek getir.her blog yaz�s�na blog ad�nda bir takma ad veriyorum. Bunlar�n bilgisini getirirken i�ine dahil et,include et diyorum. Neyi dahil et? b'nin i�indekii yani veritaban�ndan �ekti�i her blog yaz�s� i�in onun i�inde yer alan navigation prop ile tutulan Comments bilgisini �ek diyorum. Yani bu da demek oluyorki her blog'un ka� yorumu varsa onu da sorguya dahil et. Navigation olmadan sorguya ba�ka tablodan veri �ekemem 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListBlogPostListItemDto> response = _mapper.Map<GetListResponse<GetListBlogPostListItemDto>>(blogPosts);
            return response;
        }
    }
}