using MelonLoader;
using Il2Cpp;
using Il2CppInterop;
using UnityEngine;
using UnityEngine.SceneManagement;
using Il2CppInterop.Runtime.Runtime;

namespace VoidLib
{
    public class PauseMenu : MelonMod
    {

        static public GameObject AddMenuButton(Vector2 pos2d, Vector2 scale, string text, string name)
        {
            /// <summary>Adds a new menu button</summary>
            /// <param name="pos2d">The position of the button returned in the menu's plane.</param>
            /// <param name="scale">The size of the button returned.</param>
            /// <param name="text">The text of the button returned.</param>
            /// <param name="name">The name of the button returned.</param>
            /// <returns>The new menu button GameObject. If not called while Version 1.9 POST is active, returns null.</returns>
            if (SceneManager.GetActiveScene().name != "Version 1.9 POST") { return null; }
            GameObject exitButton = GameObject.Find("Return");
            GameObject mainMenu = GameObject.Find("Menu");
            if (exitButton == null)
            {
                MelonLogger.Error("Return button not found! Adding new button fails");
                return null;
            }

            GameObject newButton = Object.Instantiate(exitButton, mainMenu.transform);
            GameObject newButtonFrame = newButton.transform.Find("Frame").gameObject;
            GameObject newButtonText = newButton.transform.Find("Text").gameObject;
            UIButtonCore UICore = newButton.GetComponent<UIButtonCore>();
            RectTransform RectTransform = newButton.GetComponent<RectTransform>();
            UICore.onClick.RemoveAllListeners();
            if (pos2d != null) { RectTransform.anchoredPosition = pos2d; }
            if (scale != null)
            {
                RectTransform.localScale = new Vector3(scale.x, scale.y, RectTransform.localScale.z);
                RectTransform.anchoredPosition = new Vector2(
                    RectTransform.anchoredPosition.x + scale.x/2,
                    RectTransform.anchoredPosition.y);
            }


            return newButton;
        }

        // Feature not implemented
        //static public GameObject AddMenuGroup()
    }
}
