using Heartbeat.Abstractions;
using HeartbeatClientService;
using Microsoft.Extensions.DependencyInjection;

namespace MainForm
{
    public partial class Form1 : Form
    {
        private IHeartbeatGeneratorService _heartbeatClientService;
        public Form1(IServiceProvider provider)
        {
            InitializeComponent();
            _heartbeatClientService = provider.GetRequiredService<HeartbeatGeneratorService>();
            _heartbeatClientService.HeartbeatSent += UpdateHeartbeatStatus;
            _heartbeatClientService.HeartbeatContext = SynchronizationContext.Current;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnPause.Enabled = true;
            btnResume.Enabled = false;
        }

        public void UpdateHeartbeatStatus(object? sender, HeartbeatStatus status)
        {
            switch (status)
            {
                case HeartbeatStatus.Beating:
                    txtOutput.Invoke(() => txtOutput.AppendText($"Heartbeat sent at {DateTime.Now.ToString("HH:mm:ss")}\r\n"));
                    break;
                case HeartbeatStatus.Pause:
                    txtOutput.Invoke(() => txtOutput.AppendText($"Heartbeat paused at {DateTime.Now.ToString("HH:mm:ss")}\r\n"));
                    break;
            }
        }

        private void Form1_Close(object sender, EventArgs e)
        {
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            _heartbeatClientService.Pause();
            btnPause.Enabled = false;
            btnResume.Enabled = true;
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            _heartbeatClientService.Resume();
            btnPause.Enabled = true;
            btnResume.Enabled = false;
        }

        private void btnBlock_Click(object sender, EventArgs e)
        {
            Thread.Sleep(10000);
        }
    }
}