using System.ComponentModel.DataAnnotations;

namespace EmailService.Contracts
{
    public class SmtpOptions
    {
        [Required]
        public string EmailServiceProvider { get; set; } = null!;
        [Required]
        public string EmailFromAddress { get; set; } = null!;
        [Required]
        public Smtp Smtp { get; set; } = null!;
    }

    public class Smtp
    {
        [Required]
        public string Host { get; set; } = null!;
        [Required]
        public int Port { get; set; }
        public bool UseSsl { get; set; } = false;
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        public string DeliveryMethod { get; set; } = null!;
        public string PickupDirectoryLocation { get; set; } = null!;
    }
}
