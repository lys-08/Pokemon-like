using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace DesignPattern.State
{
    public class BattleState : MonoBehaviour, IState
    {
        private Game game;
        public event Action<bool> OnBattleOver; 
    
        public BattleState(Game game)
        {
            this.game = game;
        }
        


        private void Awake()
        {
            
        }


        #region IState

        public void Enter()
        {
            game.battle.SetUpBattle();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        public void Update()
        {
            if (game.battle.combatEnded)
            {
                game.battle.combatEnded = false;
                Exit();
            }
        }

        public void Exit()
        {
            Cursor.visible = false;
            OnBattleOver?.Invoke(true);
        }

        #endregion
    }
}