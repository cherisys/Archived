using Api.Global;

namespace Api.Models
{
    public class Participant: EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
        public int TimeSpent { get; set; }
    }
}
