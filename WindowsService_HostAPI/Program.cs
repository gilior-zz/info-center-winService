using System;
using System.ServiceProcess;
using System.Runtime.InteropServices;

namespace WindowsService_HostAPI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static void Main(string[] args)
        {
            //ServiceBase[] ServicesToRun;

            //SelfHostService _selfHostService = new SelfHostService();

            //_selfHostService.ServiceName = "foo";

            //ServicesToRun = new ServiceBase[]
            //{
            //    _selfHostService
            //};
            //ServiceBase.Run(ServicesToRun);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException; ;

            var service = new SelfHostService();

            service.ManualStart(args);
            var handle = GetConsoleWindow();

            // Hide
            ShowWindow(handle, SW_HIDE);
            Console.WriteLine("Service started, press any key to kill");
            Console.ReadKey();

            //service.ManualStop();

            //ServiceBase.Run(new ServiceBase[] { service });


            AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;




        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
