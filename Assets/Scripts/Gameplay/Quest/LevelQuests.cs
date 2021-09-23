using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelQuests : MonoBehaviour
{

    public QuestUI questUI;

    Level m_level;
    List<Recipe> m_recipes;
    GameManager m_gameManager;
    float m_questTimeLimit;
    float m_questTimeLeftAlert;
    public float m_timeBetweenGenerate= 5.0f;
    bool m_isGenerating; //set to true at beginging of level;
    public Queue<Quest> questQueue = new Queue<Quest>();


    private void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
    }

    public void Update()
    {
        UpdateQuestTime();
        CheckFirstQuestExpiration();
    }

    void UpdateQuestTime()
    {
        foreach(Quest quest in questQueue)
        {
            quest.CheckExpiration();
        }
    }
    void CheckFirstQuestExpiration()
    {
        if (questQueue.Count > 0)
        {
            Quest firstQuest = questQueue.Peek();

            if (firstQuest != null && firstQuest.m_expired)
            {

                //expired
                //trigger wave and 

                m_gameManager.TriggerQuestFailPunishment(firstQuest);

                questQueue.Dequeue();

                //ui card destroy
            }
        }
    }

    public bool CheckQuestOnPick(int _enemyDropId)
    {
        //peak first quest on the queue and check the c
        Quest firstQuest = questQueue.Peek();
        //check if firstquest still need this drop
        bool foundQuestItem = firstQuest.CheckItemComplete(_enemyDropId);

        if (foundQuestItem)
        {
            //Update UI
        }

        //if first quest is done:
        if (firstQuest!=null && firstQuest.CheckQuestComplete())
        {
            //earn coins

            m_gameManager.EarnCoins(firstQuest.GetRewardPrice()+m_level.m_levelQuestPriceBase);
            //completed quest drop quest
            questQueue.Dequeue();
            
        }

        return foundQuestItem;
        
    }

    public void LoadLevelQuest()
    {
        GetLevelInfo();
        m_isGenerating = true;
        StartCoroutine( GenerateQuestQueue());
    }

    void GetLevelInfo()
    {
        m_level = m_gameManager.m_currentLevel;
        m_recipes = m_level.recipes;
        m_questTimeLimit = m_level.m_questTimeLimit;
        m_questTimeLeftAlert = m_level.m_questAlertTime;
    }

    Quest RandomQuest()
    {
        //randomly grab a recipe from recipe list
        //and use this recipe to create a new quest 
        //and add quest to quest queue;

        int len = m_recipes.Count;
        int index = (int)Random.Range(0, len);

        Quest quest = new Quest(m_recipes[index], m_questTimeLimit, m_questTimeLeftAlert);


        return quest;
    }

    IEnumerator GenerateQuestQueue()
    {
        //generate quest queue with level info

        //generate quest gradually

        while (m_isGenerating)
        {
            if (questQueue.Count < m_gameManager.m_maxQuestInQueue)
            {
                //randomly genearte quest base on recipe of this level
                Quest newQuest = RandomQuest();
                questQueue.Enqueue(newQuest);
                m_gameManager.UpdateQuestQueue(this);

                questUI.AddNewQuestCard(newQuest);

                
            }
            yield return new WaitForSeconds(m_timeBetweenGenerate);
        }

        yield break;
    }

    public void ClearLevelQuest()
    {
        //Call at the end of a level
        m_isGenerating = false;
        questQueue.Clear();
    }

    

}
