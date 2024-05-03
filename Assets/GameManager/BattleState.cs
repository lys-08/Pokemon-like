using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace DesignPattern.State
{
    public class BattleState : MonoBehaviour, IState
    {
        private Game game;
    
        public BattleState(Game game)
        {
            this.game = game;
        }
        


        #region IState

        public void Enter()
        {
            game.mainCamera.gameObject.SetActive(false);
            game.battle.SetUpBattle();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        public void Update()
        {
            if (game.battle.combatEnded)
            {
                game.battle.combatEnded = false;
                game.GamestateMachine.TransitionTo(game.GamestateMachine.playState);
            }
        }

        public void Exit()
        {
            game.battle.OutBattle();
            Cursor.visible = false;
            game.mainCamera.gameObject.SetActive(true);
        }

        #endregion
    }
}