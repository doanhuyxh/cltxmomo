using System.ComponentModel.DataAnnotations;

namespace cltxmomo.Models
{
    public class PromotionalCode
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public int CountUse { set; get; }
    }
}
