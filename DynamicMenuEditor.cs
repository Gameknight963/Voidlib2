using Il2Cpp;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace VoidLib2
{
    public static class DynamicMenuEditor
    {
        private static GameObject? returnButton;
        private static GameObject? mainMenu;
        private static GameObject? resumeButton;

        public static bool UpdateCachedGameObjects()
        {
            returnButton = GameObject.Find("Return");
            mainMenu = GameObject.Find("Menu");
            resumeButton = GameObject.Find("Resume");
            if (returnButton == null || mainMenu == null || resumeButton == null) return false;
            return true;
        }

        public enum ButtonType
        {
            SmallL,
            SmallR,
            Big
        }

        /// <summary>Adds a new menu button</summary>
        /// <param name="posY">The Y position (in the menu's plane) of the button to be created.</param>
        /// <param name="buttonType">Which kind of button will be created.</param>
        /// <param name="buttonText">The text the button will display.</param>
        /// <param name="name">The name of the GameObject (button) that will be created.</param>
        /// <param name="resultObject">The resulting GameObject.</param>
        /// <returns>Whether the operation was successful or not.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        static public bool AddMenuButton(float posY, 
            ButtonType buttonType, 
            string buttonText, 
            string name, 
            out GameObject? resultObject)
        {
            resultObject = null;
            if (SceneManager.GetActiveScene().name != "Version 1.9 POST"
                || SceneManager.GetActiveScene().name != "Sample Level Setup") return false;

            if (returnButton == null || mainMenu == null || resumeButton == null)
            {
                UpdateCachedGameObjects();
                if (mainMenu == null) return false;
            }

            RectTransform RectTransform;
            GameObject newButton;

            switch (buttonType)
            {
                case ButtonType.SmallL:
                    if (mainMenu == null) return false;
                    newButton = UnityEngine.Object.Instantiate(
                    mainMenu, mainMenu.transform);
                    RectTransform = newButton.GetComponent<RectTransform>();
                    RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, posY);
                    break;

                case ButtonType.SmallR:
                    if (returnButton == null) return false;
                    newButton = UnityEngine.Object.Instantiate(
                    returnButton, mainMenu.transform);
                    RectTransform rectTransform = newButton.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = new Vector2(20f, rectTransform.anchoredPosition.y);
                    break;

                case ButtonType.Big:
                    if (returnButton == null) return false;
                    newButton = UnityEngine.Object.Instantiate(
                    returnButton, mainMenu.transform);
                    RectTransform = newButton.GetComponent<RectTransform>();
                    RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, posY);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonType));
            }

            Text text = newButton.FindFirstChild("Text").GetComponent<Text>();
            text.text = buttonText;
            
            newButton.name = name;
            newButton.GetComponent<UIButtonCore>().onClick = new UnityEngine.Events.UnityEvent();
            
            resultObject = newButton;
            return true;
        }
        
        //static public GameObject AddMenuGroup()
        //{
        //    throw new NotImplementedException("AddMenuGroup(): Feature not implemented");
        //}
        //static public GameObject AddMenuSlider()
        //{
        //    throw new NotImplementedException("AddMenuSlider(): Feature not implemented");
        //}
        //static public GameObject AddMenuCheckbox()
        //{
        //    throw new NotImplementedException("AddMenuCheckbox(): Feature not implemented");
        //}
    }
}
