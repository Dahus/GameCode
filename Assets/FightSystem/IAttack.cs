using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.FightSystem
{
    public interface IAttack
    {
        void AttackMelee(IDamage objectDamage,float coefModificators);

        void AttackDistance(IDamage objectDamage, float coefModificators);

        void GetReadyAttack();

        TypeAttack AttackType{ get; }

        void CancelAttack();

        List<GameObject> objectForAttack { get; }

    }

    public enum TypeAttack
    {
        MelleFight,
        DistanceFight
    }
}
