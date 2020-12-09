using System.Threading.Tasks;
using QuotesManager.Helpers;
using QuotesManager.Repository.Interfaces;
using QuotesManager.ViewModels;
using Unity;
using Wpf.Tools.Base;

namespace QuotesManager.Commands
{
    public class SearchCurrencyCommand : AsyncCommandBase
    {
        private readonly IUnityContainer _container;

        public SearchCurrencyCommand(IUnityContainer container)
        {
            _container = container.RegisterInstance(this);
        }

        protected override async Task ExecuteExternal(object parameter)
        {
            var mainViewModel = _container.Resolve<MainViewModel>();
            var repository = _container.Resolve<ICurrencyRepository>();
            var currencyInfoList = await repository.FindCurrencyAsync(mainViewModel.SearchString);

            await DispatcherHelper.BeginInvokeInMainThread(() =>
            {
                mainViewModel.FoundCurrencyCollection.Clear();

                foreach (var currency in currencyInfoList)
                {
                    mainViewModel.FoundCurrencyCollection.Add(currency);
                }
            });
        }
    }
}