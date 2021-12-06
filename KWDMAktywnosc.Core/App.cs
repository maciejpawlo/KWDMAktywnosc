using KWDMAktywnosc.Core.ViewModels;
using MvvmCross.ViewModels;
using System;

namespace KWDMAktywnosc.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();
            RegisterAppStart<MainViewModel>();
        }
    }
}
