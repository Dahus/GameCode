using System.Collections;
using System.Collections.Generic;
using Game.GlobalData;
using UnityEngine;
namespace Game.BuildingSystem
{
    [CreateAssetMenu(fileName = "Build", menuName = "Build", order = 1)]
    public class BuildData : ScriptableObject
    {
        [SerializeField] private float _lifeBuild;
        [SerializeField] private int _timeBuilding;
        [SerializeField] private string _nameBuild;
        [SerializeField] private Texture bildTexture;

        public float GetLifeBuild() => _lifeBuild;
        public int GetTimeBuilding() => _timeBuilding;
        public string GetName() => _nameBuild;
        public Texture BildTexture => bildTexture;


    }
}

