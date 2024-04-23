using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI; 

namespace DesignPattern.State.Battle
{
    public class StartState : IState
    {
        private Battle battle;
    
        
        public StartState(Battle battle)
        {
            this.battle = battle;
        }

        
        #region IState

        public void Enter()
        {
            
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