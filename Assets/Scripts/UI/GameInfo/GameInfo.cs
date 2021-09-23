using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameInfo : MonoBehaviour
{
    //variable to display
    int _coin;
    int _health;
    float _timer;
    List<int> _perkList;
    int _level;

    GameManager gameManager;
    //UI
    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text timerText;
 //   [SerializeField] TMP_Text levelText;
    [SerializeField] Slider healthBarSlider;
    [SerializeField] List<Image> perkImageList;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
       
    }

    private void Start()
    {
        UpdateCoin();
    }

    public void Update()
    {
        UpdateTimer();
        UpdateHealthBar();
    }
    public void UpdateCoin()
    {
        coinText.text = gameManager.m_coins.ToString();
    }
   public void UpdateTimer()
    {
        timerText.text = ((int)gameManager.m_timer).ToString();
    }

    public void UpdateHealthBar()
    {
        healthBarSlider.value = gameManager.mainChar.m_health / gameManager.mainChar.m_fullHealth;
    }
}
