

using System.Diagnostics;
using System.ServiceProcess;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.SelfHost;

namespace WindowsService_HostAPI
{
    partial class SelfHostService : ServiceBase
    {
        public SelfHostService()
        {
            InitializeComponent();
        }

        public void ManualStart(string[] args)
        {
            OnStart(args);
        }

        public void ManualStop()
        {
            OnStop();
        }

        protected override void OnStart(string[] args) 
        {
            //Process[] pname = Process.GetProcessesByName("smart-office-service");
            //if (pname.Length != 0) pname[0].Kill();


            var config = new HttpSelfHostConfiguration("http://localhost:8080");

            config.Routes.MapHttpRoute(
               name: "API",
               routeTemplate: "api/{controller}/{action}/{fp}",
               defaults: new { fp = RouteParameter.Optional }
           );
            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);
            HttpSelfHostServer server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
