using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace XPTechnicalInterview.Domain
{
    public class FinancialProduct
    {
        [Key]
        public long FinancialProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public DateTime DueDate { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public string? Status { get; set; }
    }
}