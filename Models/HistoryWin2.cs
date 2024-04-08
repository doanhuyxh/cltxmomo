using System.ComponentModel.DataAnnotations;

namespace cltxmomo.Models
{
    public class HistoryWin2
    {
        [Key]
        public int Id { get; set; }
        public string player { get; set; }
        public int bet { get; set; }
        public int win { get; set; }
        public string content { get; set; }
        public string status { get; set; }
    }
}
