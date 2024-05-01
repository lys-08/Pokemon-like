using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Inventory;
using Inventory.UI;
using Unity.VisualScripting;

namespace DesignPattern.State
{
    public class EndState : IState
    {
        private BattleSystem battle;
    
        
        public EndState(BattleSystem battle)
        {
            this.battle = battle;
        }

        
        #region IState

        public void Enter()
        {
            battle.playerPokemon.ResetCoeffs(); // We reset the coefficients associated with the defense and attack of the player pokemon
            battle.wildPokemon.ResetCoeffs();
            
            battle.StartCoroutine(battle.dialogBox.TypeDialog($"The fight is over."));
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0)) Exit();
        }

        public void Exit()
        {
            battle.combatEnded = true;
        }

        #endregion
    }
}