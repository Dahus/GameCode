using Game.GlobalData;
using Game.GlobalSystems;
using Game.GridSystem;
using Game.Player;
using System.Collections;
using System.Collections.Generic;
using Game.FightSystem;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Game.BuildingSystem
{
    public abstract class AbstractBuild : MonoBehaviour, IStepPlayer, IPatency, IDamage, IAttack, IColorize, IPlayerOwnership, IUserInteractable, IHexObserver
    {
        public HealthBar Healthbar { get; set; }
        public PlayerManager Player => _player;

        public TypeAttack AttackType => TypeAttack.DistanceFight;

        public Color StartColor { get => _player.GetColor(); }
        public List<GameObject> objectForAttack { get; }
        public bool CanEndStep { get => canEndStep; set => canEndStep = value; }

        private bool canEndStep;
        protected string _typeObj = ConstantDictionaryObjectIHex.BUILD;
        protected GameObject _place;
        protected PlayerManager _player;
        protected ResourcePlayer _resourcePlayer;
        protected bool _isBroken = false;
        private float _maxLife;
        private SpriteRenderer _sprite;
        // [SerializeField] private bool _active = true; // �� ����� ����� �����
        [Header("Параметры здания")]
        [SerializeField] private BuildData _buildData;
        [SerializeField] private GameObject _startObject;
        [SerializeField] protected float _life;

        [SerializeField] private float _coefPatency;

        [SerializeField] protected List<IUserAction> actionsBuilds;

        [SerializeField] protected int radiusView;

        #region Get
        public BuildData GetBuildData() => _buildData;
        public string GetName() => _buildData.GetName();
        public float GetCoefPatency() => _coefPatency;
        public PlayerManager GetPlayerManager() => _player;
        public GameObject GetPlace() => _place;
        public float GetLife() => _life;
        #endregion
        #region Create
        public bool CreateStartObject(GameObject place, PlayerManager player)
        {
            var hexes = EntityLocator.instance.GetWavedAlgorithm().SearchHexesWithNotWalkalbe(place.GetComponent<GridHex>(), 2);
            bool isAlliesNear = false;
            foreach (var hex in hexes)
            {
                foreach (var objInHex in hex.ObjectsInHex)
                {
                    if (objInHex.GetComponent<IPlayerOwnership>().GetPlayer() == player)
                        isAlliesNear = true;
                }
            }

            if (!isAlliesNear)
                return false;

            var obj = Instantiate(_startObject, place.transform.position, Quaternion.identity, player.transform);
            obj.GetComponent<CreateStartObject>().SetupStartObject(this, player, place, _typeObj);
            obj.GetComponent<SpriteRenderer>().color = player.GetColor();
            place.GetComponent<GridHex>().AddObjectInHex(obj);
            player.AddObjectPlayer(obj);
            return true;
        }

        public virtual bool CreateBuilding(GameObject place, PlayerManager player)
        {
            var obj = Instantiate(gameObject, place.transform.position, Quaternion.identity, player.transform);
            obj.TryGetComponent(out AbstractBuild build);
            build.SetupActions();
            var gridHex = place.GetComponent<GridHex>();
            var mat = place.GetComponent<Renderer>().material;
            mat.SetTexture("_BuildTex", _buildData.BildTexture);
            mat.SetColor("_PlayerColor", player.GetColor());
            mat.SetInt("_HasBuild", 1);
            gridHex.AddObjectInHex(obj);
            build._life = _buildData.GetLifeBuild();
            build.SetResourcePlayer(player.GetResources());
            build.SetPlayerManager(player);
            build.SetPlace(place);
            _maxLife = _life;
            player.AddObjectInListAddObject(obj);
            build.Healthbar = player.GetDrawHealthBar().CreateHealthBar(new Vector2(place.transform.position.x, place.transform.position.y), obj.transform);
            build.Healthbar.Setup(build._life);
            build.Healthbar.UpdateValue(build._life);
            var hexes = EntityLocator.instance.GetWavedAlgorithm()
                .SearchHexesWithNotWalkalbe(place.GetComponent<GridHex>(), radiusView);
            hexes.Add(place.GetComponent<GridHex>());
            var hexesData = hexes.Select(hex => (hex.PositionInGrid, new HexObserveData(hex.gameObject, hex.HexInfo, hex.ObjectsInHex.ToList())))
                .ToArray();

            player.GetObserverMap().ObservationHex(hexesData, this);
            return true;
        }
        public virtual void UpgradeBuilding(Dictionary<string, int> res)
        {

        }

        public virtual Dictionary<string, int> DisassemblyBuilding()
        {
            throw new System.NotImplementedException();
        }
        #endregion
        #region Step
        public virtual void BeginPlayerTurn()
        {
            //throw new System.NotImplementedException();
        }

        public virtual void CompletePlayerTurn()
        {
            //throw new System.NotImplementedException();
        }
        public IEnumerator CoroutineEndStep()
        {
            yield return null;
        }
        #endregion
        #region Damage
        public virtual void TakeDamage(float damage)
        {
            _life -= damage;
            Healthbar.UpdateValue(_life);
        }

        public bool CheckDeath()
        {
            if (_life <= 0)
            {
                DestroyObject();
                return true;
            }
            return false;
        }

        public virtual void DestroyObject()
        {
            CancelFunction();
            _place.GetComponent<GridHex>().RemoveObjectInHex(this.gameObject);
            _player.AddObjectInListRemoveObject(gameObject);
            TermonationObservation();
            gameObject.SetActive(false);
        }
        public virtual void CancelFunction()
        {

        }
        public GridHex GetPlaceGridHex() => _place.GetComponent<GridHex>();
        public string GetTypeObInHex() => _typeObj;

        #endregion
        #region Attack
        public void AttackMelee(IDamage objectDamage, float coefModificators)
        {
            objectDamage.TakeDamage(0);
        }
        public void AttackDistance(IDamage objectDamage, float coefModificators)
        {

        }
        public void GetReadyAttack()
        {

        }

        public void CancelAttack()
        {

        }
        #endregion
        #region SET
        public void SetPlace(GameObject place) => _place = place;
        public void SetPlayerManager(PlayerManager player) => _player = player;
        public void SetResourcePlayer(ResourcePlayer resourcePlayer) => _resourcePlayer = resourcePlayer;

        #endregion
        public void HighLite(Color color)
        {
            _sprite.color = color;
        }
        #region RepairSystem
        /*  public void Repair()
          {
              StartCoroutine(CoroutineRepair());
          }

          public IEnumerator CoroutineRepair()
          {

          }*/
        #endregion

        public PlayerManager GetPlayer()
        {
            return _player;
        }

        public virtual void SetupActions()
        {
            actionsBuilds = new List<IUserAction>();
        }

        public virtual List<IUserAction> GetActions()
        {
            return actionsBuilds;
        }

        #region HexObserver
        public void ObservationArea()
        {
            var hexes = EntityLocator.instance.GetWavedAlgorithm().SearchHexesWithNotWalkalbe(_place.GetComponent<GridHex>(), radiusView);
            hexes.Add(_place.GetComponent<GridHex>());
            var hexesData = hexes.Select(hex => (hex.PositionInGrid, new HexObserveData(hex.gameObject, hex.HexInfo, hex.ObjectsInHex.ToList()))).ToArray();
            Player.GetObserverMap().ObservationHex(hexesData, this);
            ColorizeHexes.Colorize(hexes, Player);
        }

        public void TermonationObservation()
        {
            var hexes = EntityLocator.instance.GetWavedAlgorithm().SearchHexesWithNotWalkalbe(_place.GetComponent<GridHex>(), radiusView);
            hexes.Add(_place.GetComponent<GridHex>());
            Player.GetObserverMap().TerminationObservationHex(hexes.Select(hex => hex.PositionInGrid).ToArray(), this);
            ColorizeHexes.Colorize(hexes, Player);
        }
        #endregion
    }
}
