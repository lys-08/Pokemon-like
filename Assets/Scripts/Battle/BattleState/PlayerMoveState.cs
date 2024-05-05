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
    
        
        public PlayerMoveState(BattleSystem battle_)
        {
            this.battle = battle_;
            // battle.inventoryController.OnPerformActionInBattle += PerformActionInBattle;
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
                    battle.inventoryController.ActionBattle(true);
                    break;
                case ("Run"):
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
                }
                yield return null;
            }
        }
        
        /**
         * Coroutine for the action of the player pokemon
         */
        private IEnumerator PerformAction(string action)
        {
            yield return battle.dialogBox.TypeDialog($"{battle.playerPokemon.name} used {action}.");

            bool pressed = false;
            while (!pressed)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    pressed = true;
                    if (battle.wildPokemon.ko)
                    {
                        /*
                         * We get an item
                         */
                        battle.inventoryController.GetInventoryItemData().AddItem(battle.wildPokemon.GetObj());
                        battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.endState);
                    }
                    else
                    {
                        battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.enemyMoveState);
                    }
                }
                yield return null;
            }
        }

        /**
         * Called a coroutine that determine which item had been used
         */
        private void PerformActionInBattle(int index)
        {
            battle.inventoryController.ActionBattle(false);
            battle.StartCoroutine(PerformActionInBattleCoroutine(index));
        }

        private IEnumerator PerformActionInBattleCoroutine(int index)
        {
            InventoryItem item = battle.inventoryController.GetInventoryItemData().GetItemAt(index);
            
            
            if (item.IsEmpty) yield return battle.dialogBox.TypeDialog($"You didn't choose an item.");
            bool pressed = false;
            
            EdibleItemSO edibleItemAction = item.item as EdibleItemSO;
            if (edibleItemAction != null)
            {
                Debug.Log($"avant hp {battle.playerPokemon.hp}");
                edibleItemAction.Perform(battle.playerPokemon);
                Debug.Log($"new hp {battle.playerPokemon.hp}");
                
                // We destroy one item once used
                IDestroyableItem destroyableItem = item.item as IDestroyableItem;
                if (destroyableItem != null)
                {
                    if (item.quantity == 1) battle.inventoryController.GetInventoryItem().ResetSelection();
                    battle.inventoryController.GetInventoryItemData().RemoveItem(index, 1);
                }
                
                yield return battle.battleHUD.UpdatePlayerPokemonBar(battle.playerPokemon.hp);
                yield return battle.dialogBox.TypeDialog($"You used a {item.item.Name} on {battle.playerPokemon}.");
                
                while (!pressed)
                {
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        pressed = true;
                        battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.enemyMoveState);
                    }
                    yield return null;
                }
            }
            
            CaptureItemSO captureItemAction = item.item as CaptureItemSO;
            if (captureItemAction != null)
            {
                var isCaptured = captureItemAction.Perform(battle.wildPokemon);
                
                // We destroy one item once used
                IDestroyableItem destroyableItem = item.item as IDestroyableItem;
                if (destroyableItem != null)
                {
                    if (item.quantity == 1) battle.inventoryController.GetInventoryItem().ResetSelection();
                    battle.inventoryController.GetInventoryItemData().RemoveItem(index, 1);
                }
                
                yield return battle.dialogBox.TypeDialog($"You launch a {item.item.Name} at {battle.wildPokemon}.");
                
                while (!pressed)
                {
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        pressed = true;
                        if (isCaptured)
                        {
                            battle.wildPokemon.hp = battle.wildPokemon.hpMax; // Th epokemon gets all it's pv back
                            battle.inventoryController.AddPokemonToCollection(battle.wildPokemon);
                            yield return battle.dialogBox.TypeDialog($"You catched {battle.wildPokemon.name}.");
                            yield return battle.dialogBox.TypeDialog($"{battle.wildPokemon.name} has been added to your collection.");

                            bool pressed2 = false;
                            while (!pressed2)
                            {
                                if (Input.GetKeyDown(KeyCode.Mouse0))
                                {
                                    pressed2 = true;
                                    battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.endState);
                                }

                                yield return null;
                            }
                        }
                        else
                        {
                            yield return battle.dialogBox.TypeDialog($"{battle.wildPokemon.name} broke free.");
                            
                            bool pressed2 = false;
                            while (!pressed2)
                            {
                                if (Input.GetKeyDown(KeyCode.Mouse0))
                                {
                                    pressed2 = true;
                                    battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.enemyMoveState);
                                }

                                yield return null;
                            }
                        }
                    }
                    yield return null;
                }
            }

            battle.BattleStateMachine.TransitionTo(battle.BattleStateMachine.enemyMoveState);
        }
        
        #region IState

        public void Enter()
        {   
            foreach (BattleAction action in battle.dialogBox.actions.gameObject.GetComponentsInChildren<BattleAction>())
            {
                action.OnItemClicked += OnRequestedAction;
            }

            battle.dialogBox.ToggleDialogText(false);
            battle.dialogBox.ToggleAction(true);
            battle.inventoryController.OnPerformActionInBattle += PerformActionInBattle;
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            battle.inventoryController.OnPerformActionInBattle -= PerformActionInBattle;
            foreach (BattleAction action in battle.dialogBox.actions.gameObject.GetComponentsInChildren<BattleAction>())
            {
                action.OnItemClicked -= OnRequestedAction;
            }
        }

        #endregion
    }
}