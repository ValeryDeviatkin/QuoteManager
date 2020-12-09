using System;
using System.Threading.Tasks;
using QuotesManager.Helpers;
using QuotesManager.Repository.Interfaces;
using QuotesManager.ViewModels;
using Unity;
using Wpf.Tools.Base;

namespace QuotesManager.Commands
{
    public class RefreshCurrencyCommand : AsyncCommandBase
    {
        private readonly IUnityContainer _container;

        public RefreshCurrencyCommand(IUnityContainer container)
        {
            _container = container.RegisterInstance(this);
        }

        protected override async Task ExecuteExternal(object parameter)
        {
            var mainViewModel = _container.Resolve<MainViewModel>();
            var id = (mainViewModel.SelectedCurrency ?? throw new NotSupportedException()).CurrencyId;
            var repository = _container.Resolve<ICurrencyRepository>();
            var currencyInfo = await repository.GetCurrencyAsync(id);

            await DispatcherHelper.BeginInvokeInMainThread(() =>
            {
                mainViewModel.SelectedCurrencyInfo = currencyInfo;
            });
        }
    }
}