using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Player;
using Game.Economics;
using Game.GlobalData;
using Game.GlobalSystems;

namespace Game.Market
{
    public class UIMarket : MonoBehaviour
    {
        [SerializeField] private GameObject _tradeMenu;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private KeepMarketPanel _MarketResource;
        [SerializeField] private GameObject _parentResourcePanel;
        [SerializeField] private DictionaryResource _dictionaryResource;
        [SerializeField] private KeepMarketPanel[] massiveText;

        private PlayerEconomic _playerEconomic;

        [SerializeField] private int _priceOfMetal;
        [SerializeField] private int _priceOfEnergyCrystall;
        [SerializeField] private int _priceOfNanostructure;

        [SerializeField] private int _offsetMetal;
        [SerializeField] private int _offsetEnergyCrystall;
        [SerializeField] private int _offsetNanostructure;

        //[SerializeField] private int _procentOfGoodsOnMarket;
        [SerializeField] private int _marketPriceChangeRate;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _commission;


        [SerializeField] int minimumSizeFinalPriceInt = 30;
        [SerializeField] int maximumSizeFinalPriceInt = 170;

        // Ending prices buy
        float theFinalPriceToBuyMetal;
        [SerializeField] int theFinalPriceToBuyMetalToInt;

        float theFinalPriceToSellMetal;
        [SerializeField] int theFinalPriceToSellMetalToInt;

        float theFinalPriceToBuyEnergyCrystall;
        [SerializeField] int theFinalPriceToBuyEnergyCrystallToInt;

        float theFinalPriceToSellEnergyCrystall;
        [SerializeField] int theFinalPriceToSellEnergyCrystallToInt;

        float theFinalPriceToBuyNanostructure;
        [SerializeField] int theFinalPriceToBuyNanostructureInt;

        float theFinalPriceToSellNanostructure;
        [SerializeField] int theFinalPriceToSellNanostructureInt;

        public void SetPlayer(PlayerManager player)
        {
            _playerManager = player;
            UpdateInfoPlayer();
        }

        void Start()
        {
            _playerEconomic = new PlayerEconomic(); // нужно PlayerEconomic засунуть в игрока!

            //InstantiateResourceInPanel();
        }

        

        /*
        void InstantiateResourceInPanel()
        {
            massiveText = new KeepMarketPanel[3];
            for (int i = 0; i < 3; i++)
            {
                var instanceMarketResource = Instantiate(_MarketResource, _parentResourcePanel.transform.position, Quaternion.identity, _parentResourcePanel.transform);
                massiveText[i] = instanceMarketResource; 
            }
            massiveText[0].GetText().text = "Кристаллы";
            massiveText[1].GetText().text = "Маталлы";
            massiveText[2].GetText().text = "Наноструктуры";
        }
        */

        public void OpenTrade()
        {
            // обновляем меню рынка

            _playerEconomic.SetCommission(_commission);
            _commission = _playerEconomic.GetCommission();

            theFinalPriceToBuyMetal = economicBuy(_priceOfMetal, _offsetMetal, _marketPriceChangeRate, _commission);
            theFinalPriceToBuyMetalToInt = Mathf.RoundToInt(theFinalPriceToBuyMetal);

            CountCoefTrade();


            _tradeMenu.SetActive(true);
        }

        public void CloseTrade()
        {
            _tradeMenu.SetActive(false);
        }

        public bool isSubtractResource(string nameResource, int countResource)
        {
            //Debug.LogError($"Resource ({nameResource}) : " + _manager.GetCurrentPlayer().GetResources().GetResourceDictionary().GetValue(nameResource) + " - " + $"{countResource}");
            /*
            if (_manager.GetCurrentPlayer().GetResources().GetResourceDictionary().GetValue(nameResource) - countResource >= 0)
            {
                return true;
            }
            */

            if (_playerManager.GetResources().GetResourceDictionary().GetValue(nameResource) - countResource >= 0)
            {
                return true;
            }
            Debug.Log("Колличество ресурса не может быть меньше 0");
            return false;
        }

        public bool numberComparison(int oldNumber, int newNumber)
        {
            if (newNumber != oldNumber)
            {
                return false;
            }
            return true;
        }

