using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Game.BuildingSystem;
using Game.FightSystem;
using UnityEngine;
using Game.Grid;
using Game.Player;
using Game.GlobalData;
using Game.GridSystem;
using UnityEngine.Serialization;
using Game.GlobalSystems;

namespace Game.UnitsSystem
{
    public abstract class AbstractUnit : MonoBehaviour, IPatency, IDamage, IStepPlayer, IActionPoints, IAttack, IColorize, IModificatorFight, IUserInteractable, IPlayerOwnership, IHexObserver
    {
        public HealthBar Healthbar { get; set; }
        public int StartActionPoints { get => _startActionPoints; set => _startActionPoints = value; }
        public int ActionPoints { get => _actionPoints; set => _actionPoints = value; }
        public int FightActionPoints { get => _fightActionPoint; set => _fightActionPoint = value; }

        public PlayerManager Player => _player;

        public TypeAttack AttackType => _typeAttack;

        public Color StartColor => _player.GetColor();

        public List<GameObject> objectForAttack => _objectsForAttack;

        public List<ModificatorFight> Modificators => _modificators;

        public bool CanEndStep { get => canEndStep; set => canEndStep=value; }

       [SerializeField] private bool canEndStep;

        private string _typeObj = ConstantDictionaryObjectIHex.UNIT;
        private bool _isFighting = true;

        [SerializeField] private TypeUnit _unitType;

        [Header("��������� �����")]
        [SerializeField] protected TypeAttack _typeAttack;
        [SerializeField] protected float _heatlh;
        [SerializeField] protected float _maxHealth;
        [SerializeField] protected float _armor;
        [SerializeField] protected float _damageMelee;
        [SerializeField] protected float _damageDistance;
        [SerializeField] protected float _damageForBuild;
        [SerializeField] protected int _radiusDamage;
        [SerializeField] private string _name;
        [SerializeField] private float _coefMobility;
        [SerializeField] private int _actionPoints;
        [SerializeField] private UnitsData _unitsData;
        [SerializeField] private StateUnit _state = StateUnit.Nun;
        [SerializeField] private int _startActionPoints;
        [SerializeField] private int _fightActionPoint;
        [SerializeField] private int _radiusView;

        private SpriteRenderer _sprite;

        [Header("��������� ������������")]
        [SerializeField] private float _coefPatency;

        [Header("��������� ��������")]
        [SerializeField] protected GridHex _startHex;
        [SerializeField] protected PathfindingWithCoef _pathfinding;
        [SerializeField] protected WavedAlgorithm _wavedAlgorithm;
        [SerializeField] private List<GridHex> _gridHexsMovement = new List<GridHex>();
        [SerializeField] private int currentId;
        private GridHex _currentHex;
        private GridHex _endHex;
        private List<GameObject> _objectsForAttack = new List<GameObject>();
        [SerializeField] private List<ModificatorFight> _modificators = new List<ModificatorFight>();


        [SerializeField] protected PlayerManager _player;

        

        #region  Setup 
        public virtual void Setup(GameObject place, PlayerManager player, PathfindingWithCoef pathfinding)
        {
            _pathfinding = pathfinding;
            _wavedAlgorithm = pathfinding.GetComponent<WavedAlgorithm>();
            _player = player;
            _startHex = place.GetComponent<GridHex>();

            CanEndStep = true;
            GetComponent<SpriteRenderer>().color = player.GetColor();
            place.GetComponent<GridHex>().AddObjectInHex(gameObject);
            _player.AddObjectInListAddObject(gameObject);
            SetStateUnit(StateUnit.Nun);
            SetupData();
            Healthbar = player.GetDrawHealthBar().CreateHealthBar(new Vector2(place.transform.position.x, place.transform.position.y), gameObject.transform);
            Healthbar.Setup(_heatlh);
            _sprite = GetComponent<SpriteRenderer>();
        }

        public void SetupData()
        {
            _heatlh = _unitsData.GetHealthUnit();
            _maxHealth = _heatlh;
            _damageMelee = _unitsData.GetDamageMelee();
            _damageDistance = _unitsData.GetDamageDistance();
            _damageForBuild = _unitsData.GetDamageForBuild();
            StartActionPoints = _unitsData.GetActionPoints();
            ActionPoints = StartActionPoints;
            _armor = _unitsData.GetArmor();
            _name = _unitsData.GetName();
            FightActionPoints = _unitsData.GetFightActionPoint();
            _radiusDamage = _unitsData.GetRadiusDamage();
            SetupActions();
        }
        #endregion
        #region Move
        private bool _isMoving = false;

        public bool IsMoving()
        {
            return _isMoving;
        }

        public virtual void Movement(GridHex end)
        {
            CanEndStep = false;
            _isMoving = true;
            PathFind(end);
            StartCoroutine(Expect(_gridHexsMovement));
        }
        private void PathFind(GridHex end)
        {
            _gridHexsMovement.Clear();
            _isMoving = true;
            _endHex = end;
            _gridHexsMovement = _pathfinding.FindHexPath(_startHex, end);
        }

