using Microsoft.AspNetCore.Mvc;
using XPTechnicalInterview.Domain;
using XPTechnicalInterview.DTO;
using XPTechnicalInterview.Exceptions;
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
        [ProducesResponseType(typeof(List<Client>), StatusCodes.Status200OK)] // Expected response type for success
        public ActionResult<List<Client>> GetClients()
        {
            var clients = clientService.GetClients();
            return Ok(clients); //200
        }

        // GET: api/clients/{id}
        [HttpGet("{id}")]
        public ActionResult<Client> GetClientById(int id)
        {
            try
            {
                var client = clientService.GetClientById(id);
                return Ok(client);
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message); // Set 404 status code
            }
        }

        // POST: api/clients
        [HttpPost]
        [Consumes("application/json")] // Specify the consumed content type
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)] // Expected response type for success
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Expected response for bad request
        public ActionResult<Client> CreateClient([FromBody] ClientDTO clientDto)
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
        [Consumes("application/json")] // Specify the consumed content type
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)] // Expected response type for success
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Expected response for bad request
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
        [ProducesResponseType(StatusCodes.Status200OK)] // Expected response type for success
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Expected response for bad request
        public IActionResult DeleteClient(int id)
        {
            try
            {
                clientService.DeleteClient(id);
                return Ok();
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message); // Set 404 status code
            }
        }
    }
}
