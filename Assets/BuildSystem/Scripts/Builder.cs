using UnityEngine;
using Game.Grid;
using Game.BuildingSystem;
using Game.GridSystem;
using Game.UnitsSystem;
namespace Game.Player
{
    public class Builder : MonoBehaviour
    {

        public static Builder instance;
        [SerializeField] private FactoryForBuild _factoryBuilds;
        [SerializeField] private FactoryForUnits _factoryUnits;
        
        //[SerializeField] private ResourcePlayer _resourcePlayer;
        


        public void SetupBuilder(FactoryForBuild factory, FactoryForUnits factoryUnits)
        {
            _factoryBuilds=factory;
            _factoryUnits=factoryUnits;
           // _resourcePlayer=resourcePlayer;
        }
        private void Awake()
        {

            if (instance == null)
            {
                instance = this;
            }
            else if (instance == this)
            {
                Destroy(gameObject);
            }
        }
        
        
        //public void BuildFactory()
        //{
        //    Debug.Log("Build");
        //    GameObject place = Observer.instance.GetHexagon();
        //    place.GetComponent<GridHex>().HexLog();
        //    string name = Observer.instance.GetNameBuild();
        //    if(Observer.instance.Check(place.GetComponent<GridHex>()))
        //    _factoryBuilds.CreateBuild(name, place);
        //}

      /*  public void BuildUnit()
        {
            Debug.Log("Unit");
            GameObject place = Observer.instance.GetHexagon();
            
            place.GetComponent<GridHex>().HexLog();
            string name = Observer.instance.GetNameBuild();
            if(Observer.instance.Check(place.GetComponent<GridHex>()))
            _factoryUnits.CreateUnit(name,place);
        }
      */

        public FactoryForBuild GetFactoryForBuild()
        {
            return _factoryBuilds;
        }

        public FactoryForUnits GetFactoryForUnits()
        {
            return _factoryUnits;
        }
      
    }
}