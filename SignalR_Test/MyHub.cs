using Microsoft.AspNetCore.SignalR;

namespace SignalR_Test
{
    public class MyHub: Hub
    {
        public async Task BraodCast(string data)
        {
            await Clients.All.SendAsync("HubMethodBroadcast", data);
        }    
    }
}
