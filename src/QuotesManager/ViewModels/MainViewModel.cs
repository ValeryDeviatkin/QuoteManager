using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
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

        public ObservableCollection<CurrencyInfoDto> FoundCurrencyCollection { get; } =
            new ObservableCollection<CurrencyInfoDto>();

        public ObservableCollection<CurrencyPreviewDto> CurrencyPreviewCollection { get; } =
            new ObservableCollection<CurrencyPreviewDto>();

        #region SelectedCurrency: CurrencyPreviewDto

        public CurrencyPreviewDto SelectedCurrency
        {
            get => _selectedCurrency;
            set => SetProperty(ref _selectedCurrency, value, OnSelectedCurrencyChanged);
        }

        private void OnSelectedCurrencyChanged()
        {
            if (SelectedCurrency != null)
            {
                Commands.RefreshCurrencyCommand.Execute(null);
            }
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

        #region SelectedCurrencyInfo: CurrencyInfoDto

        public CurrencyInfoDto SelectedCurrencyInfo
        {
            get => _selectedCurrencyInfo;
            set => SetProperty(ref _selectedCurrencyInfo, value);
        }

        private CurrencyInfoDto _selectedCurrencyInfo;

        #endregion

        #region SearchString: string

        public string SearchString
        {
            get => _searchString;
            set => SetProperty(ref _searchString, value);
        }

        private string _searchString;

        #endregion

        #region SwapConvertingCurrencies command

        public ICommand SwapConvertingCurrenciesCommand => _swapConvertingCurrenciesCommand ??=
                                                               new Command(ExecuteSwapConvertingCurrencies);

        private Command _swapConvertingCurrenciesCommand;

        private async Task ExecuteSwapConvertingCurrencies(object parameter)
        {
            var tmp = SourceConvertingCurrency;
            SourceConvertingCurrency = TargetConvertingCurrency;
            TargetConvertingCurrency = tmp;

            await Task.Delay(0);
        }

        #endregion
    }
}