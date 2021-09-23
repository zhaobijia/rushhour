using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{

    public bool hasEnemy = true;
    public MainCharacter mainCharacter;
    public Character mainChar;

    public Level[] m_levels;
    public Dictionary<int, Level> m_levelDict = new Dictionary<int, Level>();
    public Level m_currentLevel;
    int m_currentLevelId = 1; //++ at end and savable?

    public UIManager uiManager;

    public Perk[] m_currentPerks; // store perks data
    public LevelQuests m_currentLevelQuest;
    public Map m_map;
    public EnemyWave m_enemyWave;
    public int m_maxQuestInQueue = 5;

    public float m_timer; // time left
    float m_startTime;
    bool levelEnd;

    public int m_coins;

    private void Awake()
    {
        InitializeLevelDictionary();
        //initially set level to 1 with no saving file
        m_currentLevelId = 1;

        //after save grab level info from saves
        
    }

    private void Start()
    {
        LoadLevel();
        StartGame();

    }

    private void Update()
    {
       

        if (levelEnd)
        {
            levelEnd = false;

            //1. transition
            LevelTransition();
            //2. reset and move to next level
            m_currentLevelId++;
        }
        else if(m_timer>0)
        {
            CountDown();
        }
        else
        {
            //level not end and timer smaller than 0:
            //choosing perk time
        }
    }
    
    public Character MainChar 
    {
        get
        {
            return mainChar;
        }
    }

    void InitializeLevelDictionary()
    {
        foreach(Level l in m_levels)
        {
            m_levelDict.Add(l.m_levelId, l);
        }
    }

 
    //GamePlay Walkthrough
    public void ShowStartMenu()
    {
        //
    }

    public void StartGame()
    {
        //generate map

        //spawn player
      //  SpawnPlayer();
        //load level quest

        LoadLevelQuest();
        //start level timer
        StartLevelTimer();
        //generate quest list

        TestTriggerEnemyWave();//TEST
    }

    void StartLevelTimer()
    {
        m_startTime = Time.time;
        m_timer = m_currentLevel.m_levelTime;
        
    }

    void CountDown()
    {
        float timePassed = Time.time - m_startTime;
        m_timer = m_currentLevel.m_levelTime - timePassed;

        if (m_timer < 00)
        {
            levelEnd = true;
        }
    }


    void SpawnPlayer()
    {
        Vector3 offset = new Vector3(0, 1, 0);
        Vector3 posToSpawn = m_map.playerSpawnPosition + offset ;

        mainChar.transform.position = posToSpawn;
    }
    void LoadLevel()
    {
        if (m_levelDict.Count > 0)
        {
            m_currentLevel = m_levelDict[m_currentLevelId];
        }

        m_coins = 100;
        m_map.LoadMapOnLevel(m_currentLevel.m_levelId);
    }

    void LevelTransition()
    {
        
        uiManager.ShowLevelTransition();
        m_map.HideMapOnLevel();
    }

    void LoadLevelQuest()
    {
        LevelQuests levelQuests = GetComponentInChildren<LevelQuests>();
        levelQuests.LoadLevelQuest();
    }
    void TestTriggerEnemyWave()
    {
        //testing fucntion
        TriggerEnemyWave(1,1);
    }

    public void UpdateQuestQueue(LevelQuests _levelQuest)
    {
        m_currentLevelQuest = _levelQuest;
    }

    public bool CheckQuest(int _dropEnemyId)
    {
        bool foundQuestItem = m_currentLevelQuest.CheckQuestOnPick(_dropEnemyId);

        return foundQuestItem;
    }

    public void EarnCoins(int _coinAmount)
    {
        m_coins += _coinAmount;
    }

    //Enemy Wave Button

    public void TriggerQuestFailPunishment(Quest quest)
    {
        TriggerMapTrap();
        TriggerEnemyWaveOnFailedQuest(quest);
    }

    void TriggerMapTrap()
    {

    }

    void TriggerEnemyWaveOnFailedQuest(Quest quest)
    {
        Recipe recipe = quest.m_recipe;

        int _indexE = Random.Range (0 , recipe.recipeEnemyIdList.Count);

        TriggerEnemyWave(recipe.recipeEnemyIdList[_indexE], m_currentLevel.m_enemyWaveAmountOnFail);


    }
    public void TriggerEnemyWave(int _enemyId, int _waveAmount)
    {
        if (m_map != null)
        {
            
            m_enemyWave.StartWave(m_map.enemySpawnPositions, _enemyId, _waveAmount);
        }
        
    }

}
