using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int m_levelId;
    public float m_levelTime; //IN seconds
    public int m_levelQuestPriceBase;
    //public float m_questPriceMediumAddOn;
    //public float m_questPriceHardAddOn;

   // public int m_questLength;
    public float m_questTimeLimit; //IN SECONDS
    public float m_questAlertTime; //IN Seconds
    public List<int> m_levelEnemyIds; 
  //  public int m_enemySpawnTileAmount;//not in use, designed for level difficulty
    public TextAsset m_levelMap;

    public List<Recipe> recipes;

    public int m_enemyWaveAmountOnFail;



}
