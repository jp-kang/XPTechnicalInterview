using XPTechnicalInterview.Domain;
using XPTechnicalInterview.Entity;
using XPTechnicalInterview.Interfaces;

namespace XPTechnicalInterview.Repositories
{
    public class InvestmentRepository : IRepository<Investment>
    {
        private readonly PortfolioContext _context; // Replace with your actual DbContext instance

        public InvestmentRepository(PortfolioContext context)
        {
            _context = context;
        }

        public IEnumerable<Investment> ListAll()
        {
            return _context.Investments.ToList(); // Replace with your specific query if needed
        }

        public Investment GetById(long id)
        {
            return _context.Investments.Find(id);
        }

        public IEnumerable<Investment> GetByClientId(long id)
        {
            return _context.Investments.Where(x => x.ClientId == id).ToList();
        }

        public IEnumerable<Investment> GetActiveByClientId(long id)
        {
            return _context.Investments.Where(x => x.ClientId == id && x.Status == "Active").ToList();
        }

        public IEnumerable<Investment> GetSoldByClientId(long id)
        {
            return _context.Investments.Where(x => x.ClientId == id && x.Status == "Sold").ToList();
        }

        public IEnumerable<Investment> GetByProductId(long id)
        {
            return _context.Investments.Where(x => x.FinancialProductId == id).ToList();
        }

        public Investment Create(Investment entity)
        {
            _context.Investments.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Investment Update(Investment entity)
        {
            var update = _context.Investments.FirstOrDefault(x => x.Id == entity.Id);
            if (update != null)
            {
                update.Status = entity.Status;
                update.SellDate = entity.SellDate;
                update.SellPrice = entity.SellPrice;
                update.PurchaseDate = entity.PurchaseDate;
                update.PurchasePrice = entity.PurchasePrice;

                _context.SaveChanges();
            }
            return entity;
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
