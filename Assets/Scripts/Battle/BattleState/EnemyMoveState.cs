using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI; // UIInventoryPage

namespace DesignPattern.State
{
    public class EnemyMoveState : IState
    {
        private BattleSystem battle;
    
        
        public EnemyMoveState(BattleSystem battle)
        {
            this.battle = battle;
        }
        
        
        /**
         * Coroutine for the run case
         */
        private IEnumerator Run()
        {
            yield return battle.dialogBox.TypeDialog($"The wild {battle.wildPokemon.name} ran.");

            bool pressed = false;
            while (!pressed)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    pressed = true;
                    battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.endState);
                }
                yield return null;
            }
        }
        
        /**
         * Coroutine for the action of the pokemon
         */
        private IEnumerator PerformAction(string action)
        {
            yield return battle.dialogBox.TypeDialog($"{battle.wildPokemon.name} used {action}.");
            yield return new WaitForSeconds(1f);


            bool pressed = false;

            while(!pressed)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    pressed = true;
                    if (battle.playerPokemon.ko)
                    {
                        battle.inventoryController.UpdateMainPokemon(); // Update the main pokemon
                        battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.endState);
                    }
                    else
                    {
                        battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.playerMoveState);
                    }
                    break;
                }
                yield return null;
            }
        }
        

        #region IState

        public void Enter()
        {
            // TODO : Coefs
            //Debug.Log($"run {battle.wildPokemon.runCoeff_} fight {battle.wildPokemon.attackCoeff_} " +
            //          $"disctrac {battle.wildPokemon.distractCoeff_} focus {battle.wildPokemon.focusCoeff_}"); ;

            switch (0) // TODO : temporary
            {
                case (0):
                    float newHp = battle.playerPokemon.TakeDamage(battle.wildPokemon.GetDamage(), battle.wildPokemon.type);
                    battle.StartCoroutine(battle.battleHUD.UpdatePlayerPokemonBar(newHp));
                    battle.StartCoroutine(PerformAction("Fight"));
                    break;
                case (1):
                    battle.playerPokemon.TakeDistraction();
                    battle.StartCoroutine(PerformAction("Distract"));
                    break;
                case (2):
                    battle.wildPokemon.Focus();
                    battle.StartCoroutine(PerformAction("Fight"));
                    break;
                case (3):
                    battle.StartCoroutine(Run());
                    // TODO run message
                    break;
            }

            
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