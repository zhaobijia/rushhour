using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestCardUI : MonoBehaviour
{
    public Image orderImage;
    public List<Image> enemyImages; //assign in editor
    public Slider timeBar;
    public QuestUI questUI;
    TextureManager m_textureManager;
    Quest m_quest;
    bool startAlert = false;
    private void Awake()
    {
        m_textureManager = FindObjectOfType<TextureManager>();
        questUI = FindObjectOfType<QuestUI>();
    }

    private void Update()
    {
        if (m_quest != null)
        {
            if (m_quest.m_expired)
            {
                CardExpired();
            }
            else if (m_quest.m_alerting)
            {
                if (!startAlert)
                {
                    CardAlerting();
                }
                CardTimer();

            }
            else
            {
                CardTimer();
            }
        }

    }

    void UpdateCard()
    {
       //update complete effect
    }

    public void ShowCardContents(Quest _quest)
    {
        m_quest = _quest;
        //recipe image
        Recipe _recipe = m_quest.m_recipe;
        Sprite _orderSprite = m_textureManager.m_recipeAssetDict[_recipe.recipe_Id]._recipeSprite;
        orderImage.sprite = _orderSprite;
        //enemy list images
        //prob: 2 items vs 4 items;
        int _index = 0;
        foreach(int enemyId in _recipe.recipeEnemyIdList)
        {
            //use set active to add image 
            Sprite _enemySprite = m_textureManager.m_enemyAssetDict[enemyId]._enemySprite;
            enemyImages[_index].gameObject.SetActive(true);
            enemyImages[_index].sprite = _enemySprite;
            _index++;
        }
        //asign timebar
        CardTimer();


    }



    void CardExpired()
    {
        questUI.RemoveObj(gameObject);
        questUI.SnapAllCardOnDestroy();
        
        Destroy(gameObject, 0.1f);
        //
    }

    void CardAlerting()
    {
        Debug.Log("alerting");
        startAlert = true;
        float _to = transform.position.y + 10f;
        LeanTween.moveY(gameObject,_to, 0.3f).setLoopPingPong().setEaseInElastic();
    }

    float TimeLeftRatio()
    {
        return (float)(m_quest.m_timeLimit - m_quest.m_timePassed) / m_quest.m_timeLimit;
    }
    void CardTimer()
    {
        //for updating time bar
        timeBar.value = TimeLeftRatio();
    }

}
