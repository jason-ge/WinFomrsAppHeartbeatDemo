namespace Heartbeat.Abstractions
{
    public interface IHeartbeatClientChannel
    {
        int HeartbeatInterval { get; }
        Task SendAsync(string data, CancellationToken cancellationToken);
    }
}
