using Heartbeat.Abstractions;
using HeartbeatServerService;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace MonitoringApp
{
    public partial class MonitorForm : Form
    {
        private IServiceProvider _provider;
        private IHeartbeatMonitorService _heartbeatServerService;
        private Process clientApp = new Process();
        private int _heartbeatInterval;
        public MonitorForm(IServiceProvider provider, int heartbeatInterval)
        {
            InitializeComponent();
            _heartbeatServerService = provider.GetRequiredService<HeartbeatMonitorService>();
            _heartbeatServerService.HeartbeatReceived += UpdateHeartbeatStatus;
            _provider = provider;
            _heartbeatInterval = heartbeatInterval;
        }

        public void UpdateHeartbeatStatus(object? sender, HeartbeatStatus status)
        {
            switch (status)
            {
                case HeartbeatStatus.Beating:
                    txtOutput.Invoke(() => txtOutput.AppendText($"Heartbeat is received at {DateTime.Now.ToString("HH:mm:ss")}\r\n"));
                    break;
                case HeartbeatStatus.Missing:
                    txtOutput.Invoke(() => txtOutput.AppendText($"Heartbeat is missed at {DateTime.Now.ToString("HH:mm:ss")}\r\n"));
                    break;
                case HeartbeatStatus.Pause:
                    txtOutput.Invoke(() => txtOutput.AppendText($"Heartbeat paused at {DateTime.Now.ToString("HH:mm:ss")}\r\n"));
                    break;
                case HeartbeatStatus.Gone:
                    txtOutput.Invoke(() => txtOutput.AppendText($"Heartbeat is gone at {DateTime.Now.ToString("HH:mm:ss")}\r\n"));
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var channel = _provider.GetRequiredService<IHeartbeatMonitorChannel>();
            clientApp.StartInfo.FileName = "..\\..\\..\\..\\MainForm\\bin\\Debug\\net8.0-windows\\MainForm.exe";
            clientApp.StartInfo.Arguments = channel.ChannelHandle.ToString() + " " + _heartbeatInterval.ToString();
            clientApp.StartInfo.UseShellExecute = false;
            clientApp.Start();
            channel.DisposeLocalCopyChannelHandle();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            clientApp.Kill();
        }
    }
}