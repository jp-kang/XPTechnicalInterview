using System.ComponentModel.DataAnnotations.Schema;

namespace XPTechnicalInterview.Domain
{
    public class Investment
    {
        public long Id { get; set; }

        [ForeignKey("FinancialProduct")]
        public long FinancialProductId { get; set; }

        [ForeignKey("Client")]
        public long ClientId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public float PurchasePrice { get; set; }
        public DateTime? SellDate { get; set; }
        public float? SellPrice { get; set; }
        public string Status { get; set; }
    }
}
