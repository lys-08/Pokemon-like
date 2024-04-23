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

        
        #region IState

        public void Enter()
        {
            battle.StartCoroutine(battle.dialogBox.TypeDialog($"A wild <i>{battle.wildPokemon.name}</i> appeared."));
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.playerMoveState);
            }
        }

        public void Exit()
        {
            
        }

        #endregion
    }
}