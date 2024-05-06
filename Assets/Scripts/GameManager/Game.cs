using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using Inventory;
using Inventory.UI; // UIInventoryPage


namespace DesignPattern.State
{
   public class Game : MonoBehaviour
   {
       [field: SerializeField] public MenuPause menuPause;
       
       [field: SerializeField] public InventoryController inventory;
       [field: SerializeField] public BattleSystem battle;
       [field: SerializeField] public Player player;
       [field: SerializeField] public PlayerController playerController;
       [field: SerializeField] public Camera mainCamera;
       
       // SINGLETON
       private static Game instance_;
       
       // STATE
       private StateMachine stateMachine_;
       public StateMachine GamestateMachine => stateMachine_;

       
       // TODO temporary : voir PlayState
       [field: SerializeField] public WildPokemonSO poke2;

       

       // global access
       public static Game Instance
       {
           get
           {
               if (instance_ == null)
               {
                   SetupInstance();
               }
               return instance_;
           }
       }
       
       private static void SetupInstance()
       {
           // lazy instantiation
           instance_ = FindObjectOfType<Game>();


           if (instance_ == null)
           {
               GameObject gameObj = new GameObject();
               gameObj.name = "Singleton Game";
               instance_ = gameObj.AddComponent<Game>();
               DontDestroyOnLoad(gameObj);
           }
       }

       /**
        *
        */
       private void StartBattle(WildPokemonSO wildPokemon)
       {
           battle.wildPokemon = wildPokemon;
           GamestateMachine.TransitionTo(GamestateMachine.battleState);
       }



       #region Unity Events Methods

       private void Awake()
       {
           // if this is the first instance, make this the persistent singleton
           if (instance_ == null)
           {
               instance_ = this;
               DontDestroyOnLoad(this.gameObject);
           }
           // otherwise, remove any duplicates
           else
           {
               Destroy(gameObject);
           }
           
           // STATE
           stateMachine_ = new StateMachine(this);

           player = FindObjectOfType<Player>();
           playerController = player.gameObject.GetComponent<PlayerController>();
           mainCamera = Camera.main;
           inventory.Hide();

           player.OnEncountered += StartBattle;
       }
       
       private void Start()
       {
           //STATE
           stateMachine_.Initialize(stateMachine_.playState);
       }
  
       private void Update()
       {
           stateMachine_.Update();
       }

       #endregion
   }
}
