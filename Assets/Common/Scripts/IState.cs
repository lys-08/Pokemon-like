using System.Collections.Generic;
using UnityEngine;


namespace DesignPattern.State
{
    public interface IState
    {
        public void Enter();

        public void Update();

        public void Exit();
    }
}