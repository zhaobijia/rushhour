using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Alert : MonoBehaviour
{
    public TMP_Text alertText;

    public void SetAlertText(string textString)
    {
        alertText.text = textString;
    }
}
