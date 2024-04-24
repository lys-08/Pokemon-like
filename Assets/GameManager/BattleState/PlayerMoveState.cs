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
            battle.dialogBox.ToggleDialogText(true);
            battle.dialogBox.ToggleAction(false);

            switch (obj.name)
            {
                case ("Fight"):
                    float newHp = battle.wildPokemon.TakeDamage(battle.playerPokemon.GetDamage(), battle.playerPokemon.type);
                    battle.StartCoroutine(battle.battleHUD.UpdateWildPokemonBar(newHp));
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
            yield return new WaitForSeconds(1f);

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
            Debug.Log("Perform Action");
            battle.StartCoroutine(battle.dialogBox.TypeDialog($"{battle.playerPokemon.name} used {action}."));
            yield return new WaitForSeconds(1f);
        
            while (true)
            {
                // TODO : transition à revoir (y'a un porblème)
                if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Mouse0)) continue;
                
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
            foreach (BattleAction action in battle.dialogBox.actions.gameObject.GetComponentsInChildren<BattleAction>())
            {
                action.OnItemClicked -= OnRequestedAction;
            }
        }

        #endregion
    }
}