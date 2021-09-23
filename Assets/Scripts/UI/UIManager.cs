using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Alert alert;


    public GameObject levelTransitionPanel;
    public List<Image> perkImages;

    public void ShowAlert(string alert_str)
    {
        alert.SetAlertText(alert_str);
    }

    public void ShowLevelTransition()
    {
        levelTransitionPanel.SetActive(true);
        //attach perks on to iamge button
    }
}
