using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.FightSystem
{
    public interface IColorize
    {
         void HighLite(Color color);

         Color StartColor{ get;}
    }
}
