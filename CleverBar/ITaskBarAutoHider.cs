using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverBar
{
    public interface ITaskBarAutoHider
    {

        bool IsAutoHideMode();
        bool TurnOffAutoHide();
        bool TurnOnAutoHide();

    }
}
