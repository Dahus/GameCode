using UnityEngine;
using UnityEngine.UI;

public class OnlyNumbersInput : MonoBehaviour
{
    public InputField inputField;

    private void Update()
    {
        inputField.onValidateInput += delegate (string input, int charIndex, char addedChar)
        {
            if (char.IsDigit(addedChar)) { 
                return addedChar;
            }
            return '\0';
        };
    }
}
