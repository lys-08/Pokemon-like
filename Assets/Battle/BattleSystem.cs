using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private BattleHUD battleHUD;
    
    /*
     * TODO : temporary
     */
    public PokemonSO p1;
    public PokemonSO p2;


    #region Unity Events Methods

    private void Start()
    {
        SetUpBattle();
    }

    #endregion
    
    
    /**
      * Initialize the UI for the battle to come
      */
    public void SetUpBattle()
    {
        battleHUD.SetData(p1, p2);
    }
}
