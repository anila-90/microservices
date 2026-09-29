using System.ComponentModel.DataAnnotations;

namespace platformservice.viewmodels
{
    public class PlatformRequest
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Publisher { get; set; }
    }
}