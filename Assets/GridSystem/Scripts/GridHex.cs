using System.Collections;
using System.Collections.Generic;
using Game.Economics;
using Game.GlobalData;
using UnityEngine;
using TMPro;

using Game.FightSystem;

namespace Game.GridSystem
{

    public class GridHex : MonoBehaviour, IColorize
    {
        [SerializeField] private GameObject _currentObject;
        [SerializeField] private GridHexInfo _hexInfo;
        [SerializeField] private List<GridHex> _neighbourArray = new List<GridHex>();

        private DictionaryResource _resourceHex = new DictionaryResource();
        [SerializeField] private List<GameObject> _objectInHexagon = new List<GameObject>();
        [SerializeField] private HexInfoPathfinder _hexInfoPathfinder;

        [SerializeField] private int _numberDot;


        private Material material;
        public Vector2Int PositionInGrid { get; set; }


        public HexInfoPathfinder HexInfoPathfinder { get => _hexInfoPathfinder; }
        public GridHexInfo HexInfo { get => _hexInfo; }
        public List<GridHex> NeighbourArray { get => _neighbourArray; set => _neighbourArray = value; }
        public GridHex GetRandomNeighbour() => _neighbourArray[Random.Range(0, _neighbourArray.Count)];
        public ResourceType ResourceType
        {
            get => _hexInfo.ResourceType;
            set
            {
                var mat = GetComponent<SpriteRenderer>().material;
                mat.SetTexture("_ResourceTex", HexTextureContainer.GetResourceTex(value));
                mat.SetFloat("_HasResource", value != ResourceType.None ? 1 : 0);
                _hexInfo.ResourceType = value;
            }
        }
        public BiomType BiomType { get => _hexInfo.BiomType; set => _hexInfo.BiomType = value; }
        public int BiomeNumber { get; set; }

        public int NumberDot { get => _numberDot; set => _numberDot = value; }
        // public bool IsResource { get; set; } = false;
        public bool IsUseGeneration { get; set; } = false;

        public ReliefType ReliefType { get => _hexInfo.ReliefType; set => _hexInfo.ReliefType = value; }
        public int ReliefNumber { get; set; }
        public DictionaryResource ResourceHex
        {
            get => _resourceHex;
            set => _resourceHex = value;
        }
        public GameObject[] ObjectsInHex { get => _objectInHexagon.ToArray(); }

        public Color StartColor => GetComponent<SpriteRenderer>().color;

        public void AddObjectInHex(GameObject newObject) => _objectInHexagon.Add(newObject);
        public void RemoveObjectInHex(GameObject objectToRemove) => _objectInHexagon.Remove(objectToRemove);
        public void RemoveObjectInHex(int objectToRemoveIndex) => _objectInHexagon.RemoveAt(objectToRemoveIndex);
        public (GameObject, GridHexInfo, GameObject[]) CopyObserveData() =>
            (_currentObject, _hexInfo.Clone() as GridHexInfo, _objectInHexagon.ToArray());

        private void Awake()
        {
            _hexInfoPathfinder = new HexInfoPathfinder();
            _hexInfo = new GridHexInfo();
            material = GetComponent<SpriteRenderer>().material;
        }

        public void HighLite(Color color)
        {
            GetComponent<SpriteRenderer>().color = color;
        }

        public void SetColor(int number, Color color)
        {
            switch (number)
            {
                case 1:
                    material.SetColor("_Color1", color);
                    break;
                case 2:
                    material.SetColor("_Color2", color);
                    break;
            }
        }

        public void SetSpriteBuild(Sprite sprite)
        {
            material.SetTexture("_ReliefTex", sprite.texture);
        }


    }
}



