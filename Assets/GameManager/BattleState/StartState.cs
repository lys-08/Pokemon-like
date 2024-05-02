using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Inventory.UI;
using Unity.VisualScripting;

namespace DesignPattern.State
{
    public class StartState : IState
    {
        private BattleSystem battle;
    
        
        public StartState(BattleSystem battle)
        {
            this.battle = battle;
        }


        private IEnumerator StartStateSetUp()
        {
            yield return battle.dialogBox.TypeDialog($"A wild {battle.wildPokemon.name} appeared.");

            bool pressed = false;
            while (!pressed)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0))
                {
                    pressed = true;
                    /*
                     * If the speed of the player pokemon is greater than the wild one, then it's the player
                     * who start. Otherwise it's the wild pokemon
                     */
                    if (battle.playerPokemon.speed > battle.wildPokemon.speed)
                    {
                        battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.playerMoveState);
                    }
                    else
                    {
                        battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.enemyMoveState);
                    }
                }

                yield return null;
            }
        }

        
        #region IState

        public void Enter()
        {
            battle.StartCoroutine(StartStateSetUp());
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