using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;
using Inventory;
using Inventory.Model;


namespace DesignPattern.State
{
   public class BattleSystem : MonoBehaviour
   {
       [field: SerializeField] public BattleHUD battleHUD;
       [field: SerializeField] public BattleDialogBox dialogBox;
       
       /*
        * Pokemon that are fighting
        */
       [field: SerializeField] public PokemonSO playerPokemon;
       [field: SerializeField] public WildPokemonSO wildPokemon;
       
       // STATE
       private BattleStateMachine battleStateMachine_;
       public BattleStateMachine BattleStateMachine => battleStateMachine_;

       [field: SerializeField] public InventoryController inventoryController;

       public bool combatEnded = false;


       

       private void Awake()
       {
           // STATE
           battleStateMachine_ = new BattleStateMachine(this);
           inventoryController = FindObjectOfType<InventoryController>();

           //this.gameObject.SetActive(false); 
       }
  
       private void Update()
       {
           battleStateMachine_.Update();
       }
       
       
       /**
       * Initialize the UI for the battle to come
       */
    public void SetUpBattle()
    {
        dialogBox.ToggleAction(false);
        dialogBox.ToggleDialogText(false);
        
        this.gameObject.SetActive(true); 
        battleHUD.SetData(playerPokemon, wildPokemon);
        battleStateMachine_.Initialize(battleStateMachine_.startState);

        wildPokemon.Initialization();
    }


       /**
       * Initialize the UI for the battle to come
       */
       public void OutBattle()
       {
            this.gameObject.SetActive(false); 
       }
   }
}
