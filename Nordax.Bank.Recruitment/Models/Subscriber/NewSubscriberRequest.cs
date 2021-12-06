using System.ComponentModel.DataAnnotations;

namespace Nordax.Bank.Recruitment.Models.Subscriber
{
    public class NewSubscriberRequest
    {
        [Required] public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Not a valid email")]
        public string Email { get; set; }
    }
}