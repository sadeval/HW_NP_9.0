using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyExchangeServer
{
    public class CurrencyRateService : BackgroundService
    {
        private readonly IHubContext<CurrencyHub> _hubContext;
        private readonly Random _random = new Random();

        public CurrencyRateService(IHubContext<CurrencyHub> hubContext)
        {
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var usdToEur = 0.85 + _random.NextDouble() * 0.1;
                var gbpToEur = 1.15 + _random.NextDouble() * 0.1;

                await _hubContext.Clients.All.SendAsync("UpdateCurrencyRate", usdToEur, gbpToEur);

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
