using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SignalR_Test.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class SignalRController : ControllerBase
    {
        private readonly IHubContext<MyHub> _hub;
        private readonly TimerManager _timer;

        public SignalRController(IHubContext<MyHub> hub, TimerManager timer)
        {
            _hub = hub;
            _timer = timer;
        }

        public IActionResult Subscribe()
        {
            if (!_timer.IsTimerStarted)
            {//是否在連線時限60秒內，是的話就調用Hub Method [HubMethodGetRandomNumber]
                _timer.PrepareTimer(() => _hub.Clients.All.SendAsync("HubMethodGetRandomNumber", GetRandom()));
            }
                
            return Ok(new { Message = "Request Completed" });
        }

        public int GetRandom() 
        { 
            var rnd = new Random();
            var Res = rnd.Next(0, 100);
            return Res;
        }
    }
}
