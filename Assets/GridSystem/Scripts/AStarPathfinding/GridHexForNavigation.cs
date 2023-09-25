using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHexForNavigation : MonoBehaviour
{
    [SerializeField] private int marker =-1;

    public int Marker
    {
        get
        {
            return marker;
        }

        set
        {
            marker = value;
        }
    }
}
