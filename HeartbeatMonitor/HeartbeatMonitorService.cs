using Heartbeat.Abstractions;
using Microsoft.Extensions.Hosting;
namespace HeartbeatServerService
{
    public class HeartbeatMonitorService(IHeartbeatMonitorChannel heartbeatChannel) : BackgroundService, IHeartbeatMonitorService
    {
        private readonly IHeartbeatMonitorChannel _heartbeatChannel = heartbeatChannel;
        public event EventHandler<HeartbeatStatus> HeartbeatReceived;
        private bool _isPaused;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string? data = await _heartbeatChannel.ReceiveAsync();
                if (data == null)
                {
                    OnHeartBeat(HeartbeatStatus.Gone);
                    break;
                }
                else if (data.Equals("*"))
                {
                    _isPaused = false;
                    OnHeartBeat(HeartbeatStatus.Beating);
                }
                else if (data.Equals("-"))
                {
                    _isPaused = true;
                    OnHeartBeat(HeartbeatStatus.Pause);
                }
                else if (!_isPaused)
                {
                    OnHeartBeat(HeartbeatStatus.Missing);
                }
            }
        }
        private void OnHeartBeat(HeartbeatStatus status)
        {
            if (HeartbeatReceived != null)
            {
                HeartbeatReceived(this, status);
            }
        }
    }
}
