using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;
using MvvX.Plugins.CryptoTools.Platform;

namespace MvvX.Plugins.CryptoTools.Touch
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.RegisterSingleton<ICryptoTools>(new PlatformCryptoTools());
        }
    }
}
