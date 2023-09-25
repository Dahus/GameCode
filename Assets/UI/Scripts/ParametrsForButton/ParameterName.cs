
using System;

[Serializable] 
public class ParameterName : IReturnObjects
{

    public string Name;
    public object[] ReturnObjects()
    {
        return new object[] { Name };
    }
}
