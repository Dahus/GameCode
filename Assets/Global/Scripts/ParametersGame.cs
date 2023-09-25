using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametersGame : MonoBehaviour
{
    [SerializeField] private int _playerCount;
    [SerializeField] private string[] _namePlayer;
    [SerializeField] private Color[] _colorPlayer;
    [SerializeField] private Sprite[] _iconPlayer;
    [SerializeField] private Vector2Int _gridSize;


    public int GetCountPlayer()
    {
        return _playerCount;
    }

    public Sprite GetIcon(int id) => _iconPlayer[id];

    public string GetNamePlayer(int id)
    {
        return _namePlayer[id];
    }

    public Color GetColorPlayer(int id)
    {
        return _colorPlayer[id];
    }

    public Vector2Int GetGridSize()
    {
        return _gridSize;
    }
}
