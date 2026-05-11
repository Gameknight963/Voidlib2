using UnityEngine;

namespace VoidLib2
{
    public static class ExitDoor
    {
        private static GameObject? exitDoor => GameObject.Find("World/House/Doors/ExitFrame/ExitDoor");
        private static GameObject? IExitDoor1 => GameObject.Find("World/Game/Acts/Hello Mita/Interactables 1/I Exit Door 1 ");
        private static GameObject? IExitDoor2 => GameObject.Find("World/Game/Acts/Quality Time/Interactables 2/I ExitDoor 2");

        public static bool SetEnabled(bool state)
        {
            if (exitDoor is null || IExitDoor1 is null || IExitDoor2 is null) return false;
            exitDoor.GetComponent<MeshRenderer>().enabled = state;
            return SetCollison(state);
        }
        public static bool SetCollison(bool state)
        {
            if (exitDoor is null || IExitDoor1 is null || IExitDoor2 is null) return false;
            exitDoor.GetComponent<BoxCollider>().enabled = false;
            IExitDoor1.GetComponent<BoxCollider>().enabled = false;
            IExitDoor2.GetComponent<BoxCollider>().enabled = false;
            return true;
        }
    }
}
