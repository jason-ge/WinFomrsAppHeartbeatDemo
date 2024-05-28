using Heartbeat.Abstractions;
using System.IO.Pipes;
using System.Threading;
namespace HeartbeatChannel.Client
{
    public class AnonymousPipeClientChannel : IHeartbeatClientChannel, IDisposable
    {
        private bool _disposed;
        private PipeStream _pipeStream;
        private StreamWriter _pipeWriter;
        private int _heartbeatInterval;
        public AnonymousPipeClientChannel(string pipeHandle, int heartbeatInterval)
        {
            _pipeStream = new AnonymousPipeClientStream(PipeDirection.Out, pipeHandle);
            _pipeWriter = new StreamWriter(_pipeStream);
            _pipeWriter.AutoFlush = true;
            _heartbeatInterval = heartbeatInterval;
        }
        public int HeartbeatInterval => _heartbeatInterval;
        public async Task SendAsync(string data, CancellationToken cancellationToken)
        {
            await _pipeWriter.WriteLineAsync(data);
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
                if (_pipeWriter != null)
                {
                    _pipeWriter.Dispose();
                }
            }
            _disposed = true;
        }
    }
}