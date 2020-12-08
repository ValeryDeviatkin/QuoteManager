using System.Linq;
using System.Threading.Tasks;
using QuotesManager.Helpers;
using QuotesManager.Repository.Interfaces;
using QuotesManager.ViewModels;
using Unity;
using Wpf.Tools.Base;

namespace QuotesManager.Commands
{
    public class DownloadCurrencyListCommand : AsyncCommandBase
    {
        private readonly IUnityContainer _container;

        public DownloadCurrencyListCommand(IUnityContainer container)
        {
            _container = container.RegisterInstance(this);
        }

        protected override async Task ExecuteExternal(object parameter)
        {
            var mainViewModel = _container.Resolve<MainViewModel>();
            var repository = _container.Resolve<ICurrencyRepository>();
            var currencyCodeList = await repository.GetCurrencyListAsync();

            await DispatcherHelper.BeginInvokeInMainThread(() =>
            {
                mainViewModel.CurrencyPreviewList.Clear();

                foreach (var currency in currencyCodeList)
                {
                    mainViewModel.CurrencyPreviewList.Add(currency);
                }

                var selectedCurrency = mainViewModel.CurrencyPreviewList.FirstOrDefault();
                mainViewModel.SelectedCurrency = selectedCurrency;
                mainViewModel.SourceConvertingCurrency = selectedCurrency;
                mainViewModel.TargetConvertingCurrency = selectedCurrency;
            });
        }
    }
}