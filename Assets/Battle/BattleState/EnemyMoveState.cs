using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI; // UIInventoryPage

namespace DesignPattern.State.Battle
{
    public class EnemyMoveState : IState
    {
        private Battle battle;
        [SerializeField] private UIInventoryPage inventory;
    
        
        public EnemyMoveState(Battle battle)
        {
            this.battle = battle;
        }
        

        #region IState

        public void Enter()
        {
            Debug.Log("Enemy Turn");
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