using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DesignPattern.State.Battle
{
    public class BusyState : IState
    {
        private Battle battle;
    
        public BusyState(Battle battle)
        {
            this.battle = battle;
        }


        #region IState

        public void Enter()
        {
            Debug.Log("Busy");
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