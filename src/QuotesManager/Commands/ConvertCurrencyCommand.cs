using System.Threading.Tasks;
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
            await Task.Delay(0);
        }
    }
}