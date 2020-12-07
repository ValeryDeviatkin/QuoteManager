using Unity;
using Wpf.Tools.Base;

namespace QuotesManager.Commands
{
    public class ConvertCurrencyCommand : CommandBase
    {
        private readonly IUnityContainer _container;

        public ConvertCurrencyCommand(IUnityContainer container)
        {
            _container = container.RegisterInstance(this);
        }

        protected override void ExecuteExternal(object parameter)
        {
            // TODO: Handle command logic here
        }
    }
}