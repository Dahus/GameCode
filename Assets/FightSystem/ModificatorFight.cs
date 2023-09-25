using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModificatorFight : MonoBehaviour
{

    [SerializeField] private float _coefForDefense;
    [SerializeField] private float _coefForAttack;
   
    public float GetCoefModificatorAttack() => _coefForAttack;
    public float GetCoefModificatorDefense() => _coefForDefense;

    public virtual void AddModificator(IModificatorFight objForModificator)
    {
        objForModificator.AddModificator(this);
    }

    public virtual void RemoveModificator(IModificatorFight objForModificator)
    {
        objForModificator.DeleteModificator(this);
    }
}




