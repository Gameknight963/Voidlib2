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
            /// <param name="posY">The Y position in the menu's plane) of the button to be returned.</param>
            /// <param name="buttonType">The ButtonType of the button returned. Use the ButtonType enum.</param>
            /// <param name="text">The text of the button returned.</param>
            /// <param name="name">The name of the button returned.</param>
            /// <returns>The new menu button GameObject. If not called while Version 1.9 POST is active, returns null.</returns>
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
            newButton.name = name;
            UIButtonCore buttonScript = newButton.GetComponent<UIButtonCore>();
            buttonScript.onClick = new UnityEngine.Events.UnityEvent();
            return newButton;
        }

        // Features not implemented
        //static public GameObject AddMenuGroup()
        //static public GameObject AddMenuSlider()
    }
}
