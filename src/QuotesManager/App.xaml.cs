using System.Windows;
using Wpf.Tools;

namespace QuotesManager
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppLifecycleManager.Instance.Init(this, ServiceLocator.Container);
            AppLifecycleManager.Instance.RegisterTypes();
            AppLifecycleManager.Instance.OnAppStart();
        }
    }
}