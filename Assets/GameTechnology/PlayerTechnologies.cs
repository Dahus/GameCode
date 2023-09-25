using System.Collections.Generic;

using Game.Player;

public class PlayerTechnologies : AbstractTechnologyTree
{

    private AbstractTechnology currentTechnology;
    
    
    public void SetupTechnologies()
    {
        _technologies = new List<AbstractTechnology>();
        _techonologiesBuy = new List<AbstractTechnology>();
    }

    public void SetupTechnologies(List<AbstractTechnology> technologies)
    {
        _technologies = new List<AbstractTechnology>();
        _technologies.AddRange(technologies);
    }

    public void Setup(AbstractTechnology technology) => currentTechnology = technology;

    public AbstractTechnology GetCurrentTechnology()
    {
        return currentTechnology;
    }
    public bool CheckCurrentTechnology()
    {
        if (currentTechnology != null)
            return true;
        else return false;
    }

    public void RemoveCurrentTechnology() => currentTechnology = null;

    public void Learn(PlayerManager player)
    {
        currentTechnology.LearnTechnology(player);
    }
     


}
