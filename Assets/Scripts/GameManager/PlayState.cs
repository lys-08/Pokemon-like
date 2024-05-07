using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI; // UIInventoryPage

namespace DesignPattern.State
{
    public class PlayState : IState
    {
        private Game game;

        LayerMask pokemon = LayerMask.GetMask("Pokemon");
    
        
        public PlayState(Game game)
        {
            this.game = game;
        }

        
        #region IState

        public void Enter()
        {

        }

        // public void Update()
        // {
        //     game.playerController.HandleUpdate();
        //     game.player.HandleUpdate();
            
        //     if (Input.GetKeyDown(KeyCode.E))
        //     {
        //         game.GamestateMachine.TransitionTo(game.GamestateMachine.pauseState);
        //     }
            
        //     if (Input.GetKeyDown(KeyCode.H))
        //     {
        //         /*
        //          * DONE : à mettre dans le script du player lorsqu'il rencontre un pokémon et qu'il faut lancer le combat
        //          */
        //         PokemonSO playerPoke = game.inventory.GetMainPokemon();
        //         if (playerPoke.ko) game.inventory.UpdateMainPokemon();
        //         game.battle.playerPokemon = game.inventory.GetMainPokemon();

        //         game.battle.wildPokemon = game.poke2;
        //         game.GamestateMachine.TransitionTo(game.GamestateMachine.battleState);
        //     }


        //     var colliders = Physics.OverlapSphere(game.player.transform.position, 5f, 6);
        //     if (colliders != null)
        //     {
        //         //game.GamestateMachine.battleState.playerPokemon = game.player.GetMainPokemon();
        //         //game.GamestateMachine.battleState.wildPokemon = colliders[0].GetComponent<PokemonSO>();
        //         game.GamestateMachine.TransitionTo(game.GamestateMachine.battleState);
        //     }
        // }

        public void Update()
        {
            game.playerController.HandleUpdate();
            game.mouseLook.HandleUpdate();
            // game.player.HandleUpdate(); à décommenter si on utilise le script Player.cs
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                game.GamestateMachine.TransitionTo(game.GamestateMachine.inventoryState);
            }
            
            if (Input.GetKeyDown(KeyCode.Q)) // A
            {
                game.GamestateMachine.TransitionTo(game.GamestateMachine.pauseState);
            }

            var colliders = Physics.OverlapSphere(game.player.transform.position, 4f, pokemon);
            if (colliders.Length != 0)
            {
                PokemonSO playerPoke = game.inventory.GetMainPokemon();
                if (playerPoke.ko) game.inventory.UpdateMainPokemon();
                game.battle.playerPokemon = game.inventory.GetMainPokemon();

                Debug.Log(colliders[0].gameObject.name);

                game.battle.wildPokemon = colliders[0].gameObject.GetComponent<WildPokemon>().getData();
                GameObject.Destroy(colliders[0].gameObject);  
                game.GamestateMachine.TransitionTo(game.GamestateMachine.battleState);

                //game.GamestateMachine.battleState.playerPokemon = game.player.GetMainPokemon();
        //         //game.GamestateMachine.battleState.wildPokemon = colliders[0].GetComponent<PokemonSO>();
        //         game.GamestateMachine.TransitionTo(game.GamestateMachine.battleState);
            }
        }

        public void Exit()
        {
            
        }

        #endregion
    }
}