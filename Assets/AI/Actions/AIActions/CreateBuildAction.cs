using Game.GridSystem;
using Game.Player;
using System.Collections.Generic;
using Game.GlobalData;
using Game.GlobalSystems;

public class CreateBuildAction : AbstractAction
{
    private readonly PlayerManager _player;
    private readonly string _nameBuild;

    public CreateBuildAction(PlayerManager player, string nameBuild)
    {
        _player = player;
        _nameBuild = nameBuild;
    }

    public override Coefficients Coefficients => GetCoefficients();

    public override List<IUserAction> GetNextActions()
    {
        return new List<IUserAction>();
    }

    protected override Coefficients GetCoefficients()
    {
        var builds = _player.GetPlayerBuilds();
        foreach (var build in builds)
        {
            if (ConstantNameBuild.ROBOTNODE == build.GetName())
                return new Coefficients(2, 2, 2);
        }
        return new Coefficients(1000, 1000, 1000);
    }

    protected override void OnExecute()
    {
        var hexes = EntityLocator.instance.GetWavedAlgorithm().SearchHexesWithNotWalkalbe(_player.GetPlayerBuilds()[0].GetPlaceGridHex(), 2);
        var place = GameGrid.GetRandomHexInGrid();
        while (!hexes.Contains(place))
        {
            place = GameGrid.GetRandomHexInGrid();
        }
        _player.GetFactoryBuild().CreateBuild(_nameBuild, place.gameObject);
    }
}
