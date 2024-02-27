using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace XPTechnicalInterview.Domain
{
    public class Client
    {
        [Key]
        public long ClientId { get; set; }
        public string Name { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public string? Status { get; set; }
    }
}
