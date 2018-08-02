using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace PMS.Hubs
{
    public class MonitoringSystemHub : Hub
    {
        public Task LoadData()
        {
            return Clients.All.SendAsync("LoadData");
        }

    }
}