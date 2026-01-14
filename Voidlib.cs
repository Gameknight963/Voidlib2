using Il2Cpp;
using Il2CppInterop;
using MelonLoader;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace VoidLib
{
    public class PauseMenu : MelonMod
    {
        public enum ButtonType
        {
            SmallL,
            SmallR,
            Big
        }

        static public GameObject AddMenuButton(float posY, ButtonType buttonType, string text, string name)
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
            GameObject mainMenu = GameObject.Find("Menu");

            RectTransform RectTransform = null;
            GameObject newButton = null;
            if (buttonType == ButtonType.SmallL)
            {
                newButton = UnityEngine.Object.Instantiate(GameObject.Find("Return"), mainMenu.transform);
            }
            else if (buttonType == ButtonType.SmallR)
            {
                newButton = UnityEngine.Object.Instantiate(GameObject.Find("Return"), mainMenu.transform);
                RectTransform = newButton.GetComponent<RectTransform>();
                RectTransform.anchoredPosition = new Vector2(20f, RectTransform.anchoredPosition.y);
            }
            else if (buttonType == ButtonType.Big)
            {
                newButton = UnityEngine.Object.Instantiate(GameObject.Find("Resume"), mainMenu.transform);
            }

            GameObject newButtonFrame = newButton.transform.Find("Frame").gameObject;
            RectTransform frameRectTransform = newButtonFrame.GetComponent<RectTransform>();
            GameObject newButtonText = newButton.transform.Find("Text").gameObject;

            if (RectTransform == null)
            {
                RectTransform = newButton.GetComponent<RectTransform>();
                RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, posY);
            }

            if (text != null)
            {
                Text textAsset = newButtonText.GetComponent<Text>();
                textAsset.m_Text = text;
            }
            if (name != null) { newButton.name = name; }

            UIButtonCore buttonScript = newButton.GetComponent<UIButtonCore>();
            // replace the event with our own
            buttonScript.onClick = new UnityEngine.Events.UnityEvent();
            // you can disable the listeners instead, but the above line makes sure they're fully gone
            //buttonScript.onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            //buttonScript.onClick.SetPersistentListenerState(1, UnityEngine.Events.UnityEventCallState.Off);

            return newButton;
        }

        // Feature not implemented
        //static public GameObject AddMenuGroup()
    }
}
