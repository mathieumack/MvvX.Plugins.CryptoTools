using MvvmCross;
using MvvmCross.Plugin;

namespace MvvX.Plugins.CryptoTools
{
    [MvxPlugin]
    [Preserve(AllMembers = true)]
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.IoCProvider.RegisterSingleton<ICryptoTools>(new CryptoTools());
        }
    }
}
