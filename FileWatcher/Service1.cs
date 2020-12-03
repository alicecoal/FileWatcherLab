using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace FileWatcher
{
    public partial class Service1 : ServiceBase
    {
        Watcher watcher;
        public Service1()
        {
            InitializeComponent();
            AutoLog = true;
            CanStop = true;
            watcher = new Watcher();
        }

        protected override void OnStart(string[] args)
        {
            watcher = new Watcher();
            var thread = new Thread(new ThreadStart((watcher.Start)));
            thread.Start();
            Logger.Log("Cервис запущен");
        }

        protected override void OnStop()
        {
            Logger.Log("Cервис остановлен");
            watcher.Stop();
        }
    }
}
