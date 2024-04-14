using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInventoryPage : MonoBehaviour
{
    [SerializeField] private UiInventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private UIInventoryDescription itemDescription;

    /*
     * Reference to all item created
     */
    private List<UiInventoryItem> listUiItems = new List<UiInventoryItem>();

    // TODO : temporary variable
    public Sprite image;
    public int quantity;
    public string title, description;


    private void Awake()
    {
        Hide();
        itemDescription.ResetDescription();
    }

    /**
     * Methods to initialize our inventory with a given size
     *
     * @param inventorySize the size of the inventory
     */
    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UiInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listUiItems.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseButtonClick += HandleShowItemActions;
        }
    }

    private void HandleItemSelection(UiInventoryItem obj)
    {
        itemDescription.SetDescription(image, title, description);
    }

    private void HandleBeginDrag(UiInventoryItem obj)
    {
        
    }
    
    private void HandleSwap(UiInventoryItem obj)
    {
        
    }

    private void HandleEndDrag(UiInventoryItem obj)
    {
        
    }
    
    private void HandleShowItemActions(UiInventoryItem obj)
    {
        
    }

    /**
     * Methods that show the inventory page
     */
    public void Show()
    {
        gameObject.SetActive(true);
        // To be sure nothing is printed when we have select something in the previous time we had had shown the inventory
        itemDescription.ResetDescription(); 
        
        listUiItems[0].SetData(image, quantity);
    }
    
    /**
     * Methods that hides the inventory page
     */
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
