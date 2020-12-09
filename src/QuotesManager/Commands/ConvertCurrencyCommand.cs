using System.Threading.Tasks;
using QuotesManager.Helpers;
using QuotesManager.Repository.Interfaces;
using QuotesManager.ViewModels;
using Unity;
using Wpf.Tools.Base;

namespace QuotesManager.Commands
{
    public class ConvertCurrencyCommand : AsyncCommandBase
    {
        private readonly IUnityContainer _container;

        public ConvertCurrencyCommand(IUnityContainer container)
        {
            _container = container.RegisterInstance(this);
        }

        protected override async Task ExecuteExternal(object parameter)
        {
            var mainViewModel = _container.Resolve<MainViewModel>();
            var repository = _container.Resolve<ICurrencyRepository>();
            var convertedCurrencyValue = await repository.ConvertCurrencyAsync(
                                             mainViewModel.SourceConvertingCurrency.Id,
                                             mainViewModel.SourceConvertingValue,
                                             mainViewModel.TargetConvertingCurrency.Id);

            await DispatcherHelper.BeginInvokeInMainThread(() =>
            {
                mainViewModel.TargetConvertingValue =
                    convertedCurrencyValue;
            });
        }
    }
}