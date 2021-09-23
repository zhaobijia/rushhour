using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestUI : MonoBehaviour
{
    public List<RectTransform> questRectList;
    public GameObject QuestCardPrefab;
    public RectTransform parentRect;
    public RectTransform spawnRect;

    GameManager m_gameManager;
    List<GameObject> cardsObjs;

    private void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        cardsObjs = new List<GameObject>();
    }
    private void Start()
    {
        
    }

    //Add: check questqueue.count, find corresponding position from the rect list and move it toward that position
    //complete/fail a quest, first quest in the queue is destroyed
    //all quest move left for offset amount:)
    public void AddNewQuestCard(Quest newQuest)
    {
        //1. read recipe from (?)

        GameObject cardObj = Instantiate(QuestCardPrefab, spawnRect.transform.position, Quaternion.identity);

        cardObj.transform.SetParent(parentRect);
        cardsObjs.Add(cardObj);
        QuestCardUI cardUI = cardObj.GetComponent<QuestCardUI>();
        cardUI.ShowCardContents(newQuest);

        //4. move card to right position
        int posIndex = m_gameManager.m_currentLevelQuest.questQueue.Count - 1;
        SnapCard(cardObj, questRectList[posIndex].transform);
    }

    public void SnapAllCardOnDestroy()
    {
        foreach(GameObject obj in cardsObjs)
        {
            int posIndex = cardsObjs.IndexOf(obj);
            SnapCard(obj, questRectList[posIndex].transform);
        }
    }

    public void RemoveObj(GameObject obj) {
       
        if (cardsObjs.Contains(obj))
        {
            cardsObjs.Remove(obj);
        }
    }

    public void SnapCard(GameObject cardObj, Transform snapTo)
    {
        
        LeanTween.move(cardObj, snapTo,  0.5f).setEaseInQuad();
    }
}
