using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns.Singleton;
using Inventory.UI; // UIInventoryPage

namespace DesignPatterns.State
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
        }

        public void Exit()
        {
            
        }

        #endregion
    }
}