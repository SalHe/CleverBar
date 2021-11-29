using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CleverBar
{
    public class TrayIconViewModel : ReactiveObject
    {

        private ITaskBarAutoHider _taskBarAutoHider;

        #region 属性

        private bool _autoHide;

        public bool AutoHide
        {
            get => _autoHide;
            set => this.RaiseAndSetIfChanged(ref _autoHide, value);
        }

        #endregion

        #region 命令

        public ReactiveCommand<Unit, Unit> ToggleAutoHideTaskbar { get; }
        public ReactiveCommand<Unit, Unit> AboutApplication { get; }
        public ReactiveCommand<Unit, Unit> ExitApplication { get; }

        #endregion

        public TrayIconViewModel()
        {
            // TODO 考虑看一下能不能移除无参构造函数，又能和Xaml构造TaskbarIcon需要无参构造器不冲突
        }

        public TrayIconViewModel(ITaskBarAutoHider taskBarAutoHider)
        {
            _taskBarAutoHider = taskBarAutoHider;

            AutoHide = _taskBarAutoHider.IsAutoHideMode();
            ToggleAutoHideTaskbar = ReactiveCommand.Create(() =>
            {
                if (AutoHide)
                    _taskBarAutoHider.TurnOffAutoHide();
                else
                    _taskBarAutoHider.TurnOnAutoHide();
                AutoHide = !AutoHide;
            });

            AboutApplication = ReactiveCommand.Create(() =>
            {
                MessageBox.Show("一个快速在托盘切换是否自动隐藏任务栏的小工具！", "CleverBar by SalHe",
                    MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            });
            ExitApplication = ReactiveCommand.Create(() => Application.Current.Shutdown());
        }

    }
}
