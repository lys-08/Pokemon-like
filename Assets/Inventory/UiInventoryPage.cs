using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInventoryPage : MonoBehaviour
{
    [SerializeField] private UiInventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;

    private List<UiInventoryItem> listUiItems = new List<UiInventoryItem>();


    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UiInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listUiItems.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnRightMouseButtonClick += HandleShowItemActions;
        }
    }

    private void HandleItemSelection(UiInventoryItem obj)
    {
        Debug.Log(obj.name);
    }
    
    private void HandleSwap(UiInventoryItem obj)
    {
        
    }
    
    private void HandleShowItemActions(UiInventoryItem obj)
    {
        
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
