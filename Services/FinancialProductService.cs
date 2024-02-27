using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using XPTechnicalInterview.Domain;
using XPTechnicalInterview.DTO;
using XPTechnicalInterview.Repositories;

namespace XPTechnicalInterview.Services
{
    public class FinancialProductService
    {
        private readonly FinancialProductRepository financialProductRepository;
        private readonly InvestmentRepository investmentRepository;
        public FinancialProductService(FinancialProductRepository _FinancialProductRepository, InvestmentRepository _InvestmentRepository)
        {
            financialProductRepository = _FinancialProductRepository;
            investmentRepository = _InvestmentRepository;
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

        public FinancialProduct CreateFinancialProduct(FinancialProductDTO financialProductDto)
        {
            var financialProduct = new FinancialProduct
            {
                Name = financialProductDto.Name,
                Description = financialProductDto.Description,
                Price = financialProductDto.Price,
                DueDate = financialProductDto.DueDate
            };
            var createdFinancialProduct = financialProductRepository.Create(financialProduct);
            return createdFinancialProduct; 
        }

        public FinancialProduct UpdateFinancialProduct(FinancialProduct FinancialProduct)
        {
            var updated = financialProductRepository.Update(FinancialProduct);
            return updated;
        }

        public void DeleteFinancialProduct(int id)
        {
            var investments = investmentRepository.GetActiveByProductId(id);
            if (investments.Count() > 0)
            {
                throw new InvalidOperationException("This product has active investments, please sell them before deleting the product.");
            }
            financialProductRepository.Delete(id);
        }
    }
}
