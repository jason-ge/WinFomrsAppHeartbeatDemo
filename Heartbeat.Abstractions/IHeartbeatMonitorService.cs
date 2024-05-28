namespace Heartbeat.Abstractions
{
    public interface IHeartbeatMonitorService
    {
        event EventHandler<HeartbeatStatus> HeartbeatReceived;
    }
}