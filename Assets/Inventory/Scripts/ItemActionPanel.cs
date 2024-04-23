using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;


namespace Inventory.UI
{
    public class ItemActionPanel : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;


        /**
         * Add a button and the on click action associated in the panel
         */
        public void AddButton(string name, System.Action onClickAction)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            button.GetComponent<Button>().onClick.AddListener(() => onClickAction()); // We set the action to apply when the button is cliqued
            button.GetComponentInChildren<TextMeshProUGUI>().text = name; // We set the name of the button
        }
        
        /**
         * Make the panel appear or disappear
         * -> If the panel is appearing, we first remove all buttons
         */
        public void Toggle(bool value)
        {
            if (value) RemoveOldButtons();
            gameObject.SetActive(value);
        }

        /**
         * Remove all the buttons in the panels
         */
        private void RemoveOldButtons()
        {
            foreach (Transform transformChildObj in transform)
            {
                Destroy(transformChildObj.gameObject);
            }
        }
    }
}
