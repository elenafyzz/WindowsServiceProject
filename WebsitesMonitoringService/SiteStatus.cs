using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace WebsitesMonitoringService
{
    public class SiteStatus
    {
        private FileWriter fw;
        private CancellationTokenSource cancelTokenSource;
        public static Mutex mutex;

        public SiteStatus(CancellationTokenSource cancelTokenSource)
        {
            fw = new FileWriter();
            mutex = new Mutex();
            this.cancelTokenSource = cancelTokenSource;
        }

        public void GoogleStatus()
        {
            while (!cancelTokenSource.IsCancellationRequested)
            {
                ping("https://www.google.com.ua/");
                Thread.Sleep(120000);
            }
        }

        public void AppleStatus()
        {
            while (!cancelTokenSource.IsCancellationRequested)
            {
                ping("http://www.apple.com/");
                Thread.Sleep(300000);
            }
        }

        public void MicrosoftStatus()
        {
            while (!cancelTokenSource.IsCancellationRequested)
            {
                var dateNow = DateTime.Now;
                int hour = 22, minute = 15, second = 0;
                DateTime dateTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, hour, minute, second);
                TimeSpan delay;
                if (dateNow > dateTime)
                    dateTime = dateTime.AddDays(2);
                delay = dateTime - dateNow;
                Task.Delay(delay).Wait();
                if (!cancelTokenSource.IsCancellationRequested)
                    ping("https://www.microsoft.com/uk-ua/");
            }
        }
        
        public void ping(string site_name)
        {
            Uri uri = new Uri(site_name);
            Ping ping = new Ping();
            try
            {
                PingReply pingReply = ping.Send(uri.Host);
                mutex.WaitOne();
                fw.Write(string.Format("{0,32} {1,15} {2,30}", site_name, pingReply.Status, DateTime.Now));
                mutex.ReleaseMutex();
            }
            catch (Exception) { }
        }

        public string getInfo()
        {
           return "Sites' statuses are being written in the file " + fw.getFileName;
        }
    }
}
