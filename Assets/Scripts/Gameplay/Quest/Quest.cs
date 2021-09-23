using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public struct QuestItem
{
    public int _enemyId;
    public bool _completed;


    public QuestItem(int id, bool c)
    {
        _enemyId = id;
        _completed = c;
    }
}

public class Quest 
{ 
    public float m_timeLimit;
    float m_timeLeftAlert;
    int m_questRewardPrice;
    List<QuestItem> questList;
    public Recipe m_recipe;
    public bool m_expired;
    public bool m_alerting;
    public float m_timePassed;

    float m_startTime;


    public Quest(Recipe _recipe, float _timeLimit, float _timeAlert)
    {
        m_timeLimit = _timeLimit;
        m_recipe = _recipe;
        m_questRewardPrice =  m_recipe.recipePrice;
        m_timeLeftAlert = _timeAlert;

        questList = new List<QuestItem>();
        m_startTime = Time.time;
        m_expired = false;
        m_alerting = false;
        InitializeQuestItemList();
    }

    public int GetRewardPrice()
    {
        return m_questRewardPrice;
    }



    void InitializeQuestItemList()
    {
        foreach (int enemyId in m_recipe.recipeEnemyIdList)
        {
            QuestItem item = new QuestItem();
            item._enemyId = enemyId;
            item._completed = false;
            questList.Add(item);
        }
    }
    public bool CheckItemComplete(int _enemyDropId)
    {
        for(int i = 0; i < questList.Count; i++)
        {

            if (questList[i]._enemyId == _enemyDropId && !questList[i]._completed)
            {
                //mark item as true
                questList[i] = new QuestItem(questList[i]._enemyId, true);
                
                //ui check

                return true;
            }
        }
        //if return false, dont pick it up or something
        return false;

    }

    public bool CheckQuestComplete()
    {
        foreach(QuestItem item in questList)
        {
            if (!item._completed)
            {
                //still not check
                return false;
            }
        }

        return true;
    }

    public bool CheckExpiration()
    {
        //call at update 
        m_timePassed = Time.time - m_startTime;
        if (m_timePassed<m_timeLimit && (m_timeLimit - m_timePassed) < m_timeLeftAlert)
        {
            //ui start alerting time 
            m_alerting = true;
        }
        else if(m_timePassed >= m_timeLimit)
        {
            m_expired = true;
        }

        return m_expired;
    }

    


}
