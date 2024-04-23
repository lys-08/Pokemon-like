using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum BattleStates { StartState, PlayerMove, EnemyMove, Busy, EndState}

public class BattleSystem : MonoBehaviour
{
    [field: SerializeField] public BattleHUD battleHUD;
    [SerializeField] private BattleDialogBox dialogBox;

    private BattleStates state;
    
    /*
     * TODO : temporary (public -> private)
     */
    public PokemonSO playerPokemon;
    public PokemonSO wildPokemon;


    #region Unity Events Methods

    private void Start()
    {
        StartCoroutine(SetUpBattle());

        foreach (BattleAction action in dialogBox.actions.gameObject.GetComponentsInChildren<BattleAction>())
        {
            action.OnItemClicked += OnRequestedAction;
        }
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    private void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        
        if (state == BattleStates.PlayerMove)
        {
            PlayerMove();
            state = BattleStates.Busy;
        }
        
        else if (state == BattleStates.EnemyMove)
        {
            
        }
        
        else if (state == BattleStates.Busy)
        {
            return;
        }
        
        else if (state == BattleStates.EndState)
        {
            playerPokemon.ResetCoeffs();
            Destroy(wildPokemon.GameObject());
            Debug.Log("TODO : back in game");
        }

        else // state == BattleStates.Start
        {
            
        }
    }

    #endregion


    /**
     * Set the pokemon who are fighting
     */
    public void SetFightingPokemon(PokemonSO playerPokemon, PokemonSO wildPokemon)
    {
        this.playerPokemon = playerPokemon;
        this.wildPokemon = wildPokemon;
    }
    
    /**
      * Initialize the UI for the battle to come
      */
    private IEnumerator SetUpBattle()
    {
        Debug.Log("setUp");
        battleHUD.SetData(playerPokemon, wildPokemon);

        yield return dialogBox.TypeDialog($"A wild <i>{wildPokemon.name}</i> appeared.");
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("end");
                PlayerMove();
                yield break;
            }
            yield return null;
        }
        
    }

    private void PlayerMove()
    {
        //StartCoroutine(dialogBox.TypeDialog($"Choose an action."));
        dialogBox.ToggleDialogText(false);
        dialogBox.ToggleAction(true);
    }

    private IEnumerator PerformPlayerAction(string action)
    {
        dialogBox.ToggleAction(false);
        dialogBox.ToggleDialogText(true);
        StartCoroutine(dialogBox.TypeDialog($"<i>{playerPokemon.name}</i> used {action}."));
        
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("end PERFORM");
                //PlayerMove();
                yield break;
            }
            yield return null;
        }
    }
    
    
    /**
     * Show the battle page
     */
    public void Show(PokemonSO playerPokemon, PokemonSO wildPokemon)
    {
        gameObject.SetActive(true);
        this.playerPokemon = playerPokemon;
        this.wildPokemon = wildPokemon;
    }
        
    /**
     * Hide the battle page
     */
    public void Hide()
    {
        gameObject.SetActive(false);
    }


    #region Action

    private void OnRequestedAction(BattleAction obj)
    {
        switch (obj.name)
        {
            case ("Fight"):
                Debug.Log("Fight");
                wildPokemon.TakeDamage(playerPokemon.GetDamage(), playerPokemon.type);
                break;
            case ("Distract"):
                Debug.Log("Distract");
                wildPokemon.TakeDistraction();
                break;
            case ("Focus"):
                Debug.Log("Focus");
                playerPokemon.TakeFocus();
                break;
            case ("Heal"):
                Debug.Log("Heal : TODO");
                break;
            case ("Capture"):
                Debug.Log("Capture : TODO");
                break;
            case ("Run"):
                Debug.Log("Run");
                state = BattleStates.EndState;
                break;
        }

        StartCoroutine(PerformPlayerAction(obj.name));
    }

    #endregion
}
