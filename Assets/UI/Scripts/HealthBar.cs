using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private float _maxHp;
    [SerializeField] private float _currentHp;
    private Renderer _render;

    private void Awake()
    {
        _render = GetComponent<Renderer>();
    }

    public void Setup(float maxHp)
    {
        _maxHp = maxHp;

    }

    public void UpgradeHealth(float addHp) 
    {
        _maxHp += addHp;
        _currentHp += addHp;
    }
    
    public void UpdateValue(float currentHp)
    {
        _render.material.SetTextureOffset("_MainTex", new Vector2(1-(currentHp / _maxHp), 0));
        _currentHp = currentHp;
    }

    public void SetState(bool newState)
    {
        _render.enabled = newState;
    }

}
