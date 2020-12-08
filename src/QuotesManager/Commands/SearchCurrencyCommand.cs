using System.Threading.Tasks;
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
            await Task.Delay(0);
        }
    }
}