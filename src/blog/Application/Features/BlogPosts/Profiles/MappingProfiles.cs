using Application.Features.BlogPosts.Commands.Create;
using Application.Features.BlogPosts.Commands.Delete;
using Application.Features.BlogPosts.Commands.Update;
using Application.Features.BlogPosts.Queries.GetById;
using Application.Features.BlogPosts.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.Members.Queries.GetList;
using Application.Features.BlogPosts.Queries.GetBlogByMemberId;

namespace Application.Features.BlogPosts.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateBlogPostCommand, BlogPost>();
        CreateMap<BlogPost, CreatedBlogPostResponse>();

        CreateMap<UpdateBlogPostCommand, BlogPost>();
        CreateMap<BlogPost, UpdatedBlogPostResponse>();

        CreateMap<DeleteBlogPostCommand, BlogPost>();
        CreateMap<BlogPost, DeletedBlogPostResponse>();

        CreateMap<BlogPost, GetByIdBlogPostResponse>();

        CreateMap<BlogPost, GetListBlogPostListItemDto>();
        CreateMap<IPaginate<BlogPost>, GetListResponse<GetListBlogPostListItemDto>>();

        

        CreateMap<BlogPost, GetListBlogPostListItemDto>()
            .ForMember(dest => dest.Comments, src => src.MapFrom(c => c.Comments.Select(
                c=>c.Content
                )))
            .ForMember(dest=>dest.MemberName,src=>src.MapFrom(b=>b.Member.FirstName));

        CreateMap<BlogPost, GetByIdBlogPostResponse>()
            .ForMember(dest => dest.Comments, src => src.MapFrom(c => c.Comments.Select(
                c => c.Content
                )))
            .ForMember(dest => dest.MemberName, src => src.MapFrom(b => b.Member.FirstName));
    }
}