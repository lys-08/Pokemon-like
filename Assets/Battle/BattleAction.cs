using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UIElements;
using System.Linq;

public class BattleAction : MonoBehaviour, IPointerClickHandler
{
    public event Action<BattleAction> OnItemClicked;
    

    #region IPointerClickHandler

    /**
     * Methods that inform our inventory page that an item has been clicked (left or right click)
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) return;
        OnItemClicked?.Invoke(this);
    }

    #endregion


    
}
