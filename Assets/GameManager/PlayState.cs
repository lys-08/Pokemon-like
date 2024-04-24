using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI; // UIInventoryPage

namespace DesignPattern.State
{
    public class PlayState : IState
    {
        private Game game;
    
        
        public PlayState(Game game)
        {
            this.game = game;
        }

        
        #region IState

        public void Enter()
        {

        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                game.GamestateMachine.TransitionTo(game.GamestateMachine.pauseState);
            }
            
            if (Input.GetKeyDown(KeyCode.H))
            {
                game.battle.playerPokemon = game.poke1;
                game.battle.wildPokemon = game.poke2;
                game.GamestateMachine.TransitionTo(game.GamestateMachine.battleState);
            }
        }

        public void Exit()
        {
            
        }

        #endregion
    }
}