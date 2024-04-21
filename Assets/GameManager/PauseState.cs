using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns.Singleton;
using Inventory.UI; // UIInventoryPage

namespace DesignPatterns.State
{
    public class PauseState : IState
    {
        private Game game;
        [SerializeField] private UIInventoryPage inventory;
    
        
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
            inventory.Show();
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
            inventory.Hide();
        }

        #endregion
    }
}