using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns.Singleton;
using UnityEngine.SceneManagement;


namespace DesignPatterns.State
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