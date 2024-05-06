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
            game.menuPause.OpenMenu();
        }

        public void Update()
        {
            if (!game.menuPause.GetIsVisible())
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