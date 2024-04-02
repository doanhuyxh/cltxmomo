using System.ComponentModel.DataAnnotations;

namespace cltxmomo.Models
{
    public class HistoryWin
    {
        [Key]
        public int Id { get; set; }
        public string PhoneNumber { set; get; }
        public string Deposit { set; get; }
        public string Received { set; get; }
        public string Content { set; get; }
        public string Status { set; get; }
    }
}
