using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;


namespace DesignPattern.State
{
    public class InventoryState : IState
    {
        private Game game;
    
        
        public InventoryState(Game game)
        {
            this.game = game;
        }
        

        #region IState

        public void Enter()
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            game.inventory.Show();
            
            // We set the UI
            game.inventory.SetUpInventoryUI();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                game.GamestateMachine.TransitionTo(game.GamestateMachine.playState);
            }
        }

        public void Exit()
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            game.inventory.Hide();
        }

        #endregion
    }
}