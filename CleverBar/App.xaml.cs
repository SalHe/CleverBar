using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CleverBar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon _taskbarIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _taskbarIcon = (TaskbarIcon)FindResource("TrayIcon");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _taskbarIcon.Dispose();
            base.OnExit(e);
        }
    }
}
