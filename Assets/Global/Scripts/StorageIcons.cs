using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageIcons : MonoBehaviour
{
    [SerializeField] private Sprite[] iconsTechnologies;


    public Sprite GetIcon(int id) => iconsTechnologies[id];
}
