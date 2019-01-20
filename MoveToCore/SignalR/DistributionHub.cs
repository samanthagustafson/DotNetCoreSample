using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MoveToCore.SignalR
{
    public class DistributionHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("distribution", user, message);
        }
    }
}
