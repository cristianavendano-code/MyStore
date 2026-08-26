using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyStore.DTO.Client;
using MyStore.DTO.Membership;
using MyStore.DTO.Product;
using MyStore.Interfaces;
using MyStore.Models;
using MyStore.Repository;

namespace MyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientController(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetClients()
        {
            var clients = _clientRepository.GetClients();
            if (clients == null)
            {
                return NotFound("There's no membership added");
            }

            var clientsDto = _mapper.Map<List<ClientResponseDto>>(clients);
            return Ok(clientsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetClientById(int id)
        {
            var client = _clientRepository.GetClientById(id);
            if (client == null)
            {
                return NotFound($"Client with id {id} was not found.");
            }

            var clientDto = _mapper.Map<ClientResponseDto>(client);
            return Ok(clientDto);
        }

        [HttpPost]
        public IActionResult AddClient([FromBody] ClientCreateDto client)
        {
            if (client == null)
            {
                return BadRequest("Client is null");
            }
            var clientModel = _mapper.Map<Client>(client);

            var clientInDb = _clientRepository.GetClients()
                .Where(c => c.Phone == client.Phone)
                .FirstOrDefault();
            if (clientInDb != null)
            {
                return BadRequest("This client was added before!");
            }

            var newClient = _clientRepository.AddClient(clientModel);

            return Ok("Client added succesfully!");
        }

        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name)
        {
            var client = _clientRepository.GetClientByName(name);
            if (client == null)
            {
                return NotFound($"Client with name {name} was not found.");
            }

            var clientDto = _mapper.Map<ClientResponseDto>(client);
            return Ok(clientDto);
        }

        [HttpGet("phone/{phone}")]
        public IActionResult GetByPhone(string phone)
        {
            var client = _clientRepository.GetClientByPhone(phone);
            if (client == null)
            {
                return NotFound($"Client with phone {phone} was not found.");
            }

            var clientDto = _mapper.Map<ClientResponseDto>(client);
            return Ok(clientDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, [FromBody] ClientUpdateDto client)
        {
            var clientToModify = _clientRepository.GetClientById(id);
            if (clientToModify == null)
            {
                return NotFound($"Client with Id {id} doesn't exist");
            }

            if (client == null)
            {
                return BadRequest("Client is null");
            }

            _mapper.Map(client, clientToModify);
            var clientUpdated = _clientRepository.UpdateClient(clientToModify);
            return Ok($"Client with Id {id} was succesfully updated!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            var clientToDelete = _clientRepository.GetClientById(id);
            if (clientToDelete == null)
            {
                return NotFound($"Client with Id {id} doesn't exist");
            }

            var clientDeleted = _clientRepository.DeleteClient(clientToDelete);
            return NoContent();
        }
    }
}
