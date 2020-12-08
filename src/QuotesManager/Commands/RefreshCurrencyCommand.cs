using System.Threading.Tasks;
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
            await Task.Delay(0);
        }
    }
}