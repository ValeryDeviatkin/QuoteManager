using QuotesManager.Commands;
using Unity;

namespace QuotesManager
{
    internal class AppCommands
    {
        private readonly IUnityContainer _container;

        public AppCommands(IUnityContainer container)
        {
            _container = container.RegisterInstance(this);
        }

        public DownloadCurrencyListCommand DownloadCurrencyListCommand =>
            _container.Resolve<DownloadCurrencyListCommand>();

        public RefreshCurrencyCommand RefreshCurrencyCommand => _container.Resolve<RefreshCurrencyCommand>();
        public SearchCurrencyCommand SearchCurrencyCommand => _container.Resolve<SearchCurrencyCommand>();
        public ConvertCurrencyCommand ConvertCurrencyCommand => _container.Resolve<ConvertCurrencyCommand>();
    }
}