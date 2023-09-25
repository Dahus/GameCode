using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIParametresLogic : MonoBehaviour
{


    [SerializeField] private int _countTextElement;
    [SerializeField] private GameObject _parent;
    [SerializeField] private Text _prefabText;
    [SerializeField] private List<Text> _listParametresText;

    [SerializeField] private ModelForParametres _model;

    private void Start()
    {
        Setup();
    }

    public void UpdateUI(int id, string str)
    {
        _listParametresText[id].gameObject.SetActive(true);
        _listParametresText[id].text = str;
    }

    public void Setup()
    {
        for (int i = 0; i < _countTextElement; i++)
        {
            var curText = Instantiate(_prefabText);
            curText.transform.SetParent(_parent.transform);
            _listParametresText.Add(curText);
            curText.gameObject.SetActive(false);
        }
        
    }

    public void SetModelForParametres(ModelForParametres model)
    {
        Debug.LogError("Модель " + model.name);
        _model = model;
    }

    public void UpdateInfo(GameObject obj)
    {
        Clear();
        _model.UpdateInfoObject(obj);
        _model.UpdateUIParametres(this);
    }

    public void Clear()
    {
        for (int i = 0; i < _countTextElement; i++)
        {
            _listParametresText[i].text = "";
            _listParametresText[i].gameObject.SetActive(false);
        }
    }

}
