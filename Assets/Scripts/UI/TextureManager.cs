using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyTexture
{
    public int _enemyID;
    public Texture _enemyTexture;
    public Sprite _enemySprite;
}

[System.Serializable]
public struct RecipeTexture
{
    public int _recipeID;
    public Sprite _recipeSprite;
}

[System.Serializable]
public struct PerkTexture
{
    public int _perkId;
    public Sprite _perkSprite;
}
public class TextureManager : MonoBehaviour
{
    public EnemyTexture[] enemyTextures;
    public RecipeTexture[] recipeTextures;
    public PerkTexture[] perkTextures;

    public Dictionary<int, EnemyTexture> m_enemyAssetDict = new Dictionary<int, EnemyTexture>();
    public Dictionary<int, RecipeTexture> m_recipeAssetDict = new Dictionary<int, RecipeTexture>();
    public Dictionary<int, PerkTexture> m_perkAssetDict = new Dictionary<int, PerkTexture>();

    private void Awake()
    {
        CreateEnemyTextureDict();
        CreateRecipeTextureDict();
        CreatePerkTextureDict();
    }

     void CreateEnemyTextureDict()
    {
        foreach(EnemyTexture eT in enemyTextures)
        {
            m_enemyAssetDict.Add(eT._enemyID, eT);
        }
    }

    void CreateRecipeTextureDict()
    {
        foreach(RecipeTexture rT in recipeTextures)
        {
            m_recipeAssetDict.Add(rT._recipeID, rT);
        }
    }

    void CreatePerkTextureDict()
    {
        foreach(PerkTexture pT in perkTextures)
        {
            m_perkAssetDict.Add(pT._perkId, pT);
        }
    }
}
