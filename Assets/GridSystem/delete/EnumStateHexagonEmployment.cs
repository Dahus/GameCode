using System;
namespace  Game.Grid
{
    [Serializable]
    public enum EnumStateHexagonEmployment
    {
        FortAndUnit,
        FortAndScout,
        BuildAndScout,
        UnitAndScout,
        Unit,
        Build,
        Scout,
        Fort,
        None
    }
}