using QuotesManager.Repository.Interfaces;
using QuotesManager.ViewModels;
using Unity;
using Wpf.Tools.Base;

namespace QuotesManager.Commands
{
    public class DownloadCurrencyListCommand : CommandBase
    {
        private readonly IUnityContainer _container;

        public DownloadCurrencyListCommand(IUnityContainer container)
        {
            _container = container.RegisterInstance(this);
        }

        protected override async void ExecuteExternal(object parameter)
        {
            var mainViewModel = _container.Resolve<MainViewModel>();
            var repository = _container.Resolve<ICurrencyRepository>();
            var currencyCodeList = await repository.GetCurrencyListAsync();

            mainViewModel.CurrencyCodeList.Clear();

            foreach (var currency in currencyCodeList)
            {
                mainViewModel.CurrencyCodeList.Add(currency);
            }
        }
    }
}