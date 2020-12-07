using Unity;
using Wpf.Tools.Base;

namespace QuotesManager.Commands
{
    public class RefreshCurrencyCommand : CommandBase
    {
        private readonly IUnityContainer _container;

        public RefreshCurrencyCommand(IUnityContainer container)
        {
            _container = container.RegisterInstance(this);
        }

        protected override void ExecuteExternal(object parameter)
        {
            // TODO: Handle command logic here
        }
    }
}