using System.Collections;
using System.Collections.Generic;
using Inventory;
using Inventory.Model;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace DesignPattern.State
{
    public class PlayerMoveState : IState
    {
        private BattleSystem battle;
    
        
        public PlayerMoveState(BattleSystem battle)
        {
            this.battle = battle;
        }
        
        /**
         * Action : activate when the player choose an action to do
         */
        private void OnRequestedAction(BattleAction obj)
        {
            battle.dialogBox.ToggleDialogText(true);
            battle.dialogBox.ToggleAction(false);

            switch (obj.name)
            {
                case ("Fight"):
                    float newHp = battle.wildPokemon.TakeDamage(battle.playerPokemon.GetDamage(), battle.playerPokemon.type);
                    battle.StartCoroutine(battle.battleHUD.UpdateWildPokemonBar(newHp));
                    battle.StartCoroutine(PerformAction("Fight"));
                    break;
                case ("Distract"):
                    battle.wildPokemon.TakeDistraction();
                    battle.StartCoroutine(PerformAction("Distract"));
                    break;
                case ("Focus"):
                    battle.playerPokemon.Focus();
                    battle.StartCoroutine(PerformAction("Focus"));
                    break;
                case ("Bag"):
                    Debug.Log("Capture : TODO");
                    break;
                case ("Run"):
                    Debug.Log("Run");
                    battle.StartCoroutine(Run());
                    break;
            }
        }

        /**
         * Coroutine for the run case
         */
        private IEnumerator Run()
        {
            yield return battle.dialogBox.TypeDialog("You ran.");

            bool pressed = false;

            while(!pressed)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    pressed = true;
                    battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.endState);
                    break;
                }
                yield return null;
            }
        }
        
        /**
         * Coroutine for the use of a pokeball
         */
        private IEnumerator ThrowPokeball(CaptureItemSO pokeball)
        {
            bool isCatched = false;
            /*
             * If the pokeball hasn't have a value, we define the rate of catching the pokemon
             * -> the rate change according to the type of the ball and the pokemon
             */
            if (pokeball.value == 0)
            {
                float rate = 1 - battle.wildPokemon.hp / battle.wildPokemon.hpMax;
            
                if (pokeball.type == battle.wildPokemon.type) // The ball as the same type as the pokemon
                {
                    isCatched = CatchPokemon(rate * 1.5f);
                }
                else if (pokeball.type == Type.Simple) // The ball doesn't have any type
                {
                    isCatched = CatchPokemon(rate);
                }
                else
                {
                    isCatched = CatchPokemon(rate * 0.5f);
                }
            }
            /*
             * If the pokeball has a value, we try to catch the pokemon with the ball rate
             */
            else
            {
                isCatched = CatchPokemon(pokeball.value);
            }
            
            
            yield return battle.dialogBox.TypeDialog($"You used a {pokeball.Name}.");

            while (isCatched)
            {
                battle.pokemonInventory.AddItem(battle.wildPokemon);
                Debug.Log("The pokemon is catched");
                yield return battle.dialogBox.TypeDialog($"You catched {battle.wildPokemon.name}.");
                yield return battle.dialogBox.TypeDialog($"{battle.wildPokemon.name} has been added to your collection.");
                
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0))
                {
                    battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.endState);
                    yield break;
                }
            }
            
            while (!isCatched)
            {
                Debug.Log("The pokemon get out of the ball");
                yield return battle.dialogBox.TypeDialog($"{battle.wildPokemon.name} broke free.");
                
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0))
                {
                    battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.enemyMoveState);
                    yield break;
                }
            }
        }

        /**
         * Return true if the pokemon is catched
         */
        private bool CatchPokemon(float value)
        {
            if (value >= 1f) return true;
            
            // TODO
            return false;
        }
        
        /**
         * Coroutine for the action of the player pokemon
         */
        private IEnumerator PerformAction(string action)
        {
            Debug.Log("Perform Action");
            yield return battle.dialogBox.TypeDialog($"{battle.playerPokemon.name} used {action}.");
        
            while (true)
            {
                // TODO : transition à revoir (y'a un porblème)
                if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Mouse0)) continue;
                
                if (battle.wildPokemon.ko)
                {
                    battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.endState);
                }
                else
                {
                    battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.enemyMoveState);
                }
                yield break;
            }
        }
        
        #region IState

        public void Enter()
        {   
            Debug.Log("Player Turn");
            
            foreach (BattleAction action in battle.dialogBox.actions.gameObject.GetComponentsInChildren<BattleAction>())
            {
                action.OnItemClicked += OnRequestedAction;
            }

            battle.dialogBox.ToggleDialogText(false);
            battle.dialogBox.ToggleAction(true);
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            foreach (BattleAction action in battle.dialogBox.actions.gameObject.GetComponentsInChildren<BattleAction>())
            {
                action.OnItemClicked -= OnRequestedAction;
            }
        }

        #endregion
    }
}