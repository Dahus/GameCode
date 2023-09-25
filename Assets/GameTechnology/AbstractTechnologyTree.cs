using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTechnologyTree : AbstractTechnology
{
    protected List<AbstractTechnology> _technologies;
    protected List<AbstractTechnology> _techonologiesBuy;

    public void AddTechnologyBuy(AbstractTechnology technology)
    {
        if (_techonologiesBuy.Contains(technology))
        {
            Debug.LogError("Такая технология уже существует");
            return;
        }
        Debug.LogError("Игрок купил: " + technology.GetNameTechnology());
        _techonologiesBuy.Add(technology);
        
    }
    public void AddTechnology(AbstractTechnology technology)
    {
        if (_technologies.Contains(technology))
        {
            Debug.LogError("Такая технология уже существует");
            return;
        }
        Debug.LogError("Игрок изучил: " + technology.GetNameTechnology());
        _technologies.Add(technology);
    }
    public void RemoveTechnology(AbstractTechnology technology)
    {
        _technologies.Remove(technology);
    }

    public List<AbstractTechnology> GetTechnologies()
    {
        return _technologies;
    }

    public AbstractTechnology GetTechnology(string nameTechnology)
    {
        foreach (var technology in _technologies)
        {
            if (technology.GetNameTechnology() == nameTechnology)
                return technology;
        }
        return null;
    }

    public AbstractTechnology GetTechnologyBuy(string nameTechnology)
    {
        foreach (var technology in _techonologiesBuy)
        {
            if (technology.GetNameTechnology() == nameTechnology)
                return technology;
        }
        return null;
    }


    public bool CheckTechnologe(AbstractTechnology technology)
    {
        if (_techonologiesBuy.Contains(technology))
            return true;
        else return false;
    }
    public override void Operation(PlayerManager player) { }

    public virtual void SetupTechnologies(TechnologyInfo[] info) { }

}
