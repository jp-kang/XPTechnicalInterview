using Microsoft.AspNetCore.Mvc;
using XPTechnicalInterview.Domain;
using XPTechnicalInterview.Repositories;

namespace XPTechnicalInterview.Services
{
    public class FinancialProductService
    {
        private readonly FinancialProductRepository financialProductRepository;
        public FinancialProductService(FinancialProductRepository _FinancialProductRepository)
        {
            financialProductRepository = _FinancialProductRepository;
        }

        public IEnumerable<FinancialProduct> GetFinancialProducts()
        {
            var financialProducts = financialProductRepository.ListAll();
            return financialProducts;
        }

        public IEnumerable<FinancialProduct> GetProductsNearExpiration(int days)
        {
            var financialProducts = financialProductRepository.ListByExpirationDate(days);
            return financialProducts;
        }

        public FinancialProduct GetFinancialProductById(int id)
        {
            var financialProduct = financialProductRepository.GetById(id);
            return financialProduct;
        }

        public FinancialProduct CreateFinancialProduct(FinancialProduct FinancialProduct)
        {
            var createdFinancialProduct = financialProductRepository.Create(FinancialProduct);
            return createdFinancialProduct; 
        }

        public FinancialProduct UpdateFinancialProduct(FinancialProduct FinancialProduct)
        {
            var updated = financialProductRepository.Update(FinancialProduct);
            return updated;
        }

        public void DeleteFinancialProduct(int id)
        {
            financialProductRepository.Delete(id);
        }
    }
}
