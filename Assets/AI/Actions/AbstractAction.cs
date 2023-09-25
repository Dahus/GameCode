using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAction : IUserAction
{
    public abstract Coefficients Coefficients { get; }
    protected abstract Coefficients GetCoefficients();

    public event Action BeforeExecute;
    public event Action AfterExecute;

    public void Execute()
    {
        BeforeExecute?.Invoke();
        OnExecute();
        AfterExecute?.Invoke();
    }

    protected abstract void OnExecute();

    public abstract List<IUserAction> GetNextActions();
}
