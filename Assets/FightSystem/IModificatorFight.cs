using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModificatorFight 
{
    void AddModificator(ModificatorFight modificator);

    void DeleteModificator(ModificatorFight modificator);

    List<ModificatorFight> Modificators { get; }
}
