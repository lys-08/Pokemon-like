using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace DesignPattern.State
{
    public class BattleStateMachine : MonoBehaviour
    {
        public IState CurrentState { get; private set; }
       
        // reference to state objects
        public StartState startState;
        public EndState endState;
        public EnemyMoveState enemyMoveState;
        public PlayerMoveState playerMoveState;
       
        // event to notify other objects of the state change
        private UnityEvent<IState> stateChanged;
       
       
        
        /**
         * Constructor
         */
        public BattleStateMachine(BattleSystem battle)
        {
            // create an instance for each state
            startState = new StartState(battle);
            endState = new EndState(battle);
            enemyMoveState = new EnemyMoveState(battle);
            playerMoveState = new PlayerMoveState(battle);
        }
        
        public void AddStateChangedListener(UnityAction<IState> listener)
        {
            stateChanged.AddListener(listener);
        }
    
        public void RemoveStateChangedListener(UnityAction<IState> listener)
        {
            stateChanged.RemoveListener(listener);
        }
        
        /**
         * Set the starting state
         */
        public void Initialize(IState state)
        {
            CurrentState = state;
            state.Enter();

            // notify other objects that state has changed
            stateChanged?.Invoke(state);
        }
       
        /**
         * Exit the current state and enter an other
         */
        public void TransitionTo(IState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            nextState.Enter();

            // notify other objects that state has changed
            stateChanged?.Invoke(nextState);
        }
        
        
        public void Update()
        {
            if (CurrentState != null)
            {
                CurrentState.Update();
            }
        }
    }
}
