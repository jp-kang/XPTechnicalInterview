using Microsoft.AspNetCore.Mvc;
using XPTechnicalInterview.Domain;
using XPTechnicalInterview.Interfaces;
using XPTechnicalInterview.Repositories;

namespace XPTechnicalInterview.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientRepository _ClientRepository;
        public ClientController(ClientRepository ClientRepository)
        {
            _ClientRepository = ClientRepository;
        }

        // GET: api/clients
        [HttpGet]
        public IActionResult GetClients()
        {
            var clients = _ClientRepository.ListAll();
            return Ok(clients); //200
        }

        // GET: api/clients/{id}
        [HttpGet("{id}")]
        public IActionResult GetClientById(int id)
        {
            var client = _ClientRepository.GetById(id);
            if (client == null)
            {
                return NotFound(); //404
            }
            return Ok(client); //200
        }

        // POST: api/clients
        [HttpPost]
        public IActionResult CreateClient([FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400
            }

            var createdClient = _ClientRepository.Create(client);
            return CreatedAtRoute(new { id = createdClient.ClientId }, createdClient); //201
        }

        // PUT: api/clients
        [HttpPut]
        public IActionResult UpdateClient([FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400
            }

            var updated = _ClientRepository.Update(client);
            return Ok(updated); //200
        }

        // DELETE: api/clients/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            _ClientRepository.Delete(id);
            return Ok();//200
        }
    }
}
