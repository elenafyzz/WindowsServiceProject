using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using System.ServiceProcess;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string address = "http://localhost:9000/";
            var service = new WebsitesMonitoringService.WebsitesMonitoringService();
            ServiceBase[] servicesToRun = new ServiceBase[] { service }; 
            if (Environment.UserInteractive)
            {
                using (WebApp.Start<Startup>(url: address))
                {
                    HttpClient client = new HttpClient();
                    var response = client.GetAsync(address + "api/status").Result;
                    Console.WriteLine(response);
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    service.Start();
                    service.printInfo();
                    Console.WriteLine("Press any key to stop your service");
                    Console.ReadKey();
                    service.Stop();
                    Console.WriteLine("Service is stopped");
                    Console.ReadLine();
                }
            }
            else
            {
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
