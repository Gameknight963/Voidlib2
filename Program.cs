using MelonLoader;
using VoidLib2;
[assembly: MelonInfo(typeof(Program), "Voidlib2", "2.0.0", "gameknight963")]

namespace VoidLib2
{
    internal class Program : MelonMod
    {
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName != "Version 1.9 POST") return;
            ExitDoor.UpdateCachedGameObjects();
        }
    }
}
