using System.ComponentModel.DataAnnotations;

namespace cltxmomo.Models
{
    public class RechargePhoneNumber
    {
        [Key]
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
    }
}
