using MvvmCross;
using MvvmCross.Plugin;

namespace MvvX.Plugins.CryptoTools
{
    [MvxPlugin]
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.IoCProvider.RegisterSingleton<ICryptoTools>(new PlatformCryptoTools());
        }
    }
}
