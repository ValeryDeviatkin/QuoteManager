using Unity;

namespace QuotesManager.Interfaces
{
    public interface IModuleInitializer
    {
        void Init(IUnityContainer container);
    }
}