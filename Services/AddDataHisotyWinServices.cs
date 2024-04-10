using System;
using System.Threading;
using System.Threading.Tasks;
using cltxmomo.Ultils; // Chắc chắn rằng namespace này chứa các lớp và phương thức cần thiết
using cltxmomo.Data;
using cltxmomo.Models;
using System.Text.RegularExpressions; // Chắc chắn rằng namespace này chứa lớp ApplicationDbContext

namespace cltxmomo.Services
{
    public class AddDataHisotyWinServices : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AddDataHisotyWinServices(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
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

                    List<HistoryWin> histories = new List<HistoryWin>();


                    for (Int16 i = 0; i < 15; i++)
                    {
                        Random random = new Random();
                        int randomNumber = random.Next(10, 100);

                        string status = (new Random()).Next(2) == 0 ? "win" : "lose";
                        HistoryWin win = new HistoryWin();
                        win.Content = new Random().Next(11, 14) switch { 13 => "Tài", 14 => "Xỉu", 12 => "Chẵn", _ => "Lẻ" };
                        win.Status = status;
                        win.PhoneNumber = "****" + string.Join("", Enumerable.Range(0, 4).Select(_ => new Random().Next(10)));
                        win.Deposit = (randomNumber * 1000).ToString();

                        if (status == "lose")
                        {
                            win.Received = "0";
                        }
                        else
                        {
                            win.Received = (randomNumber * 1000 * 2).ToString();
                        }

                        histories.Add(win);
                    }

                    dbContext.AddRange(histories);
                    //List<HistoryWin> delete = dbContext.HistoryWin.o.Take(15).ToList();
                    //dbContext.Remove(delete);
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
