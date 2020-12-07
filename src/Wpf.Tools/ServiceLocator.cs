using Unity;

namespace Wpf.Tools
{
    public static class ServiceLocator
    {
        public static IUnityContainer Container { get; } = new UnityContainer();
    }
}