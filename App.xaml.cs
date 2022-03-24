
using System.Windows;
using Prism.Ioc;
using Prism.Unity;
using RSA_App.View;
using RSA_App.ViewModel;
using Prism.Regions;

namespace RSA_App
{
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override Window CreateShell()
        {
            var w = Container.Resolve<MainWindow>();
            return w;
        }
    }
}
