using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyStore.DTO.Membership;
using MyStore.Interfaces;
using MyStore.Models;
using MyStore.Repository;

namespace MyStore.Controllers
{
    [Authorize]
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

        [HttpPut("{id}")]
        public IActionResult UpdateMembership(int id, [FromBody]  MembershipUpdateDto membership)
        {
            var membershipToModify = _membershipRepository.GetMembershipById(id);
            if (membershipToModify == null)
            {
                return NotFound($"Membership with Id {id} doesn't exist");
            }

            if (membership == null)
            {
                return BadRequest("Mmebership is null");
            }

            _mapper.Map(membership, membershipToModify);
            var membershipUpdated = _membershipRepository.UpdateMembership(membershipToModify);
            return Ok($"Mmebership with Id {id} was succesfully updated!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMembership(int id)
        {
            var membershipToDelete = _membershipRepository.GetMembershipById(id);
            if (membershipToDelete == null)
            {
                return NotFound($"Mmebership with Id {id} doesn't exist");
            }

            var membershipDeleted = _membershipRepository.DeleteMembership(membershipToDelete);
            return NoContent();
        }
    }
}
