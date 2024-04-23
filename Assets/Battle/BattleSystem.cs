using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


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
    public WildPokemonSO wildPokemon;


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

        if (state == BattleStates.EndState)
        {
            Debug.Log("Battle State : End State");
        }
    }

    #endregion


    /**
     * Set the pokemon who are fighting
     */
    public void SetFightingPokemon(PokemonSO playerPokemon, WildPokemonSO wildPokemon)
    {
        this.playerPokemon = playerPokemon;
        this.wildPokemon = wildPokemon;
    }
    
    /**
      * Initialize the UI for the battle to come
      */
    private IEnumerator SetUpBattle()
    {
        battleHUD.SetData(playerPokemon, wildPokemon);

        yield return dialogBox.TypeDialog($"A wild <i>{wildPokemon.name}</i> appeared.");
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                PlayerMove();
                yield break;
            }
            yield return null;
        }
        
    }

    /**
     * Start Player Turn
     */
    private void PlayerMove()
    {
        Debug.Log("Busy");
        state = BattleStates.Busy;
        //StartCoroutine(dialogBox.TypeDialog($"Choose an action."));
        dialogBox.ToggleDialogText(false);
        dialogBox.ToggleAction(true);
    }

    /**
     * Enemy Turn
     */
    private void EnemyMove()
    {
        state = BattleStates.EnemyMove;
        
        // TODO : Coefs
        Debug.Log($"run {wildPokemon.runCoeff_} fight {wildPokemon.attackCoeff_} disctrac {wildPokemon.distractCoeff_} focus {wildPokemon.focusCoeff_}"); ;

        string rdAction = "";
        switch (Random.Range(0, 1))
        {
            case (0):
                rdAction = "Fight";
                playerPokemon.TakeDamage(wildPokemon.GetDamage(), wildPokemon.type);
                break;
            case (1):
                rdAction = "Fight";
                playerPokemon.TakeDistraction();
                break;
            case (2):
                rdAction = "Focus";
                wildPokemon.Focus();
                break;
            case (3):
                rdAction = "Run";
                // TODO run message
                state = BattleStates.EndState;
                break;
        }
        Debug.Log($"action made : {rdAction}");
        StartCoroutine(PerformEnemyAction(rdAction));
    }

    private IEnumerator PerformEnemyAction(string action)
    {
        StartCoroutine(dialogBox.TypeDialog($"1 {wildPokemon.name} used {action}."));
        
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!playerPokemon.ko) PlayerMove();
                else state = BattleStates.EndState;
                yield break;
            }
            yield return null;
        }
    }
    

    private IEnumerator PerformPlayerAction(string action)
    {
        dialogBox.ToggleAction(false);
        dialogBox.ToggleDialogText(true);
        StartCoroutine(dialogBox.TypeDialog($"{playerPokemon.name} used {action}."));
        
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!wildPokemon.ko) EnemyMove();
                else state = BattleStates.EndState;
                //PlayerMove();
                yield break;
            }
            yield return null;
        }
    }
    
    
    /**
     * Show the battle page
     */
    public void Show(PokemonSO playerPokemon, WildPokemonSO wildPokemon)
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
                playerPokemon.Focus();
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
