namespace SignalR_Test
{
    public class TimerManager
    {
        private Timer? _timer;
        private AutoResetEvent? _autoResetEvent;
        private Action? _action;
        public DateTime TimerStarted { get; set; }
        public bool IsTimerStarted { get; set; }
        public void PrepareTimer(Action action)
        {
            _action = action;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Execute, _autoResetEvent, 1000, 5000);//1秒後開始執行，後續5秒執行推播一次
            TimerStarted = DateTime.Now;
            IsTimerStarted = true;
        }
        public void Execute(object? stateInfo)
        {
            _action();

            //連線60秒後，中斷SignalR(Server to Client)的推播
            if ((DateTime.Now - TimerStarted).TotalSeconds > 60)
            {
                IsTimerStarted = false;
                _timer.Dispose();
            }
        }
    }
}
