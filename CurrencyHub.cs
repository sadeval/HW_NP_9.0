using Microsoft.AspNetCore.SignalR;

namespace CurrencyExchangeServer
{
    public class CurrencyHub : Hub
    {
        public async Task SendCurrencyRate(string currencyPair, decimal rate)
        {
            await Clients.All.SendAsync("ReceiveCurrencyRate", currencyPair, rate);
        }
    }
}
