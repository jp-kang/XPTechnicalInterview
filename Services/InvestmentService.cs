using Microsoft.AspNetCore.Mvc;
using XPTechnicalInterview.Domain;
using XPTechnicalInterview.Interfaces;
using XPTechnicalInterview.Repositories;

namespace XPTechnicalInterview.Services
{
    public class InvestmentService
    {
        private readonly InvestmentRepository investmentRepository;
        private readonly FinancialProductRepository financialProductRepository; 
        private readonly ClientRepository clientRepository;
        public InvestmentService(InvestmentRepository _InvestmentRepository, FinancialProductRepository _FinancialProductRepository, ClientRepository _ClientRepository)
        {
            investmentRepository = _InvestmentRepository;
            financialProductRepository = _FinancialProductRepository;
            clientRepository = _ClientRepository;
        }
        public Investment handleBuyOrder(BuyOrder order)
        {
            validateClientAndProduct(order.ClientId, order.ProductId);
            {
                var investment = new Investment
                {
                    ClientId = order.ClientId,
                    FinancialProductId = order.ProductId,
                    PurchaseDate = order.PurchaseDate,
                    PurchasePrice = financialProductRepository.GetById(order.ProductId).Price,
                    Status = "Active"
                };
                return investmentRepository.Create(investment);
            }
        }

        public Investment handleSellOrder(SellOrder order)
        {
            var investment = investmentRepository.GetById(order.InvestmentId);
            if (investment != null)
            {
                investment.SellDate = order.SellDate;
                investment.SellPrice = financialProductRepository.GetById(investment.FinancialProductId).Price;
                investment.Status = "Sold";
            } 
            else
            {
                throw new KeyNotFoundException("Investment not found");
            }
            return investmentRepository.Update(investment);
        }

        public Investment GetInvestmentById(int id)
        {
            var investment = investmentRepository.GetById(id);
            return investment; 
        }

        public IEnumerable<Investment> GetInvestmentsByClientId(long clientId)
        {
            var investments = investmentRepository.GetByClientId(clientId);
            return investments;
        }

        public IEnumerable<Investment> GetActiveInvestmentsByClientId(long clientId)
        {
            var investments = investmentRepository.GetActiveByClientId(clientId);
            return investments;
        }

        public IEnumerable<Investment> GetSoldInvestmentsByClientId(long clientId)
        {
            var investments = investmentRepository.GetSoldByClientId(clientId);
            return investments;
        }

        public IEnumerable<Investment> GetInvestmentsByProductId(long productId)
        {
            var investments = investmentRepository.GetByProductId(productId);//200
            return investments;
        }

        private void validateClientAndProduct(long clientId, long productId)
        {
            var client = clientRepository.GetById(clientId);
            var product = financialProductRepository.GetById(productId);
            if ((client == null || client.Status == "DELETED") || (product == null || product.Status == "DELETED")) {
                throw new InvalidOperationException("User or product not found!");
            }
        }
    }
}
