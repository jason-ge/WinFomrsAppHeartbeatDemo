namespace Heartbeat.Abstractions
{
    public interface IHeartbeatGeneratorService
    {
        event EventHandler<HeartbeatStatus> HeartbeatSent;
        SynchronizationContext? HeartbeatContext { get; set; }
        void Pause();
        void Resume();
    }
}