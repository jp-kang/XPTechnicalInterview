using XPTechnicalInterview.Domain;
using XPTechnicalInterview.Entity;
using XPTechnicalInterview.Interfaces;

namespace XPTechnicalInterview.Repositories
{
    public class FinancialProductRepository : IRepository<FinancialProduct>
    {
        private readonly PortfolioContext _context; // Replace with your actual DbContext instance

        public FinancialProductRepository(PortfolioContext context)
        {
            _context = context;
        }

        public IEnumerable<FinancialProduct> ListAll()
        {
            return _context.FinancialProducts.ToList(); // Replace with your specific query if needed
        }

        public FinancialProduct GetById(long id)
        {
            return _context.FinancialProducts.Find(id);
        }

        public FinancialProduct Create(FinancialProduct entity)
        {
            _context.FinancialProducts.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public FinancialProduct Update(FinancialProduct entity)
        {
            var update = _context.FinancialProducts.FirstOrDefault(x => x.FinancialProductId == entity.FinancialProductId);
            if (update != null)
            {
                update.Name = entity.Name;
                update.Description = entity.Description;
                update.Price = entity.Price;
                update.DueDate = entity.DueDate;

                _context.SaveChanges();
            }
            return entity;

        }

        public void Delete(long id)
        {
            var FinancialProductToDelete = _context.FinancialProducts.Find(id);
            if (FinancialProductToDelete != null)
            {
                _context.FinancialProducts.Remove(FinancialProductToDelete);
                _context.SaveChanges();
            }
        }
    }
}
