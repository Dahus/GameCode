using Game.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Game.GlobalSystems;
public class UIGeneralTechnologies : MonoBehaviour
{
    [SerializeField] private List<GameObject> _buttons;
    [SerializeField] private Image[] imagesGeneral;
    [SerializeField] private CSVReaderTechnology csvReaderTechnology;

    private List<AbstractTechnology> _technologies = new List<AbstractTechnology>();
    private StorageIcons storageIcons;

    private void Start()
    {
        storageIcons = EntityLocator.instance.GetStorageIcons();
        for (int i = 0; i < imagesGeneral.Length; i++)
        {
            UpdateImage(i);
        }
    }
    public void UpdateButtons(PlayerManager player)
    {
        _technologies = csvReaderTechnology.GetGeneralTechnologyTree();

        for (int i = 0; i < _buttons.Count; i++)
        {
            var x = i;
            _buttons[x].GetComponent<Button>().onClick.RemoveAllListeners();
        }

        for (int i = 0; i < _buttons.Count; i++)
        {
            var x = i;
            _buttons[x].GetComponent<Button>().onClick.AddListener(delegate { _technologies[x].Operation(player); });
            _buttons[x].GetComponent<Button>().GetComponentInChildren<Text>().text = _technologies[x].GetNameTechnology();
            
        }
    }

    public void UpdateImage(int id)
    {
        imagesGeneral[id].sprite = storageIcons.GetIcon(id);
    }
}
