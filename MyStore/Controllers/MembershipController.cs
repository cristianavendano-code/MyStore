using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyStore.DTO.Membership;
using MyStore.Interfaces;
using MyStore.Models;

namespace MyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMapper _mapper;

        public MembershipController(IMembershipRepository membershipRepository, IMapper mapper)
        {
            _membershipRepository = membershipRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMemberships()
        {
            var memberships = _membershipRepository.GetMemberships();
            if (memberships == null)
            {
                return NotFound("There's no membership added");
            }

            var membershipsDto = _mapper.Map<List<MembershipResponseDto>>(memberships);
            return Ok(membershipsDto);
        }

        [HttpGet("{membershipId}")]
        public IActionResult GetMembership(int membershipId)
        {
            var membership = _membershipRepository.GetMembershipById(membershipId);
            if (membership == null)
            {
                return NotFound($"Membership with id {membershipId} was not found.");
            }
            var membershipDto = _mapper.Map<MembershipResponseDto>(membership);
            return Ok(membership);
        }

        [HttpPost]
        public IActionResult AddMembership([FromBody] MembershipCreateDto membership)
        {
            if (membership == null)
            {
                return BadRequest("Membership is null");
            }
            var membershipModel = _mapper.Map<Membership>(membership);

            var membershipInDb = _membershipRepository.GetMemberships()
                .Where(m => m.Description.ToUpper() == membership.Description.ToUpper())
                .FirstOrDefault();
            if (membershipInDb != null)
            {
                return BadRequest("This membership was added before!"); 
            }

            var newMembership = _membershipRepository.AddMembership(membershipModel);

            return Ok("Membership added succesfully!");
        }
    }
}
