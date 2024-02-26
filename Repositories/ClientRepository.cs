using Microsoft.EntityFrameworkCore;
using XPTechnicalInterview.Domain;
using XPTechnicalInterview.Entity;
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
            return _context.Clients.ToList(); // Replace with your specific query if needed
        }

        public Client GetById(long id)
        {
            return _context.Clients.Find(id);
        }

        public Client Create(Client entity)
        {
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
            return update;
        }

        public void Delete(long id)
        {
            var clientToDelete = _context.Clients.Find(id);
            if (clientToDelete != null)
            {
                _context.Clients.Remove(clientToDelete);
                _context.SaveChanges();
            }
        }
    }
}
