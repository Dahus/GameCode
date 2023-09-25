using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.GlobalSystems;
using Game.Player;
using Game.FightSystem;
using Game.GridSystem;
using Game.GlobalData;
namespace Game.BuildingSystem
{
    public class CreateStartObject : MonoBehaviour, IStepPlayer, IDamage, IAttack, IColorize
    {
      
        [SerializeField] private AbstractBuild _build;
        [SerializeField] private PlayerManager _player;
        [SerializeField] private GameObject _place;
        [SerializeField] private int _countStep;
        [SerializeField] private float _maxLifeBuild;

        [SerializeField] private float _lifeBuild = 1;
        [SerializeField] private string _type;
        [SerializeField] private HealthBar _healthBar;
        public HealthBar Healthbar { get => _healthBar; set => _healthBar = value; }

        public PlayerManager Player => _player;

        public TypeAttack AttackType => TypeAttack.MelleFight;

        public Color StartColor => _player.GetColor();

        public List<GameObject> objectForAttack { get ; }
        public bool CanEndStep { get => canEndStep; set => canEndStep=value; }
        private bool canEndStep = false;

        private float _extraLife;
       [SerializeField] private bool _isBroken = false;
        private string _typeObj = ConstantDictionaryObjectIHex.BUILDPREPARATION;
        private SpriteRenderer _sprite; 

        public void SetupStartObject(AbstractBuild build, PlayerManager player, GameObject place, string str)
        {
            _build = build;
            _player = player;
            _place = place;
            Healthbar = player.GetDrawHealthBar().CreateHealthBar(new Vector2(_place.transform.position.x, _place.transform.position.y), gameObject.transform);
            _countStep = build.GetBuildData().GetTimeBuilding();
            _maxLifeBuild = build.GetBuildData().GetLifeBuild();
            Healthbar.Setup(_maxLifeBuild);
            Healthbar.UpdateValue(_lifeBuild);
            _extraLife = _maxLifeBuild / _countStep;
            _type = str;
            _sprite = GetComponent<SpriteRenderer>();
        }
        #region Step
        public void BeginPlayerTurn()
        {
            if (!_isBroken)
            {
                _lifeBuild += _extraLife;
                Healthbar.UpdateValue(_lifeBuild);
                if (_maxLifeBuild <= _lifeBuild)
                {
                    CreateBuild();
                }
            }
        }

        public IEnumerator CoroutineEndStep()
        {
            
            yield return null;
        }
        public void CompletePlayerTurn()
        {
            
        }
        #endregion

        #region Damage
        public void TakeDamage(float damage)
        {
            _lifeBuild -= damage;
            Healthbar.UpdateValue(_lifeBuild);
            _isBroken = false;
        }

        public bool CheckDeath()
        {
            if (_lifeBuild <= 0)
            {
                DestroyObject();
                return true;
            }
            return false;
        }
        public void DestroyObject()
        {
            CancelFunction();
            _place.GetComponent<GridHex>().RemoveObjectInHex(this.gameObject);
            _player.AddObjectInListRemoveObject(gameObject);
            gameObject.SetActive(false);
        }
        public void CancelFunction()
        {
            _isBroken = true;
        }
        public string GetTypeObInHex() => _typeObj;
        public GridHex GetPlaceGridHex() => _place.GetComponent<GridHex>();
        #endregion

        #region Attack
        public void AttackMelee(IDamage objectDamage, float coefModificators)
        {
            objectDamage.TakeDamage(0);
        }

        public void AttackDistance(IDamage objectDamage, float coefModificators) {}
        public void GetReadyAttack() { }
        public void CancelAttack() { }
        #endregion

        private void CreateBuild()
        {
            _place.GetComponent<GridHex>().RemoveObjectInHex(gameObject);
            _player.AddObjectInListRemoveObject(gameObject);

            gameObject.SetActive(false);
            _build.CreateBuilding(_place, _player);
        }

        public PlayerManager GetPlayerManager()
        {
            return _player;
        }

        public void HighLite(Color color)
        {
            _sprite.color = color;
        }

        public AbstractBuild GetBuild()
        {
            return _build;
        }

        public void Repair()
        {
                
        }
    }
}
