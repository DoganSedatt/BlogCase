using Application.Features.Comments.Commands.Create;
using Application.Features.Comments.Commands.Delete;
using Application.Features.Comments.Commands.Update;
using Application.Features.Comments.Queries.GetById;
using Application.Features.Comments.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Comments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateCommentCommand, Comment>();
        CreateMap<Comment, CreatedCommentResponse>();

        CreateMap<UpdateCommentCommand, Comment>();
        CreateMap<Comment, UpdatedCommentResponse>();

        CreateMap<DeleteCommentCommand, Comment>();
        CreateMap<Comment, DeletedCommentResponse>();

        CreateMap<Comment, GetByIdCommentResponse>();

        CreateMap<Comment, GetListCommentListItemDto>();
        CreateMap<IPaginate<Comment>, GetListResponse<GetListCommentListItemDto>>();
    }
}