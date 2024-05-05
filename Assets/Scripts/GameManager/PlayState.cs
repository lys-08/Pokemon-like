using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI; // UIInventoryPage

namespace DesignPattern.State
{
    public class PlayState : IState
    {
        private Game game;
    
        private LayerMask pokemon = LayerMask.GetMask("Pokemon");
        
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
            game.playerController.HandleUpdate();
            // game.player.HandleUpdate(); à décommenter si on utilise le script Player.cs
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                game.GamestateMachine.TransitionTo(game.GamestateMachine.pauseState);
            }

            var colliders = Physics.OverlapSphere(game.player.transform.position, 4f, pokemon);
            if (colliders.Length != 0)
            {
                PokemonSO playerPoke = game.inventory.GetMainPokemon();
                if (playerPoke.ko) game.inventory.UpdateMainPokemon();
                game.battle.playerPokemon = game.inventory.GetMainPokemon();

                game.battle.wildPokemon = game.poke2;
                game.GamestateMachine.TransitionTo(game.GamestateMachine.battleState);
            }
        }

        public void Exit()
        {
            
        }

        #endregion
    }
}