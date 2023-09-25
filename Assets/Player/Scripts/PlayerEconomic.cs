using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEconomic
{
    [SerializeField] private float commission;

    public float GetCommission()
    {
        return commission;
    }

    public void SetCommission(float commissionInside)
    {
        this.commission = commissionInside;
        Debug.Log("Записалось число комисии: " + commission);
    }
}