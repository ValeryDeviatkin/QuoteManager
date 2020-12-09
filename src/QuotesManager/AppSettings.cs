using System;
using System.Configuration;
using QuotesManager.Constants;
using QuotesManager.Interfaces;
using Unity;

namespace QuotesManager
{
    internal class AppSettings : ICurrencySourceUrlProvider
    {
        public AppSettings(IUnityContainer container)
        {
            container
               .RegisterInstance(this)
               .RegisterInstance<ICurrencySourceUrlProvider>(this)
                ;
        }

        public string[] CurrencyToCalculateIdList { get; private set; }

        public string CurrencySourceUrl { get; private set; }

        public void Init()
        {
            CurrencySourceUrl = ReadSetting(SettingsKeys.CurrencySourceUrl);

            CurrencyToCalculateIdList = ReadSetting(SettingsKeys.CurrencyToCalculateIdList)
               .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        }

        private static string ReadSetting(string settingName) => ConfigurationManager.AppSettings[settingName];
    }
}