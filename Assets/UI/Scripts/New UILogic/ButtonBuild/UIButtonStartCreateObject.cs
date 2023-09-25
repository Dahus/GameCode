using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Game.BuildingSystem;
namespace Game.UI
{
    public class UIButtonStartCreateObject : UIButtonBuild
    {

        [SerializeField] private CreateStartObject _startCreateObject;



        public override void Setup()
        {
            ClearActions();
           
        //    AddActions(CreateUnit);
       //     AddActions(Attack);
        }

        private void Start()
        {
            _startCreateObject = GetComponent<CreateStartObject>();
        }


        private void Repair()
        {
           // _startCreateObject.
        }

    }
}
