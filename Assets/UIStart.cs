using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIStart : MonoBehaviour
{
    GameOptions _gameOptions = new GameOptions();
    [SerializeField] GameObject _windowOfParameters;

    [Space]
    [SerializeField] Button reset;
    [SerializeField] Dropdown dropdownMountains;
    [SerializeField] Dropdown dropdownRuggedTerrain;
    [SerializeField] Dropdown dropdownPlain;
    [SerializeField] Dropdown dropdownNaturaTrails;
    [Space]
    [SerializeField] Dropdown dropdownMetalScaffolding;
    [SerializeField] Dropdown dropdownCrystalValley;
    [SerializeField] Dropdown dropdownBattlefield;
    [SerializeField] Dropdown dropdownStoneDesert;
    [SerializeField] Dropdown dropdownSwamp;
    [Space]
    [SerializeField] Dropdown dropdownTotal;
    //[SerializeField] ToggleGroup toggleGroupQuantity;
    [SerializeField] Toggle botSettingsOption1, botSettingsOption2, botSettingsOption3;

    [SerializeField] int testInt = 5;

    public void OpenParameter()
    {
        _windowOfParameters.SetActive(true);
    }

    public void CloseParameter()
    {
        _windowOfParameters.SetActive(false);
    }

    // обычный

    private void Awake()
    {

        // в C# всё обьекты. Массив - это бьект. Соответственной под него нужно выделять отдельно память, вызывая через new.
        //_gameOptions.ReliefSettings = new sbyte[3];

        /*
        _gameOptions.ReliefSettings[0] = 1;
        Debug.Log(_gameOptions.ReliefSettings[0]);
        */

        /*
        _gameOptions.Mountains = (sbyte)dropdownMountains.value;
        _gameOptions.RuggedTerrain = (sbyte)dropdownRuggedTerrain.value;
        _gameOptions.Plain = (sbyte)dropdownPlain.value;
        _gameOptions.NaturaTrails = (sbyte)dropdownNaturaTrails.value;
        */
        //Debug.Log(_gameOptions.Mountains);

        _gameOptions.ReliefSettings[0] = (sbyte)dropdownMountains.value;
        _gameOptions.ReliefSettings[1] = (sbyte)dropdownRuggedTerrain.value;
        _gameOptions.ReliefSettings[2] = (sbyte)dropdownPlain.value;
        _gameOptions.ReliefSettings[3] = (sbyte)dropdownNaturaTrails.value;

        _gameOptions.BiomeSettings[0] = (sbyte)dropdownMetalScaffolding.value;
        _gameOptions.BiomeSettings[1] = (sbyte)dropdownCrystalValley.value;
        _gameOptions.BiomeSettings[2] = (sbyte)dropdownBattlefield.value;
        _gameOptions.BiomeSettings[3] = (sbyte)dropdownStoneDesert.value;
        _gameOptions.BiomeSettings[4] = (sbyte)dropdownSwamp.value;

        _gameOptions.ResourceSettings[0] = 1;
    }

    public void ResetValues ()
    {
        dropdownMountains.value = 1;
        _gameOptions.ReliefSettings[0] = 1;
        dropdownRuggedTerrain.value = 1;
        _gameOptions.ReliefSettings[1] = 1;
        dropdownPlain.value = 1;
        _gameOptions.ReliefSettings[2] = 1;
        dropdownNaturaTrails.value = 1;
        _gameOptions.ReliefSettings[3] = 1;

        dropdownMetalScaffolding.value = 1;
        _gameOptions.BiomeSettings[0] = 1;
        dropdownCrystalValley.value = 1;
        _gameOptions.BiomeSettings[1] = 1;
        dropdownBattlefield.value = 1;
        _gameOptions.BiomeSettings[2] = 1;
        dropdownStoneDesert.value = 1;
        _gameOptions.BiomeSettings[3] = 1;
        dropdownSwamp.value = 1;
        _gameOptions.BiomeSettings[4] = 1;

        dropdownTotal.value = 1;
        _gameOptions.ResourceSettings[0] = 1;

        botSettingsOption2.isOn = true;
        _gameOptions.BotSettings[0] = 2;
    }

    // Работа с массивом
    void SelectOption(sbyte[] gameOptions, sbyte dropdownValue, sbyte index)
    {
        //Debug.Log("Число: " + gameOptions[1]);
        gameOptions[index] = dropdownValue;
        switch (gameOptions[index])
        {
            case 0:
                Debug.Log("Мало");
                break;
            case 1:
                Debug.Log("Средне");
                break;
            case 2:
                Debug.Log("Много");
                break;
        }
    }

    // Настройки рельефов
    public void SelectorOfMountains()
    {
        _gameOptions.ReliefSettings[0] = (sbyte)dropdownMountains.value;
        SelectOption(_gameOptions.ReliefSettings, (sbyte)dropdownMountains.value, 0);
    }
    public void SelectorOfRuggedTerrain()
    {
        _gameOptions.ReliefSettings[1] = (sbyte)dropdownRuggedTerrain.value;
        SelectOption(_gameOptions.ReliefSettings, (sbyte)dropdownRuggedTerrain.value, 1);
    }

    public void SelectorOfPlain()
    {
        _gameOptions.ReliefSettings[2] = (sbyte)dropdownPlain.value;
        SelectOption(_gameOptions.ReliefSettings, (sbyte)dropdownPlain.value, 2);
    }

    public void SelectorOfNaturaTrails()
    {
        _gameOptions.ReliefSettings[3] = (sbyte)dropdownNaturaTrails.value;
        SelectOption(_gameOptions.ReliefSettings, (sbyte)dropdownNaturaTrails.value, 3);
    }

    // Настройки биомов
    public void SelectorOfMetalScaffolding()
    {
        _gameOptions.BiomeSettings[0] = (sbyte)dropdownMetalScaffolding.value;
        SelectOption(_gameOptions.BiomeSettings, (sbyte)dropdownMetalScaffolding.value, 0);
    }

    public void SelectorOfCrystalValley()
    {
        _gameOptions.BiomeSettings[1] = (sbyte)dropdownCrystalValley.value;
        SelectOption(_gameOptions.BiomeSettings, (sbyte)dropdownCrystalValley.value, 1);
    }

    public void SelectorOfBattlefield()
    {
        _gameOptions.BiomeSettings[2] = (sbyte)dropdownBattlefield.value;
        SelectOption(_gameOptions.BiomeSettings, (sbyte)dropdownBattlefield.value, 2);
    }
    public void SelectorOfStoneDesert()
    {
        _gameOptions.BiomeSettings[3] = (sbyte)dropdownStoneDesert.value;
        SelectOption(_gameOptions.BiomeSettings, (sbyte)dropdownStoneDesert.value, 3);
    }

    public void SelectorOfSwamp()
    {
        _gameOptions.BiomeSettings[4] = (sbyte)dropdownSwamp.value;
        SelectOption(_gameOptions.BiomeSettings, (sbyte)dropdownSwamp.value, 4);
    }

    // Настройки ресурсов
    public void SelectorOfTotal()
    {
        _gameOptions.ResourceSettings[0] = (sbyte)dropdownTotal.value;
        SelectOption(_gameOptions.ResourceSettings, (sbyte)dropdownTotal.value, 0);
    }
    // Настройки ботов

    private void Start()
    {
        if (testInt == 6)
        {
            Debug.Log("Я сработал!");
        }

        botSettingsOption1.onValueChanged.AddListener((isSelected) =>
        {
            if (isSelected)
            {
                _gameOptions.BotSettings[0] = 1;
                Debug.Log(_gameOptions.BotSettings[0]);
            }
        }
        );

        botSettingsOption2.onValueChanged.AddListener((isSelected) =>
        {
            if (isSelected)
            {
                _gameOptions.BotSettings[0] = 2;
                Debug.Log(_gameOptions.BotSettings[0]);
            }
        }
        );

        botSettingsOption3.onValueChanged.AddListener((isSelected) =>
        {
            if (isSelected)
            {
                _gameOptions.BotSettings[0] = 3;
                Debug.Log(_gameOptions.BotSettings[0]);
            }
        }
        );
    }
}
