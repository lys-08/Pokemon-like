using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DesignPatterns.Singleton;
using DesignPatterns.State;
using UnityEngine.Events;

namespace DesignPatterns.State
{
    public class StateMachine : MonoBehaviour
    {
        public IState CurrentState { get; private set; }
       
        // reference to state objects
        public PlayState playState;
        public PauseState pauseState;
        public BattleState battleState;
        public LoseState loseState;
       
        // event to notify other objects of the state change
        private UnityEvent<IState> stateChanged;
       
       
        
        /**
         * Constructor
         */
        public StateMachine(Game game)
        {
            // create an instance for each state
            this.playState = new PlayState(game);
            this.loseState = new LoseState(game);
            this.battleState = new BattleState(game);
            this.pauseState = new PauseState(game);
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