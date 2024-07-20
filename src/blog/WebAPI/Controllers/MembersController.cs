using Application.Features.Members.Commands.Create;
using Application.Features.Members.Commands.Delete;
using Application.Features.Members.Commands.Update;
using Application.Features.Members.Queries.GetById;
using Application.Features.Members.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Members.Queries.GetByEmail;
using Application.Features.Members.Queries.GetByMail;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembersController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedMemberResponse>> Add([FromBody] CreateMemberCommand command)
    {
        CreatedMemberResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedMemberResponse>> Update([FromBody] UpdateMemberCommand command)
    {
        UpdatedMemberResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedMemberResponse>> Delete([FromRoute] Guid id)
    {
        DeleteMemberCommand command = new() { Id = id };

        DeletedMemberResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdMemberResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdMemberQuery query = new() { Id = id };

        GetByIdMemberResponse response = await Mediator.Send(query);

        return Ok(response);
    }
    [HttpGet("email/{email}")]
    public async Task<ActionResult<GetByEmailMemberQuery>> GetByMail([FromRoute] string email)
    {
        GetByEmailMemberQuery query = new() {  Email= email };

        GetByEmailMemberResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListMemberQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListMemberQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListMemberListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}