using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DesignPatterns.Singleton;

namespace DesignPatterns.State
{
    public class BattleState : IState
    {
        private Game game;
    
        public BattleState(Game game)
        {
            this.game = game;
        }


        #region IState

        public void Enter()
        {
            Debug.Log("Battle");
        }

        public void Update()
        {
            //game.GameStateMachine.TransitionTo(game.GameStateMachine.playState);
        }

        public void Exit()
        {
        
        }

        #endregion
    }
}