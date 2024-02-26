using XPTechnicalInterview.Domain;
using XPTechnicalInterview.Interfaces;
using XPTechnicalInterview.Repositories;

namespace XPTechnicalInterview.Services
{
    public class InvestmentService
    {
        private readonly InvestmentRepository _InvestmentRepository;
        private readonly FinancialProductRepository _FinancialProductRepository; 
        private readonly ClientRepository _ClientRepository;
        public InvestmentService(InvestmentRepository InvestmentRepository, FinancialProductRepository FinancialProductRepository, ClientRepository ClientRepository)
        {
            _InvestmentRepository = InvestmentRepository;
            _FinancialProductRepository = FinancialProductRepository;
            _ClientRepository = ClientRepository;
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
                    PurchasePrice = _FinancialProductRepository.GetById(order.ProductId).Price,
                    Status = "Active"
                };
                return _InvestmentRepository.Create(investment);
            }
        }

        public Investment handleSellOrder(SellOrder order)
        {
            var investment = _InvestmentRepository.GetById(order.InvestmentId);
            if (investment != null)
            {
                investment.SellDate = order.SellDate;
                investment.SellPrice = _FinancialProductRepository.GetById(investment.FinancialProductId).Price;
                investment.Status = "Sold";
            } 
            else
            {
                throw new KeyNotFoundException("Investment not found");
            }
            return _InvestmentRepository.Update(investment);
        }

        private void validateClientAndProduct(long clientId, long productId)
        {
            var client = _ClientRepository.GetById(clientId);
            var product = _FinancialProductRepository.GetById(productId);
            if (client != null || product != null) {
                throw new InvalidOperationException("User or product not found!");
            }
        }
    }
}
