using XPTechnicalInterview.Domain;
using XPTechnicalInterview.DTO;
using XPTechnicalInterview.Repositories;

namespace XPTechnicalInterview.Services
{
    public class ClientService
    {
        private readonly ClientRepository clientRepository;
        public ClientService(ClientRepository _ClientRepository)
        {
            clientRepository = _ClientRepository;
        }

        public IEnumerable<Client> GetClients()
        {
            var clients = clientRepository.ListAll();
            return clients;
        }

        public Client GetClientById(int id)
        {
            var client = clientRepository.GetById(id);
            return client;
        }

        public Client CreateClient(ClientDTO clientDto)
        {
            var client = new Client
            {
                Name = clientDto.Name,
            };
            var createdClient = clientRepository.Create(client);
            return createdClient;
        }

        public Client UpdateClient(Client client)
        {
            var updated = clientRepository.Update(client);
            return updated;
        }

        public void DeleteClient(int id)
        {
            clientRepository.Delete(id);
        }
    }
}
