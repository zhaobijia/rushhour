using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    public Perk[] perklist;
    public Dictionary<int, Perk> perkDict = new Dictionary<int, Perk>();

    public List<Perk> allPerks = new List<Perk>();
    public List<Perk> chosenPerks = new List<Perk>();

    public List<int> perkIdsToDisplay = new List<int>();

    private void Awake()
    {
        InitializePerkDictionary();
        InitializeAllPerkList();
    }
    public void InitializePerkDictionary()
    {

        foreach (Perk p in perklist)
        {
            perkDict.Add(p.perkId, p);
        }
    }

    public void InitializeAllPerkList()
    {
        foreach (Perk p in perklist)
        {
            perkDict.Add(p.perkId, p);
        }
    }

    public List<int> GetRandomPerks(int amount)
    {
        //no repeat entry
        List<int> randomPerkIdlist = new List<int>();

        for (int i = 0; i < amount; i++)
        {
            int randomIndex = (int)Random.Range(0, allPerks.Count);

            randomPerkIdlist.Add(allPerks[randomIndex].perkId);


        }

        return randomPerkIdlist;

    }

    public void ChoosePerk(int _perkId)
    {
        Perk _perk = perkDict[_perkId];
        chosenPerks.Add(_perk);
        allPerks.Remove(_perk);

        //aply perks on character
        FindObjectOfType<MainCharacter>().AddCurrentPerk(_perk);
    }
}
