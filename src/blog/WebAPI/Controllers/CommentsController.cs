using Application.Features.Comments.Commands.Create;
using Application.Features.Comments.Commands.Delete;
using Application.Features.Comments.Commands.Update;
using Application.Features.Comments.Queries.GetById;
using Application.Features.Comments.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedCommentResponse>> Add([FromBody] CreateCommentCommand command)
    {
        CreatedCommentResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedCommentResponse>> Update([FromBody] UpdateCommentCommand command)
    {
        UpdatedCommentResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedCommentResponse>> Delete([FromRoute] Guid id)
    {
        DeleteCommentCommand command = new() { Id = id };

        DeletedCommentResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdCommentResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdCommentQuery query = new() { Id = id };

        GetByIdCommentResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListCommentQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCommentQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListCommentListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}