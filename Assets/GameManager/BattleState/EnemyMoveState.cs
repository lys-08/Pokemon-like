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
            battle.StartCoroutine(battle.dialogBox.TypeDialog($"The wild {battle.wildPokemon.name} ran."));

            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0)) continue;
                    
                battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.endState);
                yield break;
            }
        }
        
        /**
         * Coroutine for the action of the pokemon
         */
        private IEnumerator PerformAction(string action)
        {
            battle.StartCoroutine(battle.dialogBox.TypeDialog($"{battle.wildPokemon.name} used {action}."));
        
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0)) continue;
                
                if (battle.playerPokemon.ko)
                {
                    battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.endState);
                }
                else
                {
                    battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.playerMoveState);
                }
                yield break;
            }
        }
        

        #region IState

        public void Enter()
        {
            Debug.Log("Enemy Turn");
            
            // TODO : Coefs
            //Debug.Log($"run {battle.wildPokemon.runCoeff_} fight {battle.wildPokemon.attackCoeff_} " +
            //          $"disctrac {battle.wildPokemon.distractCoeff_} focus {battle.wildPokemon.focusCoeff_}"); ;

            switch (Random.Range(0, 1)) // TODO : temporary
            {
                case (0):
                    battle.playerPokemon.TakeDamage(battle.wildPokemon.GetDamage(), battle.wildPokemon.type);
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