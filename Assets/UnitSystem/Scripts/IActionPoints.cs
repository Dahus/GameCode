using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionPoints
{
    void ResetActionPoints();
    void RestoreActionPoints();
    int StartActionPoints { get; set; }
    int ActionPoints { get; set; }
    int FightActionPoints { get; set; }

    void UpgradeStartActionPoints(int countPoint);
    void SubstractionActionPoints(int countPoint);


}
