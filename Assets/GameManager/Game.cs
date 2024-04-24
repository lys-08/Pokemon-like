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
       
       // SINGLETON
       private static Game instance_;
       
       // STATE
       private StateMachine stateMachine_;
       public StateMachine GamestateMachine => stateMachine_;

       
       // TODO temporary
       [field: SerializeField] public PokemonSO poke1;
       [field: SerializeField] public WildPokemonSO poke2;

       
       #region Singleton

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
           inventory.gameObject.SetActive(false);
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

       #endregion
       
  
       
       private void Start()
       {
           //STATE
           stateMachine_.Initialize(stateMachine_.playState);
       }
  
       private void Update()
       {
           stateMachine_.Update();
       }

       public void AddOnPauseListener(UnityAction<bool> listener)
       {
           onPause.AddListener(listener);
       }
  
       private void EndGame()
       {
           Debug.Log("Game Over");
       }
   }
}
