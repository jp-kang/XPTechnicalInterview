using System.ComponentModel.DataAnnotations;

namespace XPTechnicalInterview.Domain
{
    public class Client
    {
        [Key]
        public long ClientId { get; set; }
        public string Name { get; set; }

    }
}
