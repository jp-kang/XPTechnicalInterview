using Microsoft.EntityFrameworkCore;
using XPTechnicalInterview.Domain;
using XPTechnicalInterview.Entity;
using XPTechnicalInterview.Exceptions;
using XPTechnicalInterview.Interfaces;

namespace XPTechnicalInterview.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly PortfolioContext _context; // Replace with your actual DbContext instance

        public ClientRepository(PortfolioContext context)
        {
            _context = context;
        }

        public IEnumerable<Client> ListAll()
        {
            return _context.Clients.Where(x => x.Status == "ACTIVE").ToList(); // Replace with your specific query if needed
        }

        public Client GetById(long id)
        {
            var client = _context.Clients.Find(id);
            if(client == null)
            {
                throw new RecordNotFoundException($"Client {id} not found.");
                
            }
            return client;
        }

        public Client Create(Client entity)
        {
            entity.Status = "ACTIVE";
            _context.Clients.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Client Update(Client entity)
        {
            var update = _context.Clients.FirstOrDefault(x => x.ClientId == entity.ClientId);
            if (update != null)
            {
                update.Name = entity.Name;

                _context.SaveChanges();
            }
            else
            {
                throw new RecordNotFoundException($"Client {entity.ClientId} not found.");
            }
            return update;
        }

        public void Delete(long id)
        {
            var delete = _context.Clients.FirstOrDefault(x => x.ClientId == id);
            if (delete != null)
            {
                delete.Status = "DELETED";

                _context.SaveChanges();
            }
            else
            {
                throw new RecordNotFoundException($"Client {id} not found.");
            }
        }
    }
}
