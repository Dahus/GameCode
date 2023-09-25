using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Player;
using Game.UnitsSystem;
using Game.GlobalData;
using Game.GridSystem;

namespace Game.FightSystem
{
    public class FightManager : MonoBehaviour
    {
        [SerializeField] private GameObject _attacking;
        [SerializeField] private GameObject _defensing;
        [SerializeField] private float _coefDefense;
        [SerializeField] private float _coefAttack;
        [SerializeField] private List<ModificatorFight> _dopModificators;

        public void SetAttacking(GameObject attacking)
        {
            _attacking = attacking;
        }

        public void SetDefencing(GameObject defensing)
        {
            _defensing = defensing;
        }

        public void Fight()
        {
            DefinitionTypeFight();
            EndFight();
        }


        private void DefinitionTypeFight()
        {
            switch (_attacking.GetComponent<IAttack>().AttackType)
            {
                case TypeAttack.MelleFight:
                    MeleeFight();
                    break;
                case TypeAttack.DistanceFight:
                    DistantFight();
                    break;
            }
        }

        private void MeleeFight()
        {
            Debug.LogError("Ближний бой");
            CountModificators(_attacking, _defensing, out _coefAttack, out _coefDefense);
            
            _defensing.GetComponent<IAttack>().AttackMelee(_attacking.GetComponent<IDamage>(),_coefDefense);
            _attacking.GetComponent<IAttack>().AttackMelee(_defensing.GetComponent<IDamage>(),_coefAttack);
            _attacking.GetComponent<IDamage>().CheckDeath();
            if (_defensing.GetComponent<IDamage>().CheckDeath())
            {
                MoveUnitInHexDefense();
            }
        }

        private void DistantFight()
        {
            CountModificators(_attacking, _defensing, out _coefAttack, out _coefDefense);
            _attacking.GetComponent<IAttack>().AttackDistance(_defensing.GetComponent<IDamage>(), _coefAttack);
        }

        private void CountModificators(GameObject attack, GameObject defence, out float coefAttack,out float coefDefense)
        {
            
            coefAttack = 0;
            coefDefense = 0;
            var hex = defence.GetComponent<IDamage>().GetPlaceGridHex();
            var hexModificators = hex.GetComponent<ModificatorHex>();
            for(int i = 0; i < hexModificators.Modificators.Count; i++)
            {
                coefAttack += hexModificators.Modificators[i].GetCoefModificatorAttack();
                coefDefense += hexModificators.Modificators[i].GetCoefModificatorDefense();
            }

            if (defence.TryGetComponent(out IModificatorFight modificatorFights))
            {
                for (int i = 0; i < modificatorFights.Modificators.Count; i++)
                {
                    coefAttack += modificatorFights.Modificators[i].GetCoefModificatorAttack();
                    coefDefense += modificatorFights.Modificators[i].GetCoefModificatorDefense();
                }
            }
            if (coefAttack < 0)
            {
                coefAttack = 0.1f;
            }
        }
        
        private float CheckDopModifictors()
        {
            return 0;
        }

        private void MoveUnitInHexDefense()
        {
            if (!_attacking.GetComponent<IDamage>().CheckDeath())
            {
                if (_attacking.TryGetComponent(out AbstractUnit unit))
                {

                    var hexAttack = unit.GetPlaceGridHex();
                    hexAttack.RemoveObjectInHex(unit.gameObject);
                    var hexDefens = _defensing.GetComponent<IDamage>().GetPlaceGridHex();
                    hexDefens.AddObjectInHex(_attacking);
                    StartCoroutine(CoroutineMove(unit, hexDefens));

                }
            }
        }


        private IEnumerator CoroutineMove(AbstractUnit unit, GridHex hex)
        {
            while (Vector2.Distance(unit.transform.position, hex.transform.position) > 0.2f)
            {
                unit.transform.position =
                    Vector2.MoveTowards(unit.transform.position, hex.transform.position, 2 * Time.deltaTime);
                yield return null;
            }
            unit.transform.position = hex.transform.position;
            unit.SetPlaceHex(hex);

        }
        private void EndFight()
        {
            _attacking.GetComponent<IActionPoints>().ResetActionPoints();
            Observer.instance.SetStateObserver(StateObserver.Standart);
        }
    }
}
