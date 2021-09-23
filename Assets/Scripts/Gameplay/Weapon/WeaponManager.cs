using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponList
{
    int weaponID;
    GameObject weaponPrefab;
}

public class WeaponManager : MonoBehaviour
{
    Dictionary<int, GameObject> weaponPrefabList;
}
