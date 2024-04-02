using System.ComponentModel.DataAnnotations;

namespace cltxmomo.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public DateTime DateTime { get; set; }
    }
}
