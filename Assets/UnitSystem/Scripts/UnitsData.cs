using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UnitsSystem
{
    [CreateAssetMenu(fileName = "Unit", menuName = "Unit", order = 1)]
    public class UnitsData : ScriptableObject
    {
        [SerializeField] private int _health;
        [SerializeField] private int _timeCreate;
        [SerializeField] private int _actionPoints;
        [SerializeField] private float _damageMelee;
        [SerializeField] private float _damageDistance;
        [SerializeField] private float _damageForBuild;
        [SerializeField] private string _name;
        [SerializeField] private Sprite icon;
        [SerializeField] private int _id;
        [SerializeField] private int _armor;
        [SerializeField] private int _fightActionPoints;
        [SerializeField] private int _radiusForFight;
        [SerializeField] private int _level;

        public int GetHealthUnit() => _health;
        public int GetTimeCreateUnit() => _timeCreate;
        public float GetDamageMelee() => _damageMelee;
        public float GetDamageDistance() => _damageDistance;
        public float GetDamageForBuild() => _damageForBuild;
        public int GetIdUint() => _id;
        public int GetArmor() => _armor;
        public int GetActionPoints() => _actionPoints;
        public string GetName()=> _name;
        public int GetFightActionPoint() => _fightActionPoints;
        public int GetRadiusDamage() => _radiusForFight;

        public Sprite GetIcon() => icon;
        public int GetLevel() => _level;




    }
}
