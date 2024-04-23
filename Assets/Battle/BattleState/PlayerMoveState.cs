using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace DesignPattern.State.Battle
{
    public class PlayerMoveState : IState
    {
        private Battle battle;
    
        
        public PlayerMoveState(Battle battle)
        {
            this.battle = battle;
        }


        #region IState

        public void Enter()
        {   
            Debug.Log("Player Turn");
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