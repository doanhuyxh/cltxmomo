using cltxmomo.Data;
using cltxmomo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Numerics;

namespace cltxmomo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.ListPhoneNap = _context.RechargePhoneNumber.ToList();
            ViewBag.TopDay = _context.TopDay.ToList();
            ViewBag.BoxChatTelegram = _context.WebConfig.FirstOrDefault(i => i.KeyName == "BoxChatTelegram").Value;
            ViewBag.Domain2 = _context.WebConfig.FirstOrDefault(i => i.KeyName == "Domain2").Value;
            ViewBag.Domain3 = _context.WebConfig.FirstOrDefault(i => i.KeyName == "Domain3").Value;
            ViewBag.CSKH = _context.WebConfig.FirstOrDefault(i => i.KeyName == "CSKH").Value;
            return View();
        }

        public IActionResult HistoryWin()
        {
            var data = from _h in _context.HistoryWin
                       orderby _h.Id descending
                       select new
                       {
                           status = _h.Status == "win" ? "thắng" : "thua",
                           content = _h.Content,
                           win = _h.Received,
                           bet = _h.Deposit,
                           player = _h.PhoneNumber,
                       };

            return Ok(data);
        }
    }
}