        public void BuyResource(ref int resourcePrice, ref int offset, ref float theFinalPriceBuy, ref float theFinalPriceSell, ref int theFinalPriceBuyInt, ref int theFinalPriceSellInt, string nameResource, int textArray)
        {
            /*
            if (!isSubtractResource(ConstantNameResource.CREDITS, theFinalPriceToBuyMetalToInt) && !isSubtractResource(ConstantNameResource.METAL, _manager.GetCurrentPlayer().GetResources().GetResourceDictionary().GetValue(ConstantNameResource.METAL)))
            {
                return;
            }
            */

            if (!isSubtractResource(ConstantNameResource.CREDITS, theFinalPriceBuyInt))
            {
                return;
            }

            DictionaryResource.SubAttributeDictionary(_dictionaryResource, new Game.Types.KeyValuePair<string, int>(ConstantNameResource.CREDITS, theFinalPriceBuyInt));
            DictionaryResource.AddAttributeDictionary(_dictionaryResource, new Game.Types.KeyValuePair<string, int>(nameResource, 100));
            _playerManager.UpdateResource();

            if (theFinalPriceBuyInt < maximumSizeFinalPriceInt)
            {
                offset = offset - 1;
            }

            isSubtractResource(ConstantNameResource.CREDITS, theFinalPriceBuyInt);
            theFinalPriceBuy = economicBuy(resourcePrice, offset, _marketPriceChangeRate, _commission);
            theFinalPriceBuyInt = Mathf.Clamp(Mathf.RoundToInt(theFinalPriceBuy), minimumSizeFinalPriceInt, maximumSizeFinalPriceInt);
            massiveText[textArray].GetPriceToBuy().text = theFinalPriceBuyInt.ToString();

            theFinalPriceSell = economicSell(resourcePrice, offset, _marketPriceChangeRate, _commission);
            theFinalPriceSellInt = Mathf.Clamp(Mathf.RoundToInt(theFinalPriceSell), minimumSizeFinalPriceInt, maximumSizeFinalPriceInt);
            massiveText[textArray].GetPriceToSell().text = theFinalPriceSellInt.ToString();

            _playerEconomic.SetCommission(_commission);
            _commission = _playerEconomic.GetCommission();

        }

        public void SellResource(ref int resourcePrice, ref int offset, ref float theFinalPriceBuy, ref float theFinalPriceSell, ref int theFinalPriceBuyInt, ref int theFinalPriceSellInt, string nameResource, int textArray)
        {
            if (!isSubtractResource(nameResource, theFinalPriceSellInt))
            {
                return;
            }

            DictionaryResource.AddAttributeDictionary(_dictionaryResource, new Game.Types.KeyValuePair<string, int>(ConstantNameResource.CREDITS, theFinalPriceSellInt));
            DictionaryResource.SubAttributeDictionary(_dictionaryResource, new Game.Types.KeyValuePair<string, int>(nameResource, 100));
            _playerManager.UpdateResource();

            if (theFinalPriceSellInt > minimumSizeFinalPriceInt)
            {
                offset = offset + 1;
            }

            theFinalPriceBuy = economicBuy(resourcePrice, offset, _marketPriceChangeRate, _commission);
            theFinalPriceBuyInt = Mathf.Clamp(Mathf.RoundToInt(theFinalPriceBuy), minimumSizeFinalPriceInt, maximumSizeFinalPriceInt);
            massiveText[textArray].GetPriceToBuy().text = theFinalPriceBuyInt.ToString();

            theFinalPriceSell = economicSell(resourcePrice, offset, _marketPriceChangeRate, _commission);
            theFinalPriceSellInt = Mathf.Clamp(Mathf.RoundToInt(theFinalPriceSell), minimumSizeFinalPriceInt, maximumSizeFinalPriceInt);
            massiveText[textArray].GetPriceToSell().text = theFinalPriceSellInt.ToString();

            _playerEconomic.SetCommission(_commission);
            _commission = _playerEconomic.GetCommission();
        }

        public void clickToPlusMetal()
        {
            BuyResource(ref _priceOfMetal, ref _offsetMetal, ref theFinalPriceToBuyMetal, ref theFinalPriceToSellMetal, ref theFinalPriceToBuyMetalToInt, ref theFinalPriceToSellMetalToInt, ConstantNameResource.METAL, 0);;
        }

        public void clickToMinusMetal()
        {
            SellResource(ref _priceOfMetal, ref _offsetMetal, ref theFinalPriceToBuyMetal, ref theFinalPriceToSellMetal, ref theFinalPriceToBuyMetalToInt, ref theFinalPriceToSellMetalToInt, ConstantNameResource.METAL, 0);
        }

        public void clickToPlusEnergyCrystall()
        {
            BuyResource(ref _priceOfEnergyCrystall, ref _offsetEnergyCrystall, ref theFinalPriceToBuyEnergyCrystall, ref theFinalPriceToSellEnergyCrystall, ref theFinalPriceToBuyEnergyCrystallToInt, ref theFinalPriceToSellEnergyCrystallToInt, ConstantNameResource.ENERGY_CRISTAL, 1);
        }

        public void clickToMinusEnergyCrystall()
        {
            SellResource(ref _priceOfEnergyCrystall, ref _offsetEnergyCrystall, ref theFinalPriceToBuyEnergyCrystall, ref theFinalPriceToSellEnergyCrystall, ref theFinalPriceToBuyEnergyCrystallToInt, ref theFinalPriceToSellEnergyCrystallToInt, ConstantNameResource.ENERGY_CRISTAL, 1);
        }

