using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.GridSystem;

public interface IAction 
{
    void Execute(GridHex hex);

}
