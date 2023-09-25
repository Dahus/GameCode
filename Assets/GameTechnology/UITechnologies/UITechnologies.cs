using Game.Player;
using UnityEngine;

public class UITechnologies : MonoBehaviour
{
    [SerializeField] private GameObject technologyCanvas;
    [SerializeField] private GameObject generalTreeUIButtons;
    [SerializeField] private GameObject administratorTreeUIButtons;
    [SerializeField] private GameObject industrialTreeUIButtons;
    [SerializeField] private PlayerManager _currentPlayer;

    [SerializeField] private UIGeneralTechnologies _uiButtonsTechnologies;

    public void OpenTechnologiesTrees()
    {
        technologyCanvas.SetActive(true);
        Debug.LogError("Почему закрываюсь");
    }

    public void CloseTechnologiesTree()
    {
        technologyCanvas.SetActive(false);
    }

    public void SetActiveGeneralTree()
    {
        if (generalTreeUIButtons.activeSelf)
            generalTreeUIButtons.SetActive(false);
        else
            generalTreeUIButtons.SetActive(true);
    }

    public void UpdatePlayer(PlayerManager player)
    {
        _currentPlayer = player;
    }

    public void SetupUIButtonTechnologies()
    {
        _uiButtonsTechnologies.UpdateButtons(_currentPlayer);
    }
}
