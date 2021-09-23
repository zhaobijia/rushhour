using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterIcon : MonoBehaviour
{
    [SerializeField] Material m_material;
    [SerializeField] Texture m_texture;
    [SerializeField] Vector3 m_offset;
    public Transform m_targetCharTransform;
    
    

    private void Start()
    {
        
        m_material.SetTexture("_MainTex", m_texture);
        
       // FaceToCamera();
    }

    void FaceToCamera()
    {
        Camera cam = Camera.main;
        Vector3 dir =  m_targetCharTransform.position- cam.transform.position;
        transform.LookAt(dir);
    }

    private void Update()
    {
        FaceToCamera();
    }
}
