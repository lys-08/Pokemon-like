using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;


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

       public bool combatEnded = false;


       

       private void Awake()
       {
           // STATE
           battleStateMachine_ = new BattleStateMachine(this);
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
       }
   }
}