        private IEnumerator CoroutineMove(List<GridHex> hexs, int id)
        {
            
            hexs[currentId - 1].RemoveObjectInHex(this.gameObject);
            _startHex = hexs[currentId - 1];

            for (int i = currentId; i < id; i++)
            {

                while (Vector2.Distance(transform.position, hexs[i].transform.position) > 0.2f)
                {
                    transform.position =
                        Vector2.MoveTowards(transform.position, hexs[i].transform.position, 2 * Time.deltaTime);
                    yield return null;
                }
                TermonationObservation();
                _startHex = hexs[i];
                ObservationArea();
            }
            currentId = id;
            _currentHex = hexs[currentId];
            while (Vector2.Distance(transform.position, _currentHex.transform.position) > 0.2f)
            {
                transform.position =
                    Vector2.MoveTowards(transform.position, _currentHex.transform.position, 2 * Time.deltaTime);
                yield return null;
            }
            if(currentId == id)
            {
                CanEndStep = true;
            }
            transform.position = _currentHex.transform.position;
            _currentHex.AddObjectInHex(gameObject);
            TermonationObservation();
            _startHex = _currentHex;
            ObservationArea();
            _state = _currentHex != _endHex ? StateUnit.Walked : StateUnit.Nun;
        }

        private int CalculatePath(List<GridHex> hexs)
        {
            int id = currentId;
            int returnId = 0;
            for (int i = id; i < hexs.Count; i++)
            {
                if (hexs[i].HexInfoPathfinder.GetCoefPatency() <= ActionPoints)
                {
                    SubstractionActionPoints((int)hexs[i].HexInfoPathfinder.GetCoefPatency());
                    returnId = i;
                }
                else
                {
                    break;
                }
            }
            return returnId;
        }

        #endregion
        #region Damage
        public virtual void TakeDamage(float damage)
        {
            _heatlh -= damage;
            Healthbar.UpdateValue(_heatlh);
        }
        public void DestroyObject()
        {
            _startHex.RemoveObjectInHex(this.gameObject);
            _player.AddObjectInListRemoveObject(gameObject);
            gameObject.SetActive(false);
        }

        public bool CheckDeath()
        {
            if (_heatlh <= 0)
            {
                DestroyObject();
                return true;
            }
            return false;
        }
        public void CancelFunction() { }
        public string GetTypeObInHex() => _typeObj;
        public GridHex GetPlaceGridHex() => _startHex;
        #endregion
        #region Get
        public GridHex GetPlace() => _startHex;
        public string GetName() => _unitsData.GetName();
        public float GetCoefPatency() => _coefPatency;
        public float GetCoefMobility() => _coefMobility;
        public PlayerManager GetPlayerManager() => _player;
        public UnitsData GetUnitsData() => _unitsData;
        public float GetHealth() => _heatlh;
        public float GetDamage() => _damageMelee;
        public TypeUnit GetTypeUnit() => _unitType;
        public PathfindingWithCoef GetPathfinding() => _pathfinding;
        public int GetRadiusDamage() => _radiusDamage;

        public int GetLevel() => _unitsData.GetLevel();
        #endregion
        #region Step

        IEnumerator Expect(List<GridHex> hexs)
        {
            currentId = 1;
            var countHexs = CalculatePath(hexs);
            if (countHexs != 0)
            {
                yield return StartCoroutine(CoroutineMove(hexs, countHexs));
            }
        }

        public virtual void BeginPlayerTurn()
        {
            RestoreActionPoints();
            CanEndStep = true;
        }

        public virtual void CompletePlayerTurn()
        {

            /* Debug.LogError("����� ����� ����");
             bool flag = false;
             if (_state == StateUnit.Walked)
             {
                Movement(_endHex);
             }
             return true;*/
        }

        public IEnumerator CoroutineEndStep()
        {
            if (_state == StateUnit.Walked)
            {
                PathFind(_endHex);
                yield return StartCoroutine(Expect(_gridHexsMovement));
            }
        }

        #endregion
        #region ActionPoint
        public void ResetActionPoints()
        {
            ActionPoints = 0;
        }

        public void RestoreActionPoints()
        {
            ActionPoints = StartActionPoints;
        }

        public void UpgradeStartActionPoints(int countPoint)
        {
            StartActionPoints += countPoint;
        }

