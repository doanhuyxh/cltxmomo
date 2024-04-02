using System.ComponentModel.DataAnnotations;

namespace cltxmomo.Models
{
    public class WebConfig
    {
        [Key] public int Id { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
    }
}
