using Heartbeat.Abstractions;
using Microsoft.Extensions.Hosting;

namespace HeartbeatClientService
{
    public class HeartbeatGeneratorService(IHeartbeatClientChannel heartbeatChannel) : BackgroundService, IHeartbeatGeneratorService
    {
        private readonly IHeartbeatClientChannel _heartbeatChannel = heartbeatChannel;
        public event EventHandler<HeartbeatStatus> HeartbeatSent;
        private bool _isPaused;
        private bool _isPauseSignalSent;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (HeartbeatContext != null)
                {
                    if (!_isPaused)
                    {
                        HeartbeatContext.Post(async (_) => {
                            await _heartbeatChannel.SendAsync("*", stoppingToken);
                        }, null);
                        OnHeartBeat(HeartbeatStatus.Beating);
                    }
                    else if (!_isPauseSignalSent)
                    {
                        HeartbeatContext.Post(async (_) => {
                            await _heartbeatChannel.SendAsync("-", stoppingToken);
                        }, null);
                        _isPauseSignalSent = true;
                        OnHeartBeat(HeartbeatStatus.Pause);
                    }
                }
                await Task.Delay(_heartbeatChannel.HeartbeatInterval * 1000, stoppingToken);
            }
        }
        public SynchronizationContext? HeartbeatContext { get; set; }
        public void Pause()
        {
            _isPaused = true;
        }
        public void Resume()
        {
            _isPaused = false;
            _isPauseSignalSent = false;
        }
        private void OnHeartBeat(HeartbeatStatus status)
        {
            if (HeartbeatSent != null)
            {
                HeartbeatSent(this, status);
            }
        }
    }
}