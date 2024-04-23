using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI; // UIInventoryPage

namespace DesignPattern.State.Game
{
    public class PlayState : IState
    {
        private Game game;
    
        
        public PlayState(Game game)
        {
            this.game = game;
        }

        
        #region IState

        public void Enter()
        {

        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                game.GamestateMachine.TransitionTo(game.GamestateMachine.pauseState);
            }


            var colliders = Physics.OverlapSphere(game.player.transform.position, 5f, 6);
            if (colliders != null)
            {
                //game.GamestateMachine.battleState.playerPokemon = game.player.GetMainPokemon();
                //game.GamestateMachine.battleState.wildPokemon = colliders[0].GetComponent<PokemonSO>();
                game.GamestateMachine.TransitionTo(game.GamestateMachine.battleState);
            }
        }

        public void Exit()
        {
            
        }

        #endregion
    }
}