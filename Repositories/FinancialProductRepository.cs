using XPTechnicalInterview.Domain;
using XPTechnicalInterview.Entity;
using XPTechnicalInterview.Exceptions;
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
            return _context.FinancialProducts.Where(x => x.Status == "ACTIVE").ToList(); // Replace with your specific query if needed
        }

        public IEnumerable<FinancialProduct> ListByExpirationDate(int days)
        {
            return _context.FinancialProducts.Where(x => x.DueDate <= DateTime.Now.AddDays(days) && x.DueDate >= DateTime.Now && x.Status == "ACTIVE").ToList();
        }

        public FinancialProduct GetById(long id)
        {
            var product = _context.FinancialProducts.Find(id);
            if (product == null)
            {
                throw new RecordNotFoundException($"Financial Product {id} not found.");
            }
            return product;
        }

        public FinancialProduct Create(FinancialProduct entity)
        {
            entity.Status = "ACTIVE";
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
            else
            {
                throw new RecordNotFoundException($"Financial Product {entity.FinancialProductId} not found.");
            }
            return entity;
        }

        public void Delete(long id)
        {
            var delete = _context.FinancialProducts.FirstOrDefault(x => x.FinancialProductId == id);
            if (delete != null)
            {
                delete.Status = "DELETED";

                _context.SaveChanges();
            }
            else
            {
                throw new RecordNotFoundException($"Financial Product {id} not found.");
            }
        }
    }
}
