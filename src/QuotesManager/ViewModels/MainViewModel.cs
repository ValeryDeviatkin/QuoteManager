using Unity;
using Wpf.Tools.Base;

namespace QuotesManager.ViewModels
{
    internal class MainViewModel : ObservableObject
    {
        private readonly IUnityContainer _container;

        /// <summary>
        ///     Only for design DataContext creation.
        /// </summary>
        public MainViewModel()
        {
        }

        public MainViewModel(IUnityContainer container, AppCommands commands)
        {
            _container = container.RegisterInstance(this);

            Commands = commands;
        }

        public AppCommands Commands { get; }
    }
}