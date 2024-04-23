using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace DesignPattern.State
{
    public class LoseState : IState
    {
        private Game game;
    
        
        public LoseState(Game game)
        {
            this.game = game;
        }


        #region IState

        public void Enter()
        {   
            Debug.Log("Game Over");
            SceneManager.LoadScene("Menu");
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