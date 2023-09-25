using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ModificatorHex : MonoBehaviour, IModificatorFight
{

    [SerializeField] private List<ModificatorFight> _modificatorFights= new List<ModificatorFight>();
    public List<ModificatorFight> Modificators => _modificatorFights;


    public void AddModificator(ModificatorFight modificator)
    {
        _modificatorFights.Add(modificator);
    }

    public void DeleteModificator(ModificatorFight modificator)
    {
        _modificatorFights.Remove(modificator);
    }
}
