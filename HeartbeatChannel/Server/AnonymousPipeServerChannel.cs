using Heartbeat.Abstractions;
using System.IO.Pipes;
namespace HeartBeatService.Server
{
    public class AnonymousPipeServerChannel : IHeartbeatMonitorChannel, IDisposable
    {
        private bool _disposed;
        private AnonymousPipeServerStream _pipeStream;
        private StreamReader _pipeReader;
        private int _heartbeatInterval;
        public object ChannelHandle
        {
            get
            {
                string handle = _pipeStream.GetClientHandleAsString();
                return handle;
            }
        }
        public void DisposeLocalCopyChannelHandle()
        {
            _pipeStream?.DisposeLocalCopyOfClientHandle();
        }
        public AnonymousPipeServerChannel(int heartbeatInterval)
        {
            _pipeStream = new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.Inheritable);
            _pipeReader = new StreamReader(_pipeStream);
            _heartbeatInterval = heartbeatInterval;
        }
        public async Task<string?> ReceiveAsync()
        {
            var timeoutTokenSource = new CancellationTokenSource();
            timeoutTokenSource.CancelAfter((_heartbeatInterval + 1) * 1000);
            try
            {
                return await _pipeReader.ReadLineAsync(timeoutTokenSource.Token);
            }
            catch
            {
                return ".";
            }
        }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                if (_pipeStream != null)
                {
                    _pipeStream.Dispose();
                }
                if (_pipeReader != null)
                {
                    _pipeReader.Dispose();
                }
            }
            _disposed = true;
        }
    }
}