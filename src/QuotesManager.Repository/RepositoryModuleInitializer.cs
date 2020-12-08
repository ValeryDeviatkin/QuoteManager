using QuotesManager.Interfaces;
using QuotesManager.Repository.Interfaces;
using QuotesManager.Repository.Services;
using Unity;

namespace QuotesManager.Repository
{
    public class RepositoryModuleInitializer : IModuleInitializer
    {
        public void Init(IUnityContainer container)
        {
            container
               .RegisterType<ICurrencyRepository, JsonWebCurrencyRepository>()
                ;
        }

        #region singleton

        private RepositoryModuleInitializer()
        {
        }

        public static RepositoryModuleInitializer Instance { get; } = new RepositoryModuleInitializer();

        #endregion
    }
}