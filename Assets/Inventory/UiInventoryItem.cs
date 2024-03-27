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

    public event Action<UiInventoryItem> OnItemClicked, OnItemDroppedOn, OnRightMouseButtonClick;

    private bool empty_ = true;


    private void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        this.itemImage_.gameObject.SetActive(false);
        empty_ = true;
    }

    public void Deselect()
    {
        borderImage_.enabled = false;
    }

    public void SetData(Sprite sprite, int quantity)
    {
        this.itemImage_.gameObject.SetActive(true);
        this.itemImage_.sprite = sprite;
        this.quantityText_.text = quantity + "";
        empty_ = false;
    }

    public void Select()
    {
        borderImage_.enabled = true;
    }

    public void OnPointerClick(BaseEventData data)
    {
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
}
