using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName ="ScriptableObjects/Projectile")]
public class ProjectileStats : ScriptableObject
{

    public string m_shaderName;
    public Color m_color;
    public Texture m_texture;
    public Material m_material;

    public float m_spawnTime;
    public float m_liveTime;
    public float m_allowedTime;
}
