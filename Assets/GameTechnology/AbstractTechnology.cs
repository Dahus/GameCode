using System.Collections.Generic;
using Game.Economics;
using Game.Player;
using UnityEngine;

public abstract class AbstractTechnology
{
    private DictionaryResource _resourceTechnology;
    [SerializeField] private string nameTechnology;
    [SerializeField] private int countOfMovesToStudy = 0;
    [SerializeField] protected List<AbstractTechnology> necessaryPreviousTechnologies;
    [SerializeField] protected List<GameObject> impactObjects = new List<GameObject>();
    protected int currentMovesToStudy;
    public virtual void Operation(PlayerManager player)
    {
        if (!player.GetPlayerTechnologies().CheckTechnologe(this))
        {
            if (!CheckAvailabilityResourceTechnology(player.GetResources()))
            {
                Debug.LogError("Не хватает ресурсов, чтобы изучить: " + GetNameTechnology());
                return;
            }
            BuyTechnology(player);
        }
        else
        {
            player.GetPlayerTechnologies().Setup(player.GetPlayerTechnologies().GetTechnologyBuy(nameTechnology));
            player.currentTechnology = nameTechnology;
        }
        

       
    }

    public virtual void CallImpactObject(PlayerManager player) { }

    public virtual void SetupTechnology(string name, int countOfMaves, DictionaryResource resource, string predecessorName1, string predecessorName2, AbstractTechnologyTree treeTechnologies) { }

    public virtual void LearnTechnology(PlayerManager player)
    {
        currentMovesToStudy++;
        Debug.LogError("Step technology" + currentMovesToStudy + " " + nameTechnology);
        if(currentMovesToStudy >= countOfMovesToStudy)
        {
            CallImpactObject(player);
            player.GetPlayerTechnologies().AddTechnology(this);
            player.GetPlayerTechnologies().RemoveCurrentTechnology();
            player.learnTechnologies.Add(nameTechnology);
            player.currentTechnology = " ";
        }
     
    }

    public DictionaryResource GetResourceTechnology()
    {
        return _resourceTechnology;
    }

    public string GetNameTechnology()
    {
        return nameTechnology;
    }

    public void BuyTechnology(PlayerManager player)
    {
        Debug.LogError(player.name);
        SubstractResource(player);
        player.buyTechnologies.Add(nameTechnology);
        player.GetPlayerTechnologies().AddTechnologyBuy(this);
        player.GetPlayerTechnologies().Setup(player.GetPlayerTechnologies().GetTechnologyBuy(nameTechnology));
        player.currentTechnology = nameTechnology;
    }

    public int GetCountOfMovesToStudyTechnology()
    {
        return countOfMovesToStudy;
    }

    public List<AbstractTechnology> GetNecessaryPreviousTechnologies()
    {
        return necessaryPreviousTechnologies;
    }

    public AbstractTechnology GetPreviousTechnologies1()
    {
        return necessaryPreviousTechnologies[0];
    }



    public AbstractTechnology GetPreviousTechnologies2()
    {
        //Debug.LogError(necessaryPreviousTechnologies[necessaryPreviousTechnologies.Count - 1]);
        return necessaryPreviousTechnologies[necessaryPreviousTechnologies.Count-1];
    }

    public void AddNecessaryPreviousTechnology(AbstractTechnology technology)
    {
        necessaryPreviousTechnologies.Add(technology);
    }

    public void RemoveNecessaryPreviousTechnology(AbstractTechnology technology)
    {
        necessaryPreviousTechnologies.Remove(technology);
    }

    public void SetNameTechnology(string name)
    {
        nameTechnology = name;
    }

    public void SetCountOfMovesToStudyTechnology(int countOfMoves)
    {
        countOfMovesToStudy = countOfMoves;
    }

    public void SetResourceTechnology(DictionaryResource resource)
    {
        _resourceTechnology = new DictionaryResource();
        _resourceTechnology.SetupResource(resource);
    }

    protected bool CheckAvailabilityResourceTechnology(ResourcePlayer playerResource)
    {
        if (DictionaryResource.CheckAvailabilityResource(playerResource, _resourceTechnology))
            return true;
        return false;
    }

    protected void SubstractResource(PlayerManager player)
    {
        DictionaryResource.SubstractionResourcePlayer(player.GetResources(), _resourceTechnology);
        player.UpdateResource();
    }
}