        public void clickToPlusNanostructure()
        {
            BuyResource(ref _priceOfNanostructure, ref _offsetNanostructure, ref theFinalPriceToBuyNanostructure, ref theFinalPriceToSellNanostructure, ref theFinalPriceToBuyNanostructureInt, ref theFinalPriceToSellNanostructureInt, ConstantNameResource.NANO_STRUCTURE, 2);
        }

        public void clickToMinusNanoNanostructure()
        {
            SellResource(ref _priceOfNanostructure, ref _offsetNanostructure, ref theFinalPriceToBuyNanostructure, ref theFinalPriceToSellNanostructure, ref theFinalPriceToBuyNanostructureInt, ref theFinalPriceToSellNanostructureInt, ConstantNameResource.NANO_STRUCTURE, 2);
        }

        private void UpdateInfoPlayer()
        {
            _dictionaryResource = _playerManager.GetResources();
            CountCoefTrade();

            massiveText[0].GetPriceToBuy().text = theFinalPriceToBuyMetalToInt.ToString();
            massiveText[0].GetPriceToSell().text = theFinalPriceToSellMetalToInt.ToString();
            //massiveText[0].GetPriceToBuy().text = "Text";
            massiveText[1].GetPriceToBuy().text = theFinalPriceToBuyEnergyCrystallToInt.ToString();
            massiveText[1].GetPriceToSell().text = theFinalPriceToSellEnergyCrystallToInt.ToString();

            massiveText[2].GetPriceToBuy().text = theFinalPriceToBuyNanostructureInt.ToString();
            massiveText[2].GetPriceToSell().text = theFinalPriceToSellNanostructureInt.ToString();

        }

        private void CountCoefTrade()
        {
            theFinalPriceToBuyMetal = economicBuy(_priceOfMetal, _offsetMetal, _marketPriceChangeRate, _commission);
            theFinalPriceToBuyMetalToInt = Mathf.RoundToInt(theFinalPriceToBuyMetal);

            theFinalPriceToSellMetal = economicSell(_priceOfMetal, _offsetMetal, _marketPriceChangeRate, _commission);
            theFinalPriceToSellMetalToInt = Mathf.RoundToInt(theFinalPriceToSellMetal);

            theFinalPriceToBuyEnergyCrystall = economicBuy(_priceOfEnergyCrystall, _offsetEnergyCrystall, _marketPriceChangeRate, _commission);
            theFinalPriceToBuyEnergyCrystallToInt = Mathf.RoundToInt(theFinalPriceToBuyEnergyCrystall);

            theFinalPriceToSellEnergyCrystall = economicSell(_priceOfEnergyCrystall, _offsetEnergyCrystall, _marketPriceChangeRate, _commission);
            theFinalPriceToSellEnergyCrystallToInt = Mathf.RoundToInt(theFinalPriceToSellEnergyCrystall);

            theFinalPriceToBuyNanostructure = economicBuy(_priceOfNanostructure, _offsetNanostructure, _marketPriceChangeRate, _commission);
            theFinalPriceToBuyNanostructureInt = Mathf.RoundToInt(theFinalPriceToBuyNanostructure);

            theFinalPriceToSellNanostructure = economicSell(_priceOfNanostructure, _offsetNanostructure, _marketPriceChangeRate, _commission);
            theFinalPriceToSellNanostructureInt = Mathf.RoundToInt(theFinalPriceToSellNanostructure);
        }

        float economicBuy(int resource, int offset, int coefficientOfPriceChangeInTheMarket, float commissionPerPlayer)
        {
            //по определенному курсу, на который влияют объемы закупок и продаж товара.

            // каждый ресурс имеет свою цену + комиссия (налог).

            //(ЦенуТипаРесурса(P)+КолличествоТовараНаРынке(Δ)*КоэффициентаИзмененияЦеныНаРынке(dP))*(1+КоммиссияКаждогоИгрока(C)) = GeneratePriceToBuyResource
            // новая формула: (ЦенуТипаРесурса(P)*(1+КоммиссияКаждогоИгрока(C))-КолличествоТовараНаРынке(Δ)*КоэффициентаИзмененияЦеныНаРынке(dP)) = GeneratePriceToBuyResource
            //float buyRate = (metal + resourceCountOnMarket * coefficientOfPriceChangeInTheMarket) * (1 + commissionPerPlayer);

            float buyRate = ((resource * (1 + commissionPerPlayer)) - (offset * coefficientOfPriceChangeInTheMarket));

            return buyRate;
        }

        float economicSell(int resource, int offset, int coefficientOfPriceChangeInTheMarket, float commissionPerPlayer)
        {
            //(ЦенуТипаРесурса(P)+КолличествоТовараНаРынке(Δ)*КоэффициентаИзмененияЦенынаРынке(dP))*(1-КоммиссияКаждогоИгрока(C)) = sellRate
            float sellRate = ((resource * (1 - commissionPerPlayer)) + (offset * coefficientOfPriceChangeInTheMarket));

            return sellRate;
        }
    }
}

// playerEconomic (скрипт), в нём будет переменная offset...