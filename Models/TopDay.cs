using System.ComponentModel.DataAnnotations;

namespace cltxmomo.Models
{
    public class TopDay
    {
        [Key]
        public int Id { get; set; }
        public int Top { get; set; }
        public string PhoneNumber { get; set; }
        public string Money { get; set; }
        public string Reward { get; set; }
    }
}
