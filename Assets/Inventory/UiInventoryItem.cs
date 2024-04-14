using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiInventoryItem : MonoBehaviour
{
    [SerializeField] private Image itemImage_;
    [SerializeField] private TextMeshProUGUI quantityText_;
    [SerializeField] private Image borderImage_;
    
    
    public event Action<UiInventoryItem> 
        OnItemClicked, // Left click => selection of the item to print it's description
        OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, 
        OnRightMouseButtonClick; // Right click => show what we can do (main pokemon, use potion, free, ...)

    private bool empty_ = true;


    private void Awake()
    {
        ResetData();
        Deselect();
    }
    
    public void ResetData()
    {
        /*
         * We desable the image so the quantity is also disabled
         */
        this.itemImage_.gameObject.SetActive(false);
        empty_ = true;
    }

    /**
     * Methods that deselect an item (so it's border is no longer visible)
     */
    public void Deselect()
    {
        borderImage_.enabled = false;
    }
    
    /**
     * Methods that select an item (so it's border is visible)
     */
    public void Select()
    {
        borderImage_.enabled = true;
    }

    
    /**
     * Methods that set the data of our inventory item added to the inventory
     */
    public void SetData(Sprite sprite, int quantity)
    {
        this.itemImage_.gameObject.SetActive(true);
        this.itemImage_.sprite = sprite;
        this.quantityText_.text = quantity + "";
        empty_ = false;
    }
    
    /**
     * Methods that inform our inventory page that an item has been clicked (left or right click)
     */
    public void OnPointerClick(BaseEventData data)
    {
        if (empty_) return;
        
        PointerEventData pointerData = (PointerEventData)data;

        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseButtonClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    /**
     * Methods that inform our inventory page that an item is being draged
     */
    public void OnBeginDrag()
    {
        if (empty_) return;
        OnItemBeginDrag?.Invoke(this);
    }
    
    /**
     * Methods that inform our inventory page that an item is being droped
     */
    public void OnDrop()
    {
        OnItemDroppedOn?.Invoke(this);
    }
    
    /**
     * Methods that inform our inventory page that a drag item has been droped
     * -> if the item is drop outside the inventory, we can reset parameters
     */
    public void OnEndDrag()
    {
        OnItemEndDrag?.Invoke(this);
    }
}
