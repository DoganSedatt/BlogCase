using Application.Features.Members.Queries.GetByMail;
using Application.Features.Members.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Members.Queries.GetByEmail;

public class GetByEmailMemberQuery : IRequest<GetByEmailMemberResponse>
{
    public string Email { get; set; }

    public class GetByEmailMemberQueryHandler : IRequestHandler<GetByEmailMemberQuery, GetByEmailMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly MemberBusinessRules _memberBusinessRules;

        public GetByEmailMemberQueryHandler(IMapper mapper, IMemberRepository memberRepository, MemberBusinessRules memberBusinessRules)
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
            _memberBusinessRules = memberBusinessRules;
        }

        public async Task<GetByEmailMemberResponse> Handle(GetByEmailMemberQuery request, CancellationToken cancellationToken)
        {
            Member? member = await _memberRepository.GetAsync(predicate: m => m.User.Email == request.Email, cancellationToken: cancellationToken);
            await _memberBusinessRules.MemberShouldExistWhenSelected(member);

            GetByEmailMemberResponse response = _mapper.Map<GetByEmailMemberResponse>(member);
            return response;
        }
    }
}
