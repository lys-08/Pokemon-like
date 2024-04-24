using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using Inventory.UI; // UIInventoryPage


namespace DesignPattern.State
{
   public class Game : MonoBehaviour
   {
       private UnityEvent<bool> onPause = new UnityEvent<bool>();
       [field: SerializeField] public UIInventoryPage inventory;
       [field: SerializeField] public BattleSystem battle;
       [field: SerializeField] public Player player;
       private PlayerController playerController;
       private Camera mainCamera;
       
       // SINGLETON
       private static Game instance_;
       
       // STATE
       private StateMachine stateMachine_;
       public StateMachine GamestateMachine => stateMachine_;

       
       // TODO temporary
       [field: SerializeField] public PokemonSO poke1;
       [field: SerializeField] public WildPokemonSO poke2;

       

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
        * Launch the battle state
        * -> Disable the main camera to use the battle camera
        */
       private void StartBattle()
       {
           GamestateMachine.TransitionTo(GamestateMachine.battleState);
           mainCamera.gameObject.SetActive(false);
       }
       
       /**
        * Stop the battle state
        * -> Activate the main camera
        */
       private void EndBattle(bool b)
       {
           GamestateMachine.TransitionTo(GamestateMachine.playState);
           mainCamera.gameObject.SetActive(true);
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
           inventory.gameObject.SetActive(false);
       }

       private void Start()
       {
           //STATE
           stateMachine_.Initialize(stateMachine_.playState);

           player.OnEncountered += StartBattle;
           stateMachine_.battleState.OnBattleOver += EndBattle;
       }
  
       private void Update()
       {
           stateMachine_.Update();

           if (stateMachine_.CurrentState == stateMachine_.playState)
           {
               playerController.HandleUpdate();
           }
       }

       #endregion
   }
}
