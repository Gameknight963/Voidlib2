using Il2Cpp;
using Il2CppInterop;
using MelonLoader;
using System;
using System.CodeDom;
using System.Configuration;
using System.Runtime.InteropServices.WindowsRuntime;
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
            if (SceneManager.GetActiveScene().name != "Version 1.9 POST")
            {
                throw new InvalidOperationException("Version 1.9 POST is the active scene!");
            }
            GameObject mainMenu = GameObject.Find("Menu");

            RectTransform RectTransform = null;
            GameObject newButton = null;
            switch (buttonType)
            {
                case ButtonType.SmallL:
                    newButton = UnityEngine.Object.Instantiate(
                        GameObject.Find("Return"), mainMenu.transform);
                    break;

                case ButtonType.SmallR:
                    newButton = UnityEngine.Object.Instantiate(
                        GameObject.Find("Return"), mainMenu.transform);
                    RectTransform rectTransform = newButton.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = new Vector2(20f, rectTransform.anchoredPosition.y);
                    break;

                case ButtonType.Big:
                    newButton = UnityEngine.Object.Instantiate(
                        GameObject.Find("Resume"), mainMenu.transform);
                    break;
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

        //TODO: add sliders and group support
        static public GameObject AddMenuGroup()
        {
            throw new NotImplementedException("AddMenuGroup(): Feature not implemented");
        }
        static public GameObject AddMenuSlider()
        {
            throw new NotImplementedException("AddMenuSlider(): Feature not implemented");
        }
        static public GameObject AddMenuCheckbox()
        {
            throw new NotImplementedException("AddMenuCheckbox(): Feature not implemented");
        }
    }
    public class Player : MelonMod
    {
        SettingsManager settingsManager = null;
        public enum GameSetting
        {
            fov,
            sensitivity,
        }
        public object GetSetting(GameSetting setting)
        {
            if (settingsManager == null)
            {

            }
            switch (setting)
            {
                case GameSetting.fov:
                    return settingsManager.fov;
                case GameSetting.sensitivity:
                    return settingsManager.sensitivity;
            }
            throw new ArgumentException($"GameSetting {setting} is invalid, idk how you broke an enum but you did");
        }
        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName == "Version 1.9 POST")
            {
                settingsManager = GameObject.Find("PlayerPrefs").GetComponent<SettingsManager>();
            }
        }
    }
}
