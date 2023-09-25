using System;
using System.Collections.Generic;

public interface IUserAction
{
    event Action BeforeExecute;
    event Action AfterExecute;
    Coefficients Coefficients { get;}
    void Execute();
    List<IUserAction> GetNextActions();
}
