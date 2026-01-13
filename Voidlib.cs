using MelonLoader;
using Il2Cpp;
using Il2CppInterop;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Il2CppInterop.Runtime.Runtime;

namespace VoidLib
{
    public class PauseMenu : MelonMod
    {

        static public GameObject AddMenuButton(Vector2 pos2d, float length, string text, string name)
        {
            /// <summary>Adds a new menu button</summary>
            /// <param name="pos2d">The position of the button returned in the menu's plane.</param>
            /// <param name="length">The length (x scale) of the button returned.</param>
            /// <param name="text">The text of the button returned.</param>
            /// <param name="name">The name of the button returned.</param>
            /// <returns>The new menu button GameObject. If not called while Version 1.9 POST is active, returns null.</returns>
            /// 
            // For refrence, the "Exit" button (named "Return") has these properties:
            // RectTransform.anchoredPosition = -305.0003f, -120.0001f
            // RectTransform.eulerAngles = 49.855f, 133.9941f, 355.2355f
            // Transform.localScale = 1.4955f, 0.8439f, 4.4859f
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
            RectTransform RectTransform = newButton.GetComponent<RectTransform>();

            //UIButtonCore UIButtonCore = newButton.GetComponent<UIButtonCore>();
            //if (UIButtonCore = null) { MelonLogger.Error("[Voidlib] UIButtonCore is null!"); }
            //UIButtonCore.onClick.RemoveAllListeners();

            if (pos2d != null) { RectTransform.anchoredPosition = pos2d; }

            RectTransform.localScale = new Vector3(length, RectTransform.localScale.y, RectTransform.localScale.z);
            RectTransform.anchoredPosition = new Vector2(
            RectTransform.anchoredPosition.x + length*50- 74.775f, // dont fucking touch these numbers
            RectTransform.anchoredPosition.y);


            if (text != null)
            {
                Text textAsset = newButtonText.GetComponent<Text>();
                textAsset.m_Text = text;
            }
            if (name != null) { newButton.name = name; }
            return newButton;
        }

        // Feature not implemented
        //static public GameObject AddMenuGroup()
    }
}
