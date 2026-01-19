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
    public class DynamicMenuEditor : MelonMod
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
            /// <returns>The new menu button GameObject. If not called while Version 1.9 POST is active, throws an exception.</returns>
            if (SceneManager.GetActiveScene().name != "Version 1.9 POST")
            {
                throw new InvalidOperationException("Version 1.9 POST is not active!");
            }
            if (text == null)
            {
                throw new ArgumentException("Text cannot be null");
            }
            GameObject mainMenu = GameObject.Find("Menu");

            RectTransform RectTransform = null;
            GameObject newButton = null;
            switch (buttonType)
            {
                case ButtonType.SmallL:
                    newButton = UnityEngine.Object.Instantiate(
                    GameObject.Find("Return"), mainMenu.transform);
                    RectTransform = newButton.GetComponent<RectTransform>();
                    RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, posY);
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
                    RectTransform = newButton.GetComponent<RectTransform>();
                    RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, posY);
                    break;
            }

            GameObject newButtonFrame = newButton.transform.Find("Frame").gameObject;
            RectTransform frameRectTransform = newButtonFrame.GetComponent<RectTransform>();
            GameObject newButtonText = newButton.transform.Find("Text").gameObject;
            Text textAsset = newButtonText.GetComponent<Text>();
            textAsset.m_Text = text;
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
        static SettingsManager settingsManager = null;
        GameObject prefs;
        public enum GameSetting
        {
            fov,
            sensitivity,
        }
        public static float GetSetting(GameSetting setting)
        {
            settingsManager = GameObject.Find("PlayerPrefs").GetComponent<SettingsManager>();

            if (settingsManager == null)
            {
                throw new InvalidOperationException("SettingsManager not found!");
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
        public static void ChangeSetting(GameSetting setting, float value)
        {
            settingsManager = GameObject.Find("PlayerPrefs").GetComponent<SettingsManager>();
            if (settingsManager == null)
            {
                throw new InvalidOperationException("SettingsManager not found!");
            }
            switch (setting)
            {
                case GameSetting.fov:
                    settingsManager.fov = value;
                    return;
                case GameSetting.sensitivity:
                    settingsManager.sensitivity = value;
                    return;
                throw new ArgumentException($"GameSetting {setting} is invalid");
            }
        }
    }
    public class WorldActions : MelonMod
    {
        public static void SetExitDoorEnabled(bool state)
        {
            GameObject.Find("World/House/Doors/ExitFrame/ExitDoor").SetActive(state);
            GameObject.Find("World/Game/Acts/Hello Mita/Interactables 1/I Exit Door 1 ").SetActive(state);
            GameObject.Find("World/Game/Acts/Quality Time/Interactables 2/I ExitDoor 2").SetActive(state);
        }
        public static void SetExitDoorCollison(bool state)
        {
            GameObject.Find("World/House/Doors/ExitFrame/ExitDoor").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("World/Game/Acts/Hello Mita/Interactables 1/I Exit Door 1 ").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("World/Game/Acts/Quality Time/Interactables 2/I ExitDoor 2").GetComponent<BoxCollider>().enabled = false;
        }
    }
    public static class ShaderUtils
    {
        /// <summary>
        /// Sets the shader of all MeshRenderers in the GameObject and its children.
        /// </summary>
        /// <param name="root">The root GameObject.</param>
        /// <param name="shader">The shader to assign.</param>
        public static void SetShaderRecursively(GameObject root, Shader shader)
        {
            if (root == null || shader == null) return;

            MeshRenderer[] renderers = root.GetComponentsInChildren<MeshRenderer>(true);

            foreach (MeshRenderer mr in renderers)
            {
                Material[] mats = mr.materials;
                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i].shader = shader;
                }
            }
        }
    }
}
