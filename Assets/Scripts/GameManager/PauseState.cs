using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;


namespace DesignPattern.State
{
    public class PauseState : IState
    {
        private Game game;
    
        
        public PauseState(Game game)
        {
            this.game = game;
        }
        

        #region IState

        public void Enter()
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            
            // We set the UI
            game.menuPause.SetActive(true);
        }

        public void Update()
        {
            if (!game.menuPause.activeInHierarchy)
            {
                game.GamestateMachine.TransitionTo(game.GamestateMachine.playState);
            }
        }

        public void Exit()
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
        }

        #endregion
    }
}