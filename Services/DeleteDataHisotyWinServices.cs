using System;
using System.Threading;
using System.Threading.Tasks;
using cltxmomo.Ultils; // Chắc chắn rằng namespace này chứa các lớp và phương thức cần thiết
using cltxmomo.Data;
using cltxmomo.Models;
using System.Text.RegularExpressions; // Chắc chắn rằng namespace này chứa lớp ApplicationDbContext

namespace cltxmomo.Services
{
    public class DeleteDataHisotyWinServices : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DeleteDataHisotyWinServices(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            Task.Run(async () => await DoWorkAsync());
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private async Task DoWorkAsync()
        {
            Console.WriteLine("DoWorkAsync");
            AddHistoryWin();
        }

        private void AddHistoryWin()
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    List<HistoryWin> delete = dbContext.HistoryWin.OrderByDescending(i => i.Id).Skip(15).ToList();
                    dbContext.Remove(delete);
                    dbContext.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding history: {ex.Message}");
            }
        }
    }
}
