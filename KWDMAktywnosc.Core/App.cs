using KWDMAktywnosc.Core.ViewModels;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using System;

namespace KWDMAktywnosc.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            RegisterAppStart<MainViewModel>();
        }
    }
}
