using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using Game.Player;
using Game.UnitsSystem;
using Game.FightSystem;
using Game.UpgradeSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Game.UI
{
    public class UIButtonAutoScout : UIButtonUnit
    {
        AutoScout autoScout = new AutoScout();

        private void Start()
        {
            _unit = GetComponent<AbstractUnit>();
        }
        public override void Setup()
        {
            ClearActions();
            AddActions(Move);
            AddActions(FindingAnOre);
            AddActions(FindingAnEnemyUnit);
            AddActions(FindingAnEnemyBase);
        }

        public override void UpdateButton(List<Button> buttons, List<Text> texts)
        {
            buttons[0].onClick.AddListener(Move);
            texts[0].text = "Ходить";
            buttons[1].onClick.AddListener(FindingAnOre);
            texts[1].text = "Поиск руды";
            buttons[2].onClick.AddListener(FindingAnEnemyUnit);
            texts[2].text = "Поиск вражеского юнита";
            buttons[3].onClick.AddListener(FindingAnEnemyBase);
            texts[3].text = "Поиск вражеской базы";
        }


        private void Move()
        {
            Observer.instance.GetMoveObserver().SetUnit(_unit);
            Observer.instance.SetStateObserver(StateObserver.MoveObserver);
        }

        void FindingAnOre ()
        {
            Debug.Log("Ищу руду");
            autoScout.headTowardsTheGoal();
        }

        void FindingAnEnemyUnit ()
        {
            Debug.Log("Ищу вражеского юинта");
        }

        void FindingAnEnemyBase()
        {
            Debug.Log("Ищу вражескую базу");
        }

    }
}
