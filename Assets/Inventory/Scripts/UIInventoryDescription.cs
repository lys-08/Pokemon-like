using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryDescription : MonoBehaviour
    {
        [SerializeField] private Image itemImage_;
        [SerializeField] private TextMeshProUGUI title_;
        [SerializeField] private TextMeshProUGUI description_;


        private void Awake()
        {
            ResetDescription();
        }

        public void ResetDescription()
        {
            /*
             * We disable the image so the quantity is also disabled
             */
            this.itemImage_.gameObject.SetActive(false);
            this.title_.text = "";
            this.description_.text = "";
        }

        /**
         * Methods that print the description of the clicked item
         */
        public void SetDescription(Sprite sprite, string itemName, string itemDescription)
        {
            this.itemImage_.gameObject.SetActive(true);
            this.itemImage_.sprite = sprite;
            this.title_.text = itemName;
            this.description_.text = itemDescription;
        }
    }

}