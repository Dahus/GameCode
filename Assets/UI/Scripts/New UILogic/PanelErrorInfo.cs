using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class PanelErrorInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text textError;
    [SerializeField] private int timeFade;


    public void UpdateTextError(string text)
    {
        textError.text = text;
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        textError.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeFade);
        textError.gameObject.SetActive(false);
    }
}
