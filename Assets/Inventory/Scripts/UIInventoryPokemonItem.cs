using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryPokemonItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler,
        IDropHandler, IDragHandler
    {
        [SerializeField] private Image itemImage_;
        [SerializeField] private Image borderImage_;
        [SerializeField] private GameObject mainText_;


        public event Action<UIInventoryPokemonItem>
            OnItemClicked, // Left click => selection of the item to print it's description
            OnItemDroppedOn,
            OnItemBeginDrag,
            OnItemEndDrag;
        
        private bool empty_ = true;



        #region Unity Event Methods

        private void Awake()
        {
            ResetData();
            Deselect();
        }

        #endregion


        public void ResetData()
        {
            this.itemImage_.gameObject.SetActive(false);
            empty_ = true;
        }

        /**
         * Set the data of our inventory item added to the inventory
         */
        public void SetData(Sprite sprite)
        {
            this.itemImage_.gameObject.SetActive(true);
            this.itemImage_.sprite = sprite;
            empty_ = false;
        }

        /**
         * Deselect an item (so it's border is no longer visible)
         */
        public void Deselect()
        {
            borderImage_.enabled = false;
        }

        /**
         * Select an item (so it's border is visible)
         */
        public void Select()
        {
            borderImage_.enabled = true;
        }

        /**
         * Set the pokemon as the main pokemon
         */
        public void SetMainPokemon()
        {
            mainText_.SetActive(true);
        }

        /**
         * Deselect the current main pokemon
         */
        public void ResetMainPokemon()
        {
            mainText_.SetActive(false);
        }


        #region IPointerClickHandler

        /**
         * Inform our inventory page that an item has been clicked (left or right click)
         */
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnItemClicked?.Invoke(this);
            }
        }

        #endregion

        #region IBeginDragHandler

        /**
         * Methods called when an item is being dragged (the drag is started)
         */
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty_) return;
            OnItemBeginDrag?.Invoke(this);
        }

        #endregion

        #region IEndDragHandler

        /**
         * Methods called when a drag item has been dropped (the drag is ended)
         * -> if the item is drop outside the inventory, we can reset parameters
         */
        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        #endregion

        #region IDropHandler

        /**
         * Methods called when an item is being dropped on a target that can accept the drop
         */
        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        #endregion

        #region IDragHandler

        public void OnDrag(PointerEventData eventData)
        {

        }

        #endregion
    }
}
