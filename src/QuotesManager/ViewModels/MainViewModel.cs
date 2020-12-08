using System.Collections.ObjectModel;
using QuotesManager.Repository.DataTransferObjects;
using Unity;
using Wpf.Tools.Base;

namespace QuotesManager.ViewModels
{
    internal class MainViewModel : ObservableObject
    {
        /// <summary>
        ///     Only for design DataContext creation.
        /// </summary>
        public MainViewModel()
        {
        }

        public MainViewModel(IUnityContainer container, AppCommands commands)
        {
            container.RegisterInstance(this);

            Commands = commands;
        }

        public AppCommands Commands { get; }

        public ObservableCollection<CurrencyPreviewDto> CurrencyCodeList { get; } =
            new ObservableCollection<CurrencyPreviewDto>();

        #region SelectedCurrency: CurrencyPreviewDto

        public CurrencyPreviewDto SelectedCurrency
        {
            get => _selectedCurrency;
            set => SetProperty(ref _selectedCurrency, value);
        }

        private CurrencyPreviewDto _selectedCurrency;

        #endregion
    }
}