using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserInteractable
{
    void SetupActions();
    List<IUserAction> GetActions();
}
