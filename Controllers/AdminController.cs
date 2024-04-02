using cltxmomo.Data;
using cltxmomo.Models;
using Microsoft.AspNetCore.Mvc;

namespace cltxmomo.Controllers
{
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("admin_ql_cltxmomo")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("admin_ql_cltxmomo/RechargePhoneNumber")]
        public IActionResult RechargePhoneNumber()
        {
            List<RechargePhoneNumber> banks = _context.RechargePhoneNumber.OrderByDescending(i => i.Id).ToList();
            return View(banks);
        }

        [HttpGet("admin_ql_cltxmomo/addedit/RechargePhoneNumber")]
        public IActionResult AddEditRechargePhoneNumber(int id)
        {
            RechargePhoneNumber bank = new RechargePhoneNumber();
            if (id != 0)
            {
                bank = _context.RechargePhoneNumber.FirstOrDefault(i => i.Id == id);
            }

            return PartialView("_addEditRechargePhoneNumber", bank);
        }

        [HttpPost("admin_ql_cltxmomo/save/RechargePhoneNumber")]
        public IActionResult SaveRechargePhoneNumber(RechargePhoneNumber vm)
        {
            if (vm.Id != 0)
            {
                _context.Update(vm);

            }
            else
            {
                _context.Add(vm);
            }
            _context.SaveChanges();

            return Ok(vm);

        }

        [HttpDelete("/admin_ql_cltxmomo/RechargePhoneNumber")]
        public IActionResult DeleteRechargePhoneNumber(int id)
        {
            RechargePhoneNumber phone = _context.RechargePhoneNumber.FirstOrDefault(i => i.Id == id);
            _context.Remove(phone);
            _context.SaveChanges();
            return Ok();
        }


        [HttpGet("admin_ql_cltxmomo/historywin")]
        public IActionResult HistoryWin()
        {
            List<HistoryWin> historyWin = _context.HistoryWin.OrderByDescending(i => i.Id).ToList();
            return View(historyWin);
        }

        [HttpGet("admin_ql_cltxmomo/addedit/historywin")]
        public IActionResult AddEditHistoryWin(int id)
        {
            HistoryWin vm = new HistoryWin();
            if (id != 0)
            {
                vm = _context.HistoryWin.FirstOrDefault(i => i.Id == id);
            }
            return PartialView("_addEditHistoryWin", vm);
        }

        [HttpPost("admin_ql_cltxmomo/save/historywin")]
        public IActionResult SaveHistoryWin(HistoryWin vm)
        {
            if (vm.Id != 0)
            {
                _context.Update(vm);
            }
            else
            {
                _context.Add(vm);
            }
            _context.SaveChanges();

            return Ok(vm);
        }

        [HttpDelete("admin_ql_cltxmomo/delete/historywin")]
        public IActionResult DeleteHistoryWin(int id)
        {
            HistoryWin his = _context.HistoryWin.FirstOrDefault(i => i.Id == id);
            _context.Remove(his);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("admin_ql_cltxmomo/topday")]
        public IActionResult TopDay()
        {
            List<TopDay> topDay = _context.TopDay.ToList();
            return View(topDay);
        }
        [HttpGet("admin_ql_cltxmomo/addedit/TopDay")]
        public IActionResult AddEditTopDay(int id)
        {
            TopDay vm = new TopDay();
            if (id != 0)
            {
                vm = _context.TopDay.FirstOrDefault(i => i.Id == id);
            }
            return PartialView("_addEditTopDay", vm);
        }

        [HttpPost("admin_ql_cltxmomo/save/TopDay")]
        public IActionResult SaveTopDay(TopDay vm)
        {
            if (vm.Id != 0)
            {
                _context.Update(vm);
            }
            else
            {
                _context.Add(vm);
            }
            _context.SaveChanges();

            return Ok(vm);
        }

        [HttpDelete("admin_ql_cltxmomo/delete/TopDay")]
        public IActionResult DeleteTopDay(int id)
        {
            TopDay his = _context.TopDay.FirstOrDefault(i => i.Id == id);
            _context.Remove(his);
            _context.SaveChanges();
            return Ok();
        }


        [HttpGet("admin_ql_cltxmomo/log")]
        public IActionResult Log()
        {
            return View();
        }

        [HttpGet("admin_ql_cltxmomo/code")]
        public IActionResult PromotionalCode()
        {
            return View();
        }


        [HttpGet("admin_ql_cltxmomo/webconfig")]
        public IActionResult WebConfig()
        {
            ViewBag.BoxChatTelegram = _context.WebConfig.FirstOrDefault(i => i.KeyName == "BoxChatTelegram").Value;
            ViewBag.Domain2 = _context.WebConfig.FirstOrDefault(i => i.KeyName == "Domain2").Value;
            ViewBag.Domain3 = _context.WebConfig.FirstOrDefault(i => i.KeyName == "Domain3").Value;
            ViewBag.CSKH = _context.WebConfig.FirstOrDefault(i => i.KeyName == "CSKH").Value;
            return View();
        }

        [HttpPost("admin_ql_cltxmomo/webconfig")]
        public IActionResult SaveWebConfig(WebConfig vm)
        {
            WebConfig w = _context.WebConfig.FirstOrDefault(i => i.KeyName.ToLower() == vm.KeyName.ToLower());
            if (w != null)
            {
                w.Value = vm.Value;
                _context.Update(w);
                _context.SaveChanges();
            }

            return Ok(w);
        }



    }
}
