using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;


namespace DesignPattern.State.Battle
{
   public class Battle : MonoBehaviour
   {
       private UnityEvent<bool> onPause = new UnityEvent<bool>();
       
       // SINGLETON
       private static Battle instance_;
       
       // STATE
       private BattleStateMachine battleStateMachine_;
       public BattleStateMachine GamestateMachine => battleStateMachine_;


       
       #region Singleton

       // global access
       public static Battle Instance
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
           battleStateMachine_ = new BattleStateMachine(this);
       }
       
       private static void SetupInstance()
       {
           // lazy instantiation
           instance_ = FindObjectOfType<Battle>();


           if (instance_ == null)
           {
               GameObject gameObj = new GameObject();
               gameObj.name = "Singleton Battle System";
               instance_ = gameObj.AddComponent<Battle>();
               DontDestroyOnLoad(gameObj);
           }
       }

       #endregion
       
  
       
       private void Start()
       {
           battleStateMachine_.Initialize(battleStateMachine_.startState);
       }
  
       private void Update()
       {
           battleStateMachine_.Update();
       }

       public void AddOnPauseListener(UnityAction<bool> listener)
       {
           onPause.AddListener(listener);
       }
  
       private void EndFight()
       {
           Debug.Log("The fight is over");
       }
   }
}
