using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GlobalSystems
{
    public interface IStepPlayer
    {
        void BeginPlayerTurn();
        void CompletePlayerTurn();
        IEnumerator CoroutineEndStep();
        bool CanEndStep { get; set; }
    }
}
