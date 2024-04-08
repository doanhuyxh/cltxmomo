using cltxmomo.Data;

namespace cltxmomo.Ultils
{
    public class Common : ICommon
    {
        private readonly ApplicationDbContext _context;
        public Common(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddHistoryWin()
        {
            return true;
        }
    }
}
