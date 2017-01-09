using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace WebsitesMonitoringService
{
    public partial class WebsitesMonitoringService : ServiceBase
    {
        private Task[] tasks;
        private CancellationTokenSource cancelTokenSource;
        private SiteStatus st;

        public WebsitesMonitoringService()
        {
            InitializeComponent();
            ServiceName = "WebsitesMonitoringService";
            cancelTokenSource = new CancellationTokenSource();
            st = new SiteStatus(cancelTokenSource);
            tasks = new Task[3];
        }

        public void printInfo()
        {
            Console.WriteLine(st.getInfo());
        }

        public void Start()
        {
            tasks[0] = Task.Factory.StartNew(st.GoogleStatus, cancelTokenSource.Token);
            tasks[1] = Task.Factory.StartNew(st.AppleStatus, cancelTokenSource.Token);
            tasks[2] = Task.Factory.StartNew(st.MicrosoftStatus, cancelTokenSource.Token);
        }

        public new void Stop()
        {
            cancelTokenSource.Cancel();
        }

        protected override void OnStart(string[] args)
        {
            Start();
        }

        protected override void OnStop()
        {
            Stop();
        }
    }
}
