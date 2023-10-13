using System.Security.Cryptography.X509Certificates;

namespace GalaxyWave_Dashboard.Api.Models.UsersDashboards.Elements
{
    public class Payment
    {
        public User User { get; set; }
        public Guid PaymentId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public int CardNumber { get; set; }
        public string CardName { get; set; }
        public int CVV { get; set; }
        public string CardType { get; set; }
        public DateTime CardEXP { get; set; }

    }
}
