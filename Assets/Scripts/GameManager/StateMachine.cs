using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace DesignPattern.State
{
    public class StateMachine : MonoBehaviour
    {
        public IState CurrentState { get; private set; }
       
        // reference to state objects
        [field: SerializeField] public PlayState playState;
        [field: SerializeField] public PauseState pauseState;
        [field: SerializeField] public InventoryState inventoryState;
        [field: SerializeField] public BattleState battleState;
        [field: SerializeField] public LoseState loseState;
       
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
            this.inventoryState = new InventoryState(game);
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
