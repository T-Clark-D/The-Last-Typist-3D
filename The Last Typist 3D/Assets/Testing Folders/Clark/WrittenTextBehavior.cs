using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrittenTextBehavior : MonoBehaviour
{
    public GameObject m_playerAnchor;
    private Camera m_cam;
    public RectTransform m_RT;
  
    void Start()
    {
        m_cam = Camera.main;
        m_RT = GetComponent<RectTransform>();
    }

    void Update()
    {
        //anchor on player transformed into point on canvas
        m_RT.position = m_cam.WorldToScreenPoint(m_playerAnchor.transform.position);
    }
}
