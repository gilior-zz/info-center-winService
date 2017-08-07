

using System;
using System.Diagnostics;
using System.Web.Configuration;
using System.Web.Http;

namespace WindowsService_HostAPI
{
    public class ValuesController : ApiController
    {


        public VersionResponse GetVersion()
        {
            string res = WebConfigurationManager.AppSettings["version"];
            return new VersionResponse() { version = res ?? "1.0.0" };
        }
        public WindowsUserinfo GetUserEnv()
        {
            WindowsUserinfo windowsUserinfo = new WindowsService_HostAPI.WindowsUserinfo();
            windowsUserinfo.name = Environment.UserName;
            windowsUserinfo.machineName = Environment.MachineName;
            windowsUserinfo.osVersion = Environment.OSVersion;
            windowsUserinfo.userDomainName = Environment.UserDomainName;

            return windowsUserinfo;
        }

        [AcceptVerbs("POST")]
        public bool RunFile([FromBody]dynamic obj)
        {
            try
            {
                Process prs = Process.Start(obj.file.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;

            }

            //bool res = prs.WaitForInputIdle();

        }
    }

    public class WindowsUserinfo
    {
        public string machineName { get; internal set; }
        public string name { get; set; }
        public OperatingSystem osVersion { get; internal set; }
        public string userDomainName { get; internal set; }
    }

    public class VersionResponse
    {
        public string version { get; set; }
    }
}