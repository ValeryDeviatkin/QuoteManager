using Unity;
using Wpf.Tools.Base;

namespace QuotesManager.Commands
{
    public class UploadCurrencyListCommand : CommandBase
    {
        private readonly IUnityContainer _container;

        public UploadCurrencyListCommand(IUnityContainer container)
        {
            _container = container.RegisterInstance(this);
        }

        protected override void ExecuteExternal(object parameter)
        {
            // TODO: Handle command logic here
        }
    }
}