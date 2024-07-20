using Application.Features.BlogPosts.Commands.Create;
using Application.Features.BlogPosts.Commands.Delete;
using Application.Features.BlogPosts.Commands.Update;
using Application.Features.BlogPosts.Queries.GetById;
using Application.Features.BlogPosts.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.Features.BlogPosts.Queries.GetBlogByMemberId;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogPostsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedBlogPostResponse>> Add([FromBody] CreateBlogPostCommand command)
    {
        CreatedBlogPostResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedBlogPostResponse>> Update([FromBody] UpdateBlogPostCommand command)
    {
        UpdatedBlogPostResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedBlogPostResponse>> Delete([FromRoute] Guid id)
    {
        DeleteBlogPostCommand command = new() { Id = id };

        DeletedBlogPostResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdBlogPostResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdBlogPostQuery query = new() { Id = id };

        GetByIdBlogPostResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListBlogPostQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBlogPostQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListBlogPostListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("member/{memberId}")]
    public async Task<IActionResult> GetByMemberId([FromRoute] Guid memberId, [FromQuery] PageRequest pageRequest)
    {
        var query = new GetListBlogByMemberIdQuery { MemberId = memberId, PageRequest = pageRequest };
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}