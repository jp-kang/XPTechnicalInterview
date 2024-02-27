using Microsoft.AspNetCore.Mvc;
using XPTechnicalInterview.Domain;
using XPTechnicalInterview.DTO;
using XPTechnicalInterview.Services;

namespace XPTechnicalInterview.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientService clientService;
        public ClientController(ClientService _ClientRepository)
        {
            clientService = _ClientRepository;
        }

        // GET: api/clients
        [HttpGet]
        public IActionResult GetClients()
        {
            var clients = clientService.GetClients();
            return Ok(clients); //200
        }

        // GET: api/clients/{id}
        [HttpGet("{id}")]
        public IActionResult GetClientById(int id)
        {
            var client = clientService.GetClientById(id);
            if (client == null)
            {
                return NotFound(); //404
            }
            return Ok(client); //200
        }

        // POST: api/clients
        [HttpPost]
        public IActionResult CreateClient([FromBody] ClientDTO clientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400
            }

            var createdClient = clientService.CreateClient(clientDto);
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

            var updated = clientService.UpdateClient(client);
            return Ok(updated); //200
        }

        // DELETE: api/clients/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            clientService.DeleteClient(id);
            return Ok();//200
        }
    }
}
