using Hardcodet.Wpf.TaskbarNotification;
using Splat;
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
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private TaskbarIcon _taskbarIcon;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RegisterServices();
            _taskbarIcon = (TaskbarIcon)FindResource("TrayIcon");
            _taskbarIcon.DataContext = Locator.Current.GetService<TrayIconViewModel>();
        }

        private void RegisterServices()
        {
            Locator.CurrentMutable.RegisterLazySingleton<ITaskBarAutoHider>(() => new WinTaskBarAutoHider());
            Locator.CurrentMutable.RegisterLazySingleton(() => new TrayIconViewModel(Locator.Current.GetService<ITaskBarAutoHider>()!));
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _taskbarIcon.Dispose();
            base.OnExit(e);
        }
    }
}