/*  public class GridHex : MonoBehaviour, IPatency
  {
      [SerializeField] private GameObject _currentObject;
      [SerializeField] private ReliefAndBiom _reliefAndBiom;
      [SerializeField] private ResourceType _typeRes;
      [SerializeField] private List<GridHex> neighbourArray = new List<GridHex>();
      [SerializeField] private bool walkable = true;
      [SerializeField] private Color _currentColor;
      [SerializeField] private int _numberBiome;
      [SerializeField] private bool _isUseBiome = false;
      public bool isReturn = false;




      private int _modPatency;
      [SerializeField] private int _coefPatency;
      private DictionaryResource _resourceHex = new DictionaryResource();
      private Dictionary<string, GameObject> _objectInHexagon = new Dictionary<string, GameObject>();
      [SerializeField] private GameObject _objInHex;
      [SerializeField] private int _countObject = 0;
      public List<GridHex> NeighbourArray { get => neighbourArray; set => neighbourArray = value; }

      #region Biome
      public bool GetUseBiome() => _isUseBiome;
      public void SetNumberBiome(int numberBiome) => _numberBiome = numberBiome;
      public int GetNumberBiome() => _numberBiome;

      public void GrowthBiome()
      {
          _isUseBiome = true;
          foreach (GridHex hex in neighbourArray)
          {
              if (hex.GetNumberBiome() == -1)
              {
                  hex.SetNumberBiome(_numberBiome);
              }
          }
      }

      public GridHex GetRandomNeighbour()
      {
          var rand = Random.Range(0, neighbourArray.Count);
          return neighbourArray[rand];
      }

      public bool CheckNeighbourBiome()
      {
          int count = 0;
          bool flag = true;
          foreach (GridHex hex in neighbourArray)
          {
              if (hex._numberBiome == _numberBiome)
              {
                  count++;
              }
          }
          if (count == 0)
          {
              flag = false;
          }
          return flag;
      }

      public bool GetUseBiome()
      {
          return _isUseBiome;
      }
      public void SetNumberBiome(int numberBiome)
      {
          _numberBiome = numberBiome;
      }

      public int GetNumberBiome()
      {
          _isUseBiome = true;
          foreach (GridHex hex in neighbourArray)
          {
              if (hex.GetNumberBiome() == -1)
              {
                  hex.SetNumberBiome(_numberBiome);
              }
          }
      }

      #endregion

      #region ObjInHexagon
      public Dictionary<string, GameObject> GetObjectsInHexagon() => _objectInHexagon;
      public int CountObjectInHex() => _countObject;
      public void SetObjectInHexagon(GameObject obj, string strType)
      {
          _objectInHexagon.Add(strType, obj);
          _countObject++;
          _objInHex = obj;

              if (_objInHex.TryGetComponent(out IModificatorFight objForModificator))
              {
                  foreach (ModificatorFight mod in _modificators)
                  {
                      mod.AddModificator(objForModificator);
                  }
              }
      }
      public GameObject GetObjectInHexagon(string name)
      {
          _currentObject = _objectInHexagon[name];
          return _currentObject;
      }
      public void DeleteObjectInHex(string strType)
      {
          var obj = _objectInHexagon[strType];
          _objectInHexagon.Remove(strType);
          _countObject--;


          if (obj.TryGetComponent(out IModificatorFight objForModificator))
          {
              foreach (ModificatorFight mod in _modificators)
              { 
                  mod.RemoveModificator(objForModificator);
              }
          }

          if (_countObject == 0)
          {
              _objInHex = null;
          }
      }

      #endregion


      #region A*
      public int GetModPatency() => _modPatency;
      public float GetCoefPatency() => _coefPatency;
      public void SetupCoefPatency(int coef) => _coefPatency = coef;
      public bool Walkable { get => walkable; set => walkable = value; }

      #endregion

      #region Resource
      public DictionaryResource GetResourceHex() => _resourceHex;
      public void SetResourceHex(DictionaryResource resource) => _resourceHex = resource;

      public void SetTypeResource(ResourceType type) => _typeRes = type;
      public ResourceType GetTypeResource() => _typeRes;

      #endregion






      public void SetPosition(Vector3 newPosition)
      {
          transform.position = newPosition;
      }

      public GameObject GetGridHex()
      {
          return gameObject;
      }

      public void SetTypeResource(ResourceType type)
      {
          _typeRes = type;
      }

      public void SetupCoefPatency(int coef)
      {
          _coefPatency = coef;
      }

      public ResourceType GetTypeResource()
      {
          return _typeRes;
      }

      public Dictionary<string, GameObject> GetObjectsInHexagon()
      {
          return _objectInHexagon;
      }

      public GameObject GetObjectInHexagon(string name)
      {
          _currentObject = _objectInHexagon[name];
          return _currentObject;
      }

      public int CountObjectInHex()
      {
          return _objectInHexagon.Count;
      }



      public int GetModPatency()
      {
          return _modPatency;
      }

      public DictionaryResource GetResourceHex()
      {
          return _resourceHex;
      }

      public void SetResourceHex(DictionaryResource resource)
      {
          _resourceHex = resource;
      }

      public void SetSpriteHex(Sprite sprite)
      {
          GetComponent<SpriteRenderer>().sprite = sprite;
      }

      public void SetColorHex(Color color)
      {
          GetComponent<SpriteRenderer>().color = color;
      }

      public void SetObjectInHexagon(GameObject obj, string strType)
      {
          _objectInHexagon.Add(strType, obj);

      }



      public void DeleteObjectInHex(string strType)
      {
          _objectInHexagon.Remove(strType);
      }



      public void RememberColor(Color color)
      {
          _currentColor = color;
      }

      public Color GetRememberColor()
      {
          return _currentColor;
      }

      public void AddModificator(ModificatorFight modificator)
      {
          Debug.LogError("�������� �����������" + modificator.name);
          _modificators.Add(modificator);
      }

      public void DeleteModificator(ModificatorFight modificator)
      {
          Debug.LogError("������ �����������" + modificator.name);
          var flag = modificator == _modificators[0];
          Debug.LogError("����" + flag);
          _modificators.Remove(modificator);
          Debug.LogError(_modificators.Count + _modificators[0].name);
      }
  }
}*/
