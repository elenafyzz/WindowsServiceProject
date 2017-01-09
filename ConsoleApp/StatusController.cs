using System.ServiceProcess;
using System.Web.Http;

namespace ConsoleApp
{
    public class StatusController : ApiController
    {
        // GET api/status
        // if service is installed on local machine
        [HttpGet]
        public bool CheckStatus()
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController sc in services)
            {
                if (sc.ServiceName == "WebsitesMonitoringService"
                    && sc.Status == ServiceControllerStatus.Running)
                    return true;
            }
            return false;
        }
    }
}

