using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace DesignPattern.State
{
    public class PlayerMoveState : IState
    {
        private BattleSystem battle;
    
        
        public PlayerMoveState(BattleSystem battle)
        {
            this.battle = battle;
        }
        
        
        private void OnRequestedAction(BattleAction obj)
        {
            battle.dialogBox.ToggleAction(true);
            battle.dialogBox.ToggleAction(false);
            
            switch (obj.name)
            {
                case ("Fight"):
                    battle.wildPokemon.TakeDamage(battle.playerPokemon.GetDamage(), battle.playerPokemon.type);
                    battle.StartCoroutine(PerformAction("Fight"));
                    break;
                case ("Distract"):
                    battle.wildPokemon.TakeDistraction();
                    battle.StartCoroutine(PerformAction("Distract"));
                    break;
                case ("Focus"):
                    battle.playerPokemon.Focus();
                    battle.StartCoroutine(PerformAction("Focus"));
                    break;
                case ("Heal"):
                    Debug.Log("Heal : TODO");
                    break;
                case ("Capture"):
                    Debug.Log("Capture : TODO");
                    break;
                case ("Run"):
                    Debug.Log("Run");
                    battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.endState);
                    break;
            }
        }

        /**
         * Coroutine for the run case
         */
        private IEnumerator Run()
        {
            battle.StartCoroutine(battle.dialogBox.TypeDialog($"You ran."));

            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0))
                {
                    battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.endState);
                    yield break;
                }
            }
        }
        
        /**
         * Coroutine for the action of the player pokemon
         */
        private IEnumerator PerformAction(string action)
        {
            battle.StartCoroutine(battle.dialogBox.TypeDialog($"{battle.playerPokemon.name} used {action}."));
        
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0)) continue;
                
                if (battle.wildPokemon.ko)
                {
                    battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.endState);
                }
                else
                {
                    battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.enemyMoveState);
                }
                yield break;
            }
        }


        #region IState

        public void Enter()
        {   
            Debug.Log("Player Turn");
            foreach (BattleAction action in battle.dialogBox.actions.gameObject.GetComponentsInChildren<BattleAction>())
            {
                action.OnItemClicked += OnRequestedAction;
            }

            battle.dialogBox.ToggleDialogText(false);
            battle.dialogBox.ToggleAction(true);
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }

        #endregion
    }
}