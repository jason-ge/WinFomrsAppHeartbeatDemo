namespace Heartbeat.Abstractions
{
    public interface IHeartbeatMonitorChannel
    {
        object ChannelHandle { get; }
        void DisposeLocalCopyChannelHandle();
        Task<string?> ReceiveAsync();
    }
}