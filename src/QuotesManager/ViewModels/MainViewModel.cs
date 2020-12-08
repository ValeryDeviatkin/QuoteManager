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

        public ObservableCollection<CurrencyPreviewDto> CurrencyPreviewList { get; } =
            new ObservableCollection<CurrencyPreviewDto>();

        #region SelectedCurrency: CurrencyPreviewDto

        public CurrencyPreviewDto SelectedCurrency
        {
            get => _selectedCurrency;
            set => SetProperty(ref _selectedCurrency, value);
        }

        private CurrencyPreviewDto _selectedCurrency;

        #endregion

        #region SourceConvertingCurrency: CurrencyPreviewDto

        public CurrencyPreviewDto SourceConvertingCurrency
        {
            get => _sourceConvertingCurrency;
            set => SetProperty(ref _sourceConvertingCurrency, value);
        }

        private CurrencyPreviewDto _sourceConvertingCurrency;

        #endregion

        #region TargetConvertingCurrency: CurrencyPreviewDto

        public CurrencyPreviewDto TargetConvertingCurrency
        {
            get => _targetConvertingCurrency;
            set => SetProperty(ref _targetConvertingCurrency, value);
        }

        private CurrencyPreviewDto _targetConvertingCurrency;

        #endregion

        #region SourceConvertingValue: decimal

        public decimal SourceConvertingValue
        {
            get => _sourceConvertingValue;
            set => SetProperty(ref _sourceConvertingValue, value);
        }

        private decimal _sourceConvertingValue;

        #endregion

        #region TargetConvertingValue: decimal

        public decimal TargetConvertingValue
        {
            get => _targetConvertingValue;
            set => SetProperty(ref _targetConvertingValue, value);
        }

        private decimal _targetConvertingValue;

        #endregion
    }
}