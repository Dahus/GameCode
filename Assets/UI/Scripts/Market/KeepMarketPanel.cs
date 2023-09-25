using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepMarketPanel : MonoBehaviour
{
    [SerializeField] private Text _textNameResource;
    [SerializeField] private Button _buttonPlus;
    [SerializeField] private Button _buttonMinus;
    [SerializeField] private Text _priceToBuy;
    [SerializeField] private Text _priceToSell;

    public Text GetText ()
    {
        return _textNameResource;
    }

    public Text GetPriceToBuy ()
    {
        return _priceToBuy;
    }

    public Text GetPriceToSell ()
    {
        return _priceToSell;
    }

    public void SetPriceToBuy(Text priceToBuy)
    {
        this._priceToBuy = priceToBuy;
    }

    public void SetPriceToSell(Text priceToSell)
    {
        this._priceToSell = priceToSell;
    }
}