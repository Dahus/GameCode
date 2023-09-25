using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.GlobalSystems;
using System;
namespace Game.UnitsSystem
{
    [Serializable]
    public class UnitInProduction : IStepPlayer
    {
        public string Name;
        public TypeObjectProduction typeObjProduction;
        public int Step = -1;
        public int MaxStep=-1;
        public Sprite spriteIcon;


        public bool CanEndStep { get => canEndStep; set => canEndStep = value; }
        private bool canEndStep;
        public void BeginPlayerTurn()
        {
            if (Step > 0)
            {
                Step--;
            }
        }

        public void CompletePlayerTurn()
        {
        }

        public IEnumerator CoroutineEndStep()
        {
            yield return null;
        }

    }

    public enum TypeObjectProduction
    {
        Unit,
        Upgrade
    }
}
