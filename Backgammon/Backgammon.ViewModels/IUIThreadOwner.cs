using System;
using System.Collections.Generic;
using System.Text;

namespace Backgammon.ViewModels
{
    public interface IUIThreadOwner
    {
        void Invoke(Action<object[]> action, params object[] parameters);
    }
}
