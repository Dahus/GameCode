using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Player;
using Game.GridSystem;

namespace Game.FightSystem
{
    public interface IDamage
    {
        void TakeDamage(float damage);

        PlayerManager Player { get; }

        bool CheckDeath();

        void DestroyObject();

        void CancelFunction();
        string GetTypeObInHex();

        GridHex GetPlaceGridHex();
    }
}

