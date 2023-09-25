using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawHealthBar : MonoBehaviour
{
    [SerializeField] private HealthBar _prefabHealthBar;

    [SerializeField] private List<HealthBar> _healthBars;

   

    public HealthBar CreateHealthBar(Vector2 pos, Transform parent) 
    {
        HealthBar healthBar = Instantiate(_prefabHealthBar);
        healthBar.transform.position = new Vector2(pos.x, pos.y + 0.4f);
        healthBar.transform.SetParent(parent);
        _healthBars.Add(healthBar);
        return healthBar;
    }



    
    
}
