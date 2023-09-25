using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelForParametres : MonoBehaviour
{

    [SerializeField] private string _name;
    [SerializeField] private Vector2 _position;
    [SerializeField] private int _countParametres;

    public string Name { get => _name; set => _name = value; }
    public Vector2 Position { get => _position; set => _position = value; }

    public int CountParametres { get => _countParametres; set => _countParametres = value; }

    public virtual void UpdateUIParametres(UIParametresLogic uiParametres) { }

    public virtual void UpdateInfoObject(GameObject obj) { }

}
