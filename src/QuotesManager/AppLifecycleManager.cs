using System;
using System.Windows.Threading;
using QuotesManager.Commands;
using QuotesManager.Interfaces;
using QuotesManager.Repository;
using QuotesManager.ViewModels;
using QuotesManager.Views;
using Unity;
using Wpf.Tools.Helpers;

namespace QuotesManager
{
    internal class AppLifecycleManager
    {
        private App _app;
        private IUnityContainer _container;

        public void Init(App app, IUnityContainer container)
        {
            try
            {
                _app = app;
                _container = container;
            }
            catch (Exception e)
            {
                this.LogCriticalException(e);
            }
        }

        public void OnAppStart()
        {
            try
            {
                _app.DispatcherUnhandledException += OnDispatcherUnhandledException;
                _container.Resolve<AppSettings>().Init();

                var mainWindow = _container.Resolve<MainWindow>();
                mainWindow.DataContext = _container.Resolve<MainViewModel>();
                mainWindow.Show();
            }
            catch (Exception e)
            {
                this.LogCriticalException(e);
            }
        }

        public void RegisterTypes()
        {
            try
            {
                RepositoryModuleInitializer.Instance.Init(_container);

                _container
                    // Infrastructure.
                   .RegisterType<AppCommands>()
                   .RegisterType<AppSettings>()
                   .RegisterType<ICurrencySourceUrlProvider, AppSettings>()

                    // Views.
                   .RegisterSingleton<MainWindow>()

                    // View Models.
                   .RegisterType<MainViewModel>()

                    // Commands.
                   .RegisterType<DownloadCurrencyListCommand>()
                   .RegisterType<RefreshCurrencyCommand>()
                   .RegisterType<SearchCurrencyCommand>()
                   .RegisterType<ConvertCurrencyCommand>()

                    //
                    ;
            }
            catch (Exception e)
            {
                this.LogCriticalException(e);
            }
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            this.LogCriticalException(e.Exception);
        }

        #region singleton

        private AppLifecycleManager()
        {
        }

        public static AppLifecycleManager Instance { get; } = new AppLifecycleManager();

        #endregion
    }
}