        public void SubstractionActionPoints(int countPoint)
        {

            ActionPoints -= countPoint;
        }
        #endregion
        #region Fight
        public virtual void AttackMelee(IDamage objectDamage, float coefModificators)
        {
            var damage = 0f;
            switch (objectDamage.GetTypeObInHex())
            {
                case ConstantDictionaryObjectIHex.UNIT:
                    damage = _damageMelee;
                    break;
                default:
                    damage = _damageForBuild;
                    break;
            }
            var resultCoef = coefModificators * damage;
            objectDamage.TakeDamage(damage + resultCoef);
            ResetActionPoints();
            CancelAttack();
        }
        public virtual void AttackDistance(IDamage objectDamage, float coefModificators)
        {
            var damage = 0f;
            switch (objectDamage.GetTypeObInHex())
            {
                case ConstantDictionaryObjectIHex.UNIT:
                    Debug.LogError("----����� �� ����� ������� ��� " + _damageDistance);
                    damage = _damageDistance;
                    break;
                default:
                    Debug.LogError("----����� �� ������ ������� ��� " + _damageForBuild);
                    damage = _damageForBuild;
                    break;
            }

            var resultCoef = coefModificators * damage;
            Debug.LogError("�����������: " + resultCoef + " ����: " + damage + " ������������: " + (damage + resultCoef));
            CreatePatron(damage + resultCoef, objectDamage);
        }
        public virtual void CreatePatron(float damage, IDamage objectDamage) { }
        public void GetReadyAttack()
        {
            foreach (GameObject obj in TryGetOpponents(_radiusDamage))
            {
                obj.GetComponent<IColorize>().HighLite(Color.magenta);
                _objectsForAttack.Add(obj);
            }
        }
        public void CancelAttack()
        {
            foreach (GameObject obj in TryGetOpponents(_radiusDamage))
            {
                IColorize colorize = obj.GetComponent<IColorize>();
                colorize.HighLite(colorize.StartColor);
            }
            objectForAttack.Clear();
        }


        #endregion
        #region ModificatorFight
        public void AddModificator(ModificatorFight modificator)
        {
            _modificators.Add(modificator);

        }

        public void DeleteModificator(ModificatorFight modificator)
        {
            _modificators.Remove(modificator);
        }
        #endregion

        #region Upgrade
        public virtual void UpgradeHealth(float addHealth)
        {
            _heatlh += addHealth;
            _maxHealth += addHealth;
            Healthbar.UpgradeHealth(addHealth);
        }
        public virtual void UpgradeDamage(float damage)
        {
            _damageDistance += damage;
            _damageMelee += damage;
            _damageForBuild += damage;
        }

        public void UpgradeActionPoint(int addActionPoint)
        {
            _startActionPoints += addActionPoint;
        }

        public virtual void UpgradeSpeedDeployd() { }

        public virtual void UpgradeUnit()
        {

        }
        #endregion
        public void SetStateUnit(StateUnit state) => _state = state;

        public void SetPlaceHex(GridHex hex) => _startHex = hex;
        public void HighLite(Color color)
        {
            _sprite.color = color;
        }

        protected List<GameObject> TryGetOpponents(int radius)
        {
            List<GameObject> enemies = new List<GameObject>();
            var radiusDamage = _pathfinding.GetComponent<WavedAlgorithm>().SearchHexesWithNotWalkalbe(_startHex, _radiusDamage);
            foreach (GridHex hex in radiusDamage)
            {
                var objsInHex = hex.ObjectsInHex;
                if (objsInHex.Length <= 0)
                    continue;

                foreach (var objInHex in objsInHex)
                {
                    if (objInHex.TryGetComponent(out IDamage damageObj) && damageObj.Player != _player)
                    {
                        enemies.Add(objInHex);
                    }
                }
            }
            return enemies;
        }

        protected List<IUserAction> _userActions;

        public virtual void SetupActions()
        {
            _userActions = new List<IUserAction>();
        }
        public virtual List<IUserAction> GetActions()
        {
            return _userActions;
        }
        public PlayerManager GetPlayer()
        {
            return _player;
        }

        #region HexObserve
        public void ObservationArea()
        {
            var hexes = EntityLocator.instance.GetWavedAlgorithm().SearchHexesWithNotWalkalbe(_startHex.GetComponent<GridHex>(), _radiusView);
            hexes.Add(_startHex.GetComponent<GridHex>());
            var hexesData = hexes.Select(hex => (hex.PositionInGrid, new HexObserveData(hex.gameObject, hex.HexInfo, hex.ObjectsInHex.ToList()))).ToArray();
            Player.GetObserverMap().ObservationHex(hexesData, this);
            ColorizeHexes.Colorize(hexes, Player);
        }

        public void TermonationObservation()
        {
            var hexes = EntityLocator.instance.GetWavedAlgorithm().SearchHexesWithNotWalkalbe(_startHex, _radiusView);
            hexes.Add(_startHex.GetComponent<GridHex>());
            Player.GetObserverMap().TerminationObservationHex(hexes.Select(hex => hex.PositionInGrid).ToArray(), this);
            ColorizeHexes.Colorize(hexes, Player);
        }
        #endregion
    }

    public enum TypeUnit
    {
        Infantry,
        Technique
    }
